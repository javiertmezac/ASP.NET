using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Grado
/// </summary>
public class Grados
{
	public Grados()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    #region Atributos y propiedades
    string K_PREFIJO = "Grado";
    int _id = 0;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    int _profesor = 0;

    public int Profesor
    {
        get { return _profesor; }
        set { _profesor = value; }
    }

    string _grado = string.Empty;

    public string Grado
    {
        get { return _grado; }
        set { _grado = value; }
    }
    int _tipoGrado = 0;

    public int TipoGrado
    {
        get { return _tipoGrado; }
        set { _tipoGrado = value; }
    }
    string _cedula = string.Empty;

    public string Cedula
    {
        get { return _cedula; }
        set { _cedula = value; }
    }
    string _institucion = string.Empty;

    public string Institucion
    {
        get { return _institucion; }
        set { _institucion = value; }
    }
    DateTime _fecha = DateTime.Now;

    public DateTime Fecha
    {
        get { return _fecha; }
        set { _fecha = value; }
    }

    #endregion

    #region Metodos Publicos
    /// <summary>
    /// Agrega registros de alumnos
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool UpSert()
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_UPSERT");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);
        DBaccess.ParameterAdd(cmd, "@profesor", SqlDbType.VarChar, this.Profesor);
        DBaccess.ParameterAdd(cmd, "@grado", SqlDbType.VarChar, this.Grado);
        DBaccess.ParameterAdd(cmd, "@tipoGrado", SqlDbType.VarChar, this.TipoGrado);
        DBaccess.ParameterAdd(cmd, "@cedula", SqlDbType.VarChar, this.Cedula);
        DBaccess.ParameterAdd(cmd, "@institucion", SqlDbType.VarChar, this.Institucion);
        DBaccess.ParameterAdd(cmd, "@fecha", SqlDbType.DateTime, this.Fecha);
        if (this._id == 0)
        {
            this._id = Convert.ToInt32(DBaccess.EjecutarSQLScalar(cmd));
            return this._id > 0;
        }
        else
        {
            bool s = DBaccess.EjecutarSQLNonQuery(cmd) > 0;
            return s;
        }

    }    
    /// <summary>
    /// Carga un alumno
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool Load()
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_SELECT");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);

        DataTable result = DBaccess.ExecuteSQLSelect(cmd);
        if (result.Rows.Count > 0)
        {
            this.Id = Convert.ToInt32(result.Rows[0]["id"]);
            this.Profesor = Convert.ToInt32(result.Rows[0]["profesor"].ToString());
            this.Grado = result.Rows[0]["grado"].ToString();
            this.TipoGrado = Convert.ToInt32(result.Rows[0]["tipoGrado"].ToString());
            this.Cedula = result.Rows[0]["cedula"].ToString();
            this.Institucion = result.Rows[0]["institucion"].ToString();
            this.Fecha = Convert.ToDateTime(result.Rows[0]["fecha"].ToString());            
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Elimina alumnos registrados
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool Delete()
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_DELETE");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);
        return DBaccess.EjecutarSQLNonQuery(cmd) > 0;
    }
    /// <summary>
    /// Lista de alumnos registrados
    /// </summary>
    public DataTable Lista(int idProfesor)
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LIST");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, idProfesor);
        return DBaccess.ExecuteSQLSelect(cmd);
    }

    #endregion

}