using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Management : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label l=(Label)this.Master.FindControl("LoginView1").FindControl("nombreUsuario");
        l.Text = this.SesionManager.NombreUsuario.ToString();                
    }
}