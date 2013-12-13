using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PaginaDeError : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Errores"] != null && Session["PaginaErrores"] != null)
        {
            lblError.Text = Session["Errores"].ToString();            
            Session.Remove("Errores");
            Session.Remove("PaginaErrores");
        }
    }
}
