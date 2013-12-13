using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for Profesor
/// </summary>
public class Empresa
{
	public Empresa()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Atributos y propiedades
    string K_PREFIJO = "Empresa";
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
    string _rfc = string.Empty;

    string _noCliente = string.Empty;

    public string NoCliente
    {
        get { return _noCliente; }
        set { _noCliente = value; }
    }

    public string RFC
    {
        get { return _rfc; }
        set { _rfc = value; }
    }
    string _telefono = string.Empty;

    public string Telefono
    {
        get { return _telefono; }
        set { _telefono = value; }
    }
    string _colonia = string.Empty;

    public string Colonia
    {
        get { return _colonia; }
        set { _colonia = value; }
    }
    string _calle = string.Empty;

    public string Calle
    {
        get { return _calle; }
        set { _calle = value; }
    }
    int _noInterior = 0;

    public int NoInterior
    {
        get { return _noInterior; }
        set { _noInterior = value; }
    }
    int _noExterior = 0;

    public int NoExterior
    {
        get { return _noExterior; }
        set { _noExterior = value; }
    }
    int _codPostal = 0;

    public int CodPostal
    {
        get { return _codPostal; }
        set { _codPostal = value; }
    }

    private DateTime _fechaRegistro = DateTime.Now;

    public DateTime FechaRegistro
    {
        get { return _fechaRegistro; }
        set { _fechaRegistro = value; }
    }
    bool _status = true;
    public bool Status
    {
        get { return this._status; }
        set { this._status = value; }
    }

    int _precio = 0;

    public int Precio
    {
        get { return _precio; }
        set { _precio = value; }
    }
    #endregion

    #region Metodos Publicos
    /// <summary>
    /// Agrega registros de empresas
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool UpSert()
    {
        SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_UPSERT");
        DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);
        DBaccess.ParameterAdd(cmd, "@noCliente", SqlDbType.VarChar, this.NoCliente);
        DBaccess.ParameterAdd(cmd, "@nombre", SqlDbType.VarChar, this.Nombre);
        DBaccess.ParameterAdd(cmd, "@rfc", SqlDbType.VarChar, this.RFC);
        DBaccess.ParameterAdd(cmd, "@tel", SqlDbType.VarChar, this.Telefono);
        DBaccess.ParameterAdd(cmd, "@colonia", SqlDbType.VarChar, this.Colonia);
        DBaccess.ParameterAdd(cmd, "@calle", SqlDbType.VarChar, this.Calle);
        DBaccess.ParameterAdd(cmd, "@noInt", SqlDbType.Int, this.NoInterior);
        DBaccess.ParameterAdd(cmd, "@noExt", SqlDbType.Int, this.NoExterior);
        DBaccess.ParameterAdd(cmd, "@cPostal", SqlDbType.VarChar, this.CodPostal);
        DBaccess.ParameterAdd(cmd, "@fechaRegistro", SqlDbType.DateTime, this.FechaRegistro);
        DBaccess.ParameterAdd(cmd, "@status", SqlDbType.Bit, this.Status);
        DBaccess.ParameterAdd(cmd, "@idPrecio", SqlDbType.TinyInt, this.Precio);
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
    /// Carga una empresa
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
            this.NoCliente = result.Rows[0]["noCliente"].ToString();
            this.Nombre = result.Rows[0]["nombre"].ToString();
            this.RFC = result.Rows[0]["rfc"].ToString();
            this.Telefono = result.Rows[0]["telefono"].ToString();
            this.Colonia = result.Rows[0]["colonia"].ToString();
            this.Calle = result.Rows[0]["calle"].ToString();
            this.NoInterior = Convert.ToInt32(result.Rows[0]["noInt"].ToString());
            this.NoExterior = Convert.ToInt32(result.Rows[0]["noExt"].ToString());
            this.CodPostal = Convert.ToInt32(result.Rows[0]["cPostal"].ToString());
            this.FechaRegistro = Convert.ToDateTime(result.Rows[0]["fechaRegistro"].ToString());
            this.Status =Convert.ToBoolean(result.Rows[0]["status"]);
            this.Precio = Convert.ToInt32(result.Rows[0]["idPrecio"]);
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