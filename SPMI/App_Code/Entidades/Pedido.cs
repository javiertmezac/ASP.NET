using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de Pedido
/// </summary>
public class Pedido
{
	public Pedido()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    #region Atributos y propiedades
    string K_PREFIJO = "Pedido";
    int _id = 0;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    string _noCliente = string.Empty;

    public string NoCliente
    {
        get { return _noCliente; }
        set { _noCliente = value; }
    }
    int _idChofer = 0;

    public int IdChofer
    {
        get { return _idChofer; }
        set { _idChofer = value; }
    }
    private DateTime _fechaCreacion = DateTime.Now;

    public DateTime FechaCreacion
    {
        get { return _fechaCreacion; }
        set { _fechaCreacion = value; }
    }
    private DateTime _fechaEntregar;

    public DateTime FechaEntregar
    {
        get { return _fechaEntregar; }
        set { _fechaEntregar = value; }
    }
    string _notas = string.Empty;

    public string Notas
    {
        get { return _notas; }
        set { _notas = value; }
    }
    bool _esAtendido = true;

    public bool EsAtendido
    {
        get { return _esAtendido; }
        set { _esAtendido = value; }
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
    /// Agrega registros de pedidos
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool UpSert()
    {
        try
        {
            SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_UPSERT");
            DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);
            DBaccess.ParameterAdd(cmd, "@noCliente", SqlDbType.VarChar, this.NoCliente);
            DBaccess.ParameterAdd(cmd, "@idChofer", SqlDbType.VarChar, this.IdChofer);
            DBaccess.ParameterAdd(cmd, "@fechaCreacion", SqlDbType.VarChar, this.FechaCreacion);
            DBaccess.ParameterAdd(cmd, "@fechaEntregar", SqlDbType.VarChar, this.FechaEntregar);
            DBaccess.ParameterAdd(cmd, "@notas", SqlDbType.VarChar, this.Notas);
            DBaccess.ParameterAdd(cmd, "@esAtendido", SqlDbType.VarChar, this.EsAtendido);
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
        catch (Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// Carga un pedido
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool Load()
    {
        try
        {
            SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_SELECT");
            DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);

            DataTable result = DBaccess.ExecuteSQLSelect(cmd);
            if (result.Rows.Count > 0)
            {
                this.Id = Convert.ToInt32(result.Rows[0]["id"]);
                this.NoCliente = result.Rows[0]["noCliente"].ToString();
                this.IdChofer = Convert.ToInt32(result.Rows[0]["idChofer"]);
                this.FechaCreacion = Convert.ToDateTime(result.Rows[0]["fechaCreacion"]);
                this.FechaEntregar = Convert.ToDateTime(result.Rows[0]["fechaEntregar"]);
                this.Notas = result.Rows[0]["notas"].ToString();
                this.EsAtendido = Convert.ToBoolean(result.Rows[0]["esAtendido"]);
                this.Status = Convert.ToBoolean(result.Rows[0]["status"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    /// <summary>
    /// Elimina pedidos registrados
    /// </summary>
    /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
    public bool Delete()
    {
        try
        {
            SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_DELETE");
            DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this.Id);
            return DBaccess.EjecutarSQLNonQuery(cmd) > 0;
        }
        catch (Exception)
        {
            
            throw;
        }        
    }
    /// <summary>
    /// Lista de pedidos registrados
    /// </summary>
    public DataTable Lista(string pista)
    {
        try
        {
            SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIJO + "_LIST");
            DBaccess.ParameterAdd(cmd, "@pista", SqlDbType.VarChar, pista);
            return DBaccess.ExecuteSQLSelect(cmd);
        }
        catch (Exception)
        {
            
            throw;
        }        
    }
    #endregion
}