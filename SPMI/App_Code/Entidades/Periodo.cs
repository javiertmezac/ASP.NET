using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Periodo
/// </summary>
public class Periodo
{
	public Periodo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Atributos y propiedades
    string K_PREFIJO = "Periodo";
    int _id = 0;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    string _nombre = string.Empty;

    public string Nombre
    {
        get { return _nombre; }
        set { _nombre = value; }
    }
   
    DateTime _fechaInicio = DateTime.Now;

    public DateTime FechaInicio
    {
        get { return _fechaInicio; }
        set { _fechaInicio = value; }
    }
    DateTime _fechaFin = DateTime.Now;

    public DateTime FechaFin
    {
        get { return _fechaFin; }
        set { _fechaFin = value; }
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
        DBaccess.ParameterAdd(cmd, "@nombre", SqlDbType.VarChar, this.Nombre);    
        DBaccess.ParameterAdd(cmd, "@fechaInicio", SqlDbType.DateTime, this.FechaInicio);
        DBaccess.ParameterAdd(cmd, "@fechaFin", SqlDbType.DateTime, this.FechaFin);
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
            this.Nombre = result.Rows[0]["nombre"].ToString();
            this.FechaInicio = Convert.ToDateTime(result.Rows[0]["fechaInicio"].ToString());
            this.FechaFin = Convert.ToDateTime(result.Rows[0]["fechaFin"].ToString());
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

    #endregion

}