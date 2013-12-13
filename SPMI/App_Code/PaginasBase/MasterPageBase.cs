using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MasterPage
/// </summary>
public class MasterPageBase : System.Web.UI.MasterPage
{
    #region Contructor
    public MasterPageBase()
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Pagina que contiene el MasterPage
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

    #region Eventos de la Pagina
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    #endregion

}
