using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Sesion
/// </summary>
public class SesionManager
{
	public SesionManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Constantes
    private const string K_ID_OBJETO = "idObjecto";
    private const string K_ID_USUARIO = "idUsuario";
    private const string K_NOMBRE_USUARIO = "NombreUsuario";
    private const string K_ID_OPCION_MENU = "idOpcion";
    private const string K_ID_MENU = "idMenu";
    private const string K_IDIOMA = "idioma";
    #endregion

    #region Private Attributes
    private System.Web.SessionState.HttpSessionState _session = null;
    #endregion       

    #region Public Properties
    public string Idioma
    {
        //get { return this.SessionStringGet(K_IDIOMA, "es-MX"); }
        //get { return this.SessionStringGet(K_IDIOMA, System.Threading.Thread.CurrentThread.CurrentCulture.Name); }
        get { return this.SessionStringGet(K_IDIOMA, ""); }
        set { this.SessionStringSet(K_IDIOMA, value); }
    }
    /// <summary>
    /// Identificador del Objeto (tabla)
    /// </summary>
    public int IdEntidadNegocios
    {
        get { return this.SessionIntGet(K_ID_OBJETO, 0); }
        set { this.SessionIntSet(K_ID_OBJETO, value); }
    }
    /// <summary>
    /// Identificador del ID del Usuario
    /// </summary>
    public int IDUsuario
    {
        get { return this.SessionIntGet(K_ID_USUARIO, 0); }
        set { this.SessionIntSet(K_ID_USUARIO, value); }
    }
    /// <summary>
    /// Nombre del Usuario
    /// </summary>
    public string NombreUsuario
    {
        get { return this.SessionStringGet(K_NOMBRE_USUARIO, string.Empty); }
        set { this.SessionStringSet(K_NOMBRE_USUARIO, value); }
    }
    /// <summary>
    /// Identificador de la Opcion de Menu Accedida
    /// </summary>
    public int IDOpcionMenu
    {
        get { return this.SessionIntGet(K_ID_OPCION_MENU, 0); }
        set { this.SessionIntSet(K_ID_OPCION_MENU, value); }
    }
    /// <summary>
    /// Identificador del Menu Accedido
    /// </summary>
    public int IDMenu
    {
        get { return this.SessionIntGet(K_ID_MENU, 0); }
        set { this.SessionIntSet(K_ID_MENU, value); }
    }
    /// <summary>
    /// Idioma (Cultura) de la pagina
    /// </summary>
    #endregion

    #region Encapsulación de HttpSessionState Session
    /// <summary>
    /// Apunta hacia el manejador de sesion de una aplicacion web
    /// </summary>
    protected System.Web.SessionState.HttpSessionState CurrentSession
    {
        get { return System.Web.HttpContext.Current.Session; }
    }
    /// <summary>
    /// Apuntador hacia el manejador de sesion asignado a esta instancia
    /// </summary>
    /// <remarks>Es posible que este manejador sea el mismo que this.CurrentSession</remarks>
    protected System.Web.SessionState.HttpSessionState Session
    {
        get { return this._session == null ? this.CurrentSession : this._session; }
    }
    #endregion

    #region Manejo de valores de Sesion
    /// <summary>
    /// Obtiene el valor de la sesion indicado
    /// </summary>
    /// <param name="Nombre">Nombre de la llave de sesion a buscar</param>
    /// <returns>Valor encontrado, de no exister regresa null</returns>
    public object SessionValueGet(string Nombre)
    {
        return ((this.Session == null) ? null : this.Session[Nombre]);
    }
    /// <summary>
    /// Escribe el valor de sesion indicado
    /// </summary>
    /// <param name="Nombre">Nombre de la LLave de sesion</param>
    /// <param name="Valor">Valor a escribir</param>
    public void SessionValueSet(string Nombre, object Valor)
    {
        if (this.Session != null)
        {
            this.Session[Nombre] = Valor;
        }
    }
    /// <summary>
    /// Averigua si existe el valor de Sesion
    /// </summary>
    /// <param name="Nombre">Nombre de la LLave de sesion</param>
    /// <returns>True si el valor indicado por 'Nombre' existe</returns>
    public bool SessionValueExists(string Nombre)
    {
        return ((this.Session != null) && (this.Session[Nombre] != null));
    }
    /// <summary>
    /// Borra una variable especificada de sesion
    /// </summary>
    /// <param name="Nombre">Nombre de la variable a borrar</param>
    public void SessionValueClear(string Nombre)
    {
        if (this.Session != null)
        {
            this.Session.Remove(Nombre);
        }
    }
    /// <summary>
    /// Borra una todas las variable de sesion
    /// </summary>
    public void SessionValueClearAll()
    {
        this.SessionValueClear(K_ID_OBJETO);
        this.SessionValueClear(K_ID_USUARIO);
        this.SessionValueClear(K_NOMBRE_USUARIO);
        this.SessionValueClear(K_ID_OPCION_MENU);
        this.SessionValueClear(K_ID_MENU);
    }
    #endregion

    #region Manejo de valores de Sesion por tipo de dato
    /// <summary>
    /// Obtiene un valor entero de la sesion
    /// </summary>
    /// <param name="Nombre">Nombre de la variable de sesion</param>
    /// <returns>El valor entero de la variable de sesion especificada</returns>
    public int SessionIntGet(string Nombre)
    {
        return this.SessionIntGet(Nombre, 0);
    }
    /// <summary>
    /// Obtiene un valor entero de la sesion
    /// </summary>
    /// <param name="Nombre">Nombre de la variable de sesion</param>
    /// <param name="Default">Valor default en caso de no existir el valor en Sesion</param>
    /// <returns>El valor entero de la variable de sesion especificada</returns>
    public int SessionIntGet(string Nombre, int Default)
    {
        return (this.SessionValueExists(Nombre) ? (int)this.SessionValueGet(Nombre) : Default);
    }
    /// <summary>
    /// Pone un valor entero a la variable de sesion especificada
    /// </summary>
    /// <param name="Nombre">Nombre de la variable</param>
    /// <param name="Valor">Valor entero de la variable</param>
    public void SessionIntSet(string Nombre, int Valor)
    {
        this.SessionValueSet(Nombre, Valor);
    }
    /// <summary>
    /// Obtiene un valor string de la sesion
    /// </summary>
    /// <param name="Nombre">Nombre de la variable de sesion</param>
    /// <returns>El valor string de la variable de sesion especificada</returns>
    public string SessionStringGet(string Nombre)
    {
        return this.SessionStringGet(Nombre, string.Empty);
    }
    /// <summary>
    /// Obtiene un valor string de la sesion
    /// </summary>
    /// <param name="Nombre">Nombre de la variable de sesion</param>
    /// <param name="Default">Valor default en caso de no existir el valor en Sesion</param>
    /// <returns>El valor string de la variable de sesion especificada</returns>
    public string SessionStringGet(string Nombre, string Default)
    {
        return (this.SessionValueExists(Nombre) ? this.SessionValueGet(Nombre).ToString() : Default);
    }
    /// <summary>
    /// Pone un valor string a la variable de sesion especificada
    /// </summary>
    /// <param name="Nombre">Nombre de la variable</param>
    /// <param name="Valor">Valor string de la variable</param>
    public void SessionStringSet(string Nombre, string Valor)
    {
        this.SessionValueSet(Nombre, Valor);
    }
    #endregion

}