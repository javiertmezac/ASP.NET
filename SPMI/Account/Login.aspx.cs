using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Login : Page
{
    SesionManager sesion = new SesionManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserName.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Users user = new Users();
        user.UserName = txtUserName.Text;
        user.Password = txtPassWord.Text;

        if (user.Login())
        {
            sesion.IDUsuario = user.ID;
            sesion.NombreUsuario = user.Name;

            Response.Redirect("Management.aspx");
        }
        else
        {
            Utilerias.MostrarAlert("Datos incorrectos, intente de nuevo.", this.Page);
        }

    }
}