using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for DBaccess
/// </summary>
public class DBaccess
{
    public static string mensajes = string.Empty;
    public static bool opStatus = false;

	public DBaccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static SqlCommand CreateSQLCommand(string nombreProcedimiento)
    {
        string conexionBD = DBConfiguration.DBConnection;
        SqlConnection cnx = new SqlConnection(conexionBD);
        SqlCommand cmd = cnx.CreateCommand();
        cmd.CommandText = nombreProcedimiento;
        cmd.CommandType = CommandType.StoredProcedure;
        return cmd;
    }
    /// <summary>
    /// Crear parametros para el comando
    /// </summary>
    public static SqlParameter ParameterAdd(SqlCommand cmd, string nombre, SqlDbType tipo, object valor)
    {
        SqlParameter para = new SqlParameter();
        para = cmd.CreateParameter();
        para.ParameterName = nombre;
        para.Value = valor; 
        para.SqlDbType = tipo;
        cmd.Parameters.Add(para);
        return para;
    }
    public static DataTable ExecuteSQLSelect(SqlCommand command)
    {
        DataTable tabla = new DataTable();
        try
        {
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            tabla.Load(reader);
            reader.Close();
            opStatus = true;
        }
        catch (Exception ex)
        {
            opStatus = false;
            mensajes = "Ha ocurrido un error al intentar establecer comunicación con el servidor. Contacte a su administrador de sistemas\nEl detalle del error es:"+ex.Data.ToString();
            SesionManager manejadorSesion = new SesionManager();            
        }
        finally
        {
            command.Connection.Close();
        }
        return tabla;
    }

    /// <summary>
    /// Ejecutar Scalar
    /// </summary>
    /// <param name="command">Comando SQL</param>
    /// <returns></returns>
    public static object EjecutarSQLScalar(SqlCommand command)
    {
        SesionManager manejadorSesion = new SesionManager();
        object valorObtenido = "";
        try
        {
            command.Connection.Open();
            valorObtenido = command.ExecuteScalar();
            opStatus = true;

            //   Bitacora.Bitacora_Create(7, manejadorSesion.IDUsuario, manejadorSesion.IDOpcionMenu, string.Format("{0} '{1}'", "Se ejecutó el Procedimiento", command.CommandText));
        }
        catch (Exception)
        {
            opStatus = false;
            mensajes = "Ha ocurrido un error al intentar establecer comunicación con el servidor. Contacte a su administrador de sistemas";

            // Bitacora.Bitacora_Create(8, manejadorSesion.IDUsuario, manejadorSesion.IDOpcionMenu, string.Format("{0} '{1}'. {2}", "Error al ejecutar el Procedimiento", command.CommandText, ex.Message));
        }
        finally
        {
            command.Connection.Close();
        }
        return valorObtenido;
    }
    /// <summary>
    /// Ejecutar NonQuery
    /// </summary>
    /// <param name="command">Comando SQL</param>
    /// <returns></returns>
    public static int EjecutarSQLNonQuery(SqlCommand command)
    {
        SesionManager manejadorSesion = new SesionManager();
        int renglonesAfectados = -1;
        try
        {
            command.Connection.Open();
            renglonesAfectados = command.ExecuteNonQuery();
            opStatus = true;
        }
        catch (Exception)
        {
            opStatus = false;
            mensajes = "Ha ocurrido un error al intentar establecer comunicación con el servidor. Contacte a su administrador de sistemas";
        }
        finally
        {
            command.Connection.Close();
        }
        return renglonesAfectados;
    }
}