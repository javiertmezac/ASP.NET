using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


#region interface User Control
public interface IUserControlBase
{
    void FirstLoad();
}
#endregion

/// <summary>
/// Summary description for UserControlBase
/// </summary>
public class UserControlBase : System.Web.UI.UserControl, IUserControlBase
{
    #region Contructor
    public UserControlBase()
	{
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Pagina que contiene el UserControl
    /// </summary>
    public new BasePage Page
    {
        get { return (BasePage)base.Page; }
    }
    /// <summary>
    /// Apuntador al manejador de sesion
    /// </summary>
    public SesionManager SesionManager
    {
        get { return this.Page.SesionManager; }
    }
    #endregion

    #region Implementacion de la Inferface IUserControlBase
    /// <summary>
    /// Metodo para Ejecutar al Cargar el UserControl
    /// </summary>
    public void FirstLoad()
    {
        this.OnFirstLoad();
    }
    #endregion

    #region Metodos Protegidos
    /// <summary>
    /// Metodo Ejecutado al Cargar el UserControl
    /// </summary>
    protected virtual void OnFirstLoad()
    {
    }
    /// <summary> 
    /// Obtener el mensaje de Catalogos
    /// </summary>
    protected virtual string GetMsgCatalogos()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "Catálogos." : this.Page.GetResourceText("lblCatalogos");
    }
    /// <summary> 
    /// Obtener el mensaje del registro agregado
    /// </summary>
    protected virtual string GetMsgAddItem()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "El registro ha sido agregado." : this.Page.GetResourceText("msgAddItem");
    }
    /// <summary> 
    /// Obtener el mensaje del registro NO agregado
    /// </summary>
    protected virtual string GetMsgAddItemError()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "El registro NO pudo ser agregado." : this.Page.GetResourceText("msgAddItemError");
    }
    /// <summary> 
    /// Obtener el mensaje del coddigo ya existe
    /// </summary>
    protected virtual string GetMsgCodigoExiste()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "No se pudo guardar el Registro. El Código ya existe." : this.Page.GetResourceText("msgCodigoExiste");
    }    
    /// <summary> 
    /// Obtener el mensaje del registro actualizado
    /// </summary>
    protected virtual string GetMsgUpdateItem()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "El registro ha sido actualizado." : this.Page.GetResourceText("msgUpdateItem");
    }
    /// <summary> 
    /// Obtener el mensaje del registro NO actualizado
    /// </summary>
    protected virtual string GetMsgUpdateItemError()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "El registro NO pudo ser actualizado." : this.Page.GetResourceText("msgUpdateItemError");
    }
    /// <summary> 
    /// Obtener el mensaje del registro eliminado
    /// </summary>
    protected virtual string GetMsgDeleteItem()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "El registro ha sido eliminado." : this.Page.GetResourceText("msgDeleteItem");
    }
    /// <summary> 
    /// Obtener el mensaje del registro NO eliminado
    /// </summary>
    protected virtual string GetMsgDeleteItemError()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "El registro NO pudo ser eliminanado." : this.Page.GetResourceText("msgDeleteItemError");
    }
    /// <summary> 
    /// Obtener el mensaje del registro NO eliminado
    /// </summary>
    protected virtual string GetMsgDependencyError()
    {
        return this.SesionManager.Idioma.Equals("es-MX") ? "Error al eliminar el registro. Existen dependencias hacias otros catálogos." : this.Page.GetResourceText("msgDependencyError");
    }
    /// <summary> 
    /// Obtener la traduccion del Resource
    /// </summary>
    protected virtual string GetResourceText(string resourceKey)
    {
        return this.Page.GetResourceText(resourceKey);
    }
    /// <summary> 
    /// Aplica un ResourceText a los controles de la pagina que implementen la interfaz
    /// </summary>
    protected virtual void SetResourceText(System.Web.UI.Control parentControl)
    {
        this.Page.SetResourceText(parentControl);
    }
    /// <summary> 
    /// Aplica un ResourceText a un Grid que esta en un Hoover
    /// </summary>
    protected virtual void SetResourceTextGridHoover(GridView gv, GridViewRow row)
    {
        if (row.RowType == DataControlRowType.Header)
        {
            if (!this.SesionManager.Idioma.Equals("es-MX", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int x = 0; x < row.Cells.Count; x++)
                {
                    string resource = "lbl" + ((gv.Columns[x] as DataControlField) as BoundField).DataField;
                    row.Cells[x].Text = this.GetResourceText(resource);
                }
            }
        }
    }
    #endregion

}


