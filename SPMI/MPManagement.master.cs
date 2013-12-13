using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MPManagement : MasterPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.SesionManager.IDUsuario == 0)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
             Label l = (Label)this.FindControl("LoginView1").FindControl("nombreUsuario");
            l.Text = this.SesionManager.NombreUsuario.ToString();

         

            if (!this.Page.IsPostBack)
            {
             //   this.CargarMenus();
            }
        }
    }

    #region Eventos Botones
    protected void cerrarSesion(object sender, EventArgs e)
    {
        this.SesionManager.SessionValueClearAll();
        Session.Abandon();
        //FormsAuthentication.SignOut();
        HttpContext.Current.Response.Redirect("~/Default.aspx", true); /* tu pagina de logueo*/
    }
    #endregion
}
