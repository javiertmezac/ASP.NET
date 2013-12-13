using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TipoGrado
/// </summary>
public class TipoGrado
{
	public TipoGrado()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Atributos y propiedades
    string K_PREFIJO = "TipoGrado";
    int _id = 0;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    int _valor = 0;

    public int Valor
    {
        get { return _valor; }
        set { _valor = value; }
    }

    string _descripcion = string.Empty;

    public string Descripcion
    {
        get { return _descripcion; }
        set { _descripcion = value; }
    }
    int _tipoGrado = 0;

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
        DBaccess.ParameterAdd(cmd, "@descripcion", SqlDbType.VarChar, this.Descripcion);
        DBaccess.ParameterAdd(cmd, "@valor", SqlDbType.VarChar, this.Valor);       
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
            this.Descripcion = result.Rows[0]["descripcion"].ToString();
            this.Valor = Convert.ToInt32(result.Rows[0]["valor"].ToString());           
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
    public DataTable Lista(string pista)
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LIST");
        DBaccess.ParameterAdd(cmd, "@pista", SqlDbType.VarChar, pista);
        return DBaccess.ExecuteSQLSelect(cmd);
    }
    //public DataTable ListaNombreCompleto(string pista)
    //{
    //    SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LIST_NOMBRE");
    //    DBaccess.ParameterAdd(cmd, "@pista", SqlDbType.VarChar, pista);
    //    return DBaccess.ExecuteSQLSelect(cmd);
    //}
    #endregion
}