using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Curso
/// </summary>
public class ContactoEmpresa
{
    public ContactoEmpresa()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Atributos y propiedades
    string K_PREFIJO = "ContactoEmpresa";
    int _id = 0;
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    int _idContacto = 0;
    public int IdContacto
    {
        get { return _idContacto; }
        set { _idContacto = value; }
    }
    int _idEmpresa = 0;
    public int IdEmpresa
    {
        get { return _idEmpresa; }
        set { _idEmpresa = value; }
    }
    int _periodo = 0;

    private DateTime _fechaInicio = DateTime.Now;

    public DateTime FechaInicio
    {
        get { return _fechaInicio; }
        set { _fechaInicio = value; }
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
        int n = 0;
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_UPSERT");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);
        DBaccess.ParameterAdd(cmd, "@idContacto", SqlDbType.Int, this.IdContacto);
        DBaccess.ParameterAdd(cmd, "@idEmpresa", SqlDbType.Int, this.IdEmpresa);
        DBaccess.ParameterAdd(cmd, "@fechaRegistro", SqlDbType.DateTime, this.FechaInicio);
        DBaccess.ParameterAdd(cmd, "@status", SqlDbType.Bit, this.Status);
        if (this._id == 0)
        {
            this._id = Convert.ToInt32(DBaccess.EjecutarSQLScalar(cmd));         
            return this._id > 0;
        }
        else
        {
             n=  DBaccess.EjecutarSQLNonQuery(cmd);
           
            //return s;
        }
        return n>0;

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
            this.IdContacto = Convert.ToInt32(result.Rows[0]["idContacto"]);
            this.IdEmpresa = Convert.ToInt32(result.Rows[0]["idEmpresa"]);
            this.FechaInicio = Convert.ToDateTime(result.Rows[0]["fechaRegistro"].ToString());
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
    public DataTable Lista(int id)
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LIST");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.VarChar, id);
        return DBaccess.ExecuteSQLSelect(cmd);
    }
    /// <summary>
    /// Lista de alumnos registrados
    /// </summary>
    public DataTable ListaFiltro(int id,string pista)
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LISTFILTRO");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, id);
        DBaccess.ParameterAdd(cmd, "@pista", SqlDbType.VarChar, pista);
        return DBaccess.ExecuteSQLSelect(cmd);
    }
    #endregion
}