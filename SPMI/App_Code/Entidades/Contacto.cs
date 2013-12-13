using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Materia
/// </summary>
public class Contacto
{
	public Contacto()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Atributos y propiedades
    string K_PREFIJO = "Contacto";
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
    string _apPaterno = string.Empty;

    public string ApPaterno
    {
        get { return _apPaterno; }
        set { _apPaterno = value; }
    }
    string _apMaterno = string.Empty;

    public string ApMaterno
    {
        get { return _apMaterno; }
        set { _apMaterno = value; }
    }
    string _celular = string.Empty;

    public string Celular
    {
        get { return _celular; }
        set { _celular = value; }
    }
     string _radio = string.Empty;

    public string Radio
    {
        get { return _radio; }
        set { _radio = value; }
    }
     string _email = string.Empty;

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    bool _status = true;
    public bool Status
    {
        get { return this._status; }
        set { this._status = value; }
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
        DBaccess.ParameterAdd(cmd, "@apellidoP", SqlDbType.VarChar, this.ApPaterno);
        DBaccess.ParameterAdd(cmd, "@apellidoM", SqlDbType.VarChar, this.ApMaterno);
        DBaccess.ParameterAdd(cmd, "@celular", SqlDbType.VarChar, this.Celular);
        DBaccess.ParameterAdd(cmd, "@radio", SqlDbType.VarChar, this.Radio);
        DBaccess.ParameterAdd(cmd, "@email", SqlDbType.VarChar, this.Email);
        DBaccess.ParameterAdd(cmd, "@status", SqlDbType.Bit, this.Status);
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
            this.ApPaterno = result.Rows[0]["apellidoP"].ToString();
            this.ApMaterno = result.Rows[0]["apellidoM"].ToString();
            this.Celular = result.Rows[0]["celular"].ToString();
            this.Radio = result.Rows[0]["radio"].ToString();
            this.Email = result.Rows[0]["email"].ToString();
            this.Status = Convert.ToBoolean(result.Rows[0]["status"]);
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
    public DataTable ListaNombreCompleto(string pista)
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LISTFULLNAME");
        DBaccess.ParameterAdd(cmd, "@pista", SqlDbType.VarChar, pista);
        return DBaccess.ExecuteSQLSelect(cmd);
    }
    #endregion
}