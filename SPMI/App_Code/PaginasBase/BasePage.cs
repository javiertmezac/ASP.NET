using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Text;
using System.Globalization;
using System.Web.Resources;

/// <summary>
/// Summary description for PaginaBase
/// </summary>
public class BasePage : System.Web.UI.Page
{
    #region Constantes
    protected string K_ERROR_UPSERT = "Ha ocurrido un error al guardar los datos, intente de nuevo.";
    protected string K_ERROR_DELETE = "Ha ocurrido un error al eliminar el registro, intente de nuevo.";
    #endregion

    #region Eventos de la Pagina
    /// <summary>
    /// Sirve para cachar cuando el usuario cierra el browser y con esto eliminar al el id_usuario de la aplicacion
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        if (this.Request.Form["__EVENTTARGET"] != null && this.Request.Form["__EVENTARGUMENT"] != null)
        {
            if (this.Request.Form["__EVENTTARGET"].ToString().Equals("BasePage", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Session["BasePage"] = this;
            }
        }
        if (!this.Page.IsPostBack)
        {
 
        }
        base.OnLoad(e);
    }

    private static string[] aspNetFormElements = new string[] 
      { 
        "__EVENTTARGET",
        "__EVENTARGUMENT",
        "__VIEWSTATE",
        "__EVENTVALIDATION",
        "__VIEWSTATEENCRYPTED",
      };

    protected override void Render(HtmlTextWriter writer)
    {
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        base.Render(htmlWriter);
        string html = stringWriter.ToString();
        int formStart = html.IndexOf("<form");
        int endForm = -1;
        if (formStart >= 0)
            endForm = html.IndexOf(">", formStart);

        if (endForm >= 0)
        {
            StringBuilder viewStateBuilder = new StringBuilder();
            foreach (string element in aspNetFormElements)
            {
                int startPoint = html.IndexOf("<input type=\"hidden\" name=\"" + element + "\"");
                if (startPoint >= 0 && startPoint > endForm)
                {
                    int endPoint = html.IndexOf("/>", startPoint);
                    if (endPoint >= 0)
                    {
                        endPoint += 2;
                        string viewStateInput = html.Substring(startPoint, endPoint - startPoint);
                        html = html.Remove(startPoint, endPoint - startPoint);
                        viewStateBuilder.Append(viewStateInput).Append("\r\n");
                    }
                }
            }

            if (viewStateBuilder.Length > 0)
            {
                viewStateBuilder.Insert(0, "\r\n");
                html = html.Insert(endForm + 1, viewStateBuilder.ToString());
            }
        }

        writer.Write(html);
    }
    #endregion

    #region Contructor
    public BasePage()
    {
    }
    #endregion

    #region Attributos
    private SesionManager _sesionManager = null;
    #endregion

    #region Propiedades Publicas
    /// <summary>
    /// Apuntador al manejador de sesion
    /// </summary>
    public SesionManager SesionManager
    {
        get
        {
            if (this._sesionManager == null)
            {
                this._sesionManager = new SesionManager();
            }
            return this._sesionManager;
        }
    }  
    #endregion  

    #region Manejo de Errores
    /// <summary>
    /// Para mostrar una Exception no manejada en las paginas
    /// redirecciona al la pagina PaginaDeError.aspx
    /// </summary>
    protected override void OnError(EventArgs e)
    {
        //Informacion del error
        HttpContext ctx = HttpContext.Current;
        //Obtengo la Exception
        Exception exception = ctx.Server.GetLastError();
        Session.Add("Errores", exception.Message);
        Session.Add("PaginaErrores", this.Page.GetType().Name);

        this.Response.Redirect("~/PaginaDeError.aspx");
        ctx.Server.ClearError();

        base.OnError(e);
    }
    #endregion

    #region Metodos Publicos
    /// <summary>
    /// Redirecciona hacia el Login
    /// </summary>
    public void Login()
    {
        string path = !this.Request.ApplicationPath.EndsWith("/") ? this.Request.ApplicationPath + "/" : this.Request.ApplicationPath;
        path += "Default.aspx";
        this.Response.Redirect(path);
    }
    /// <summary>
    /// Obtiene un parametro enviado a traves de la Url
    /// </summary>
    /// <param name="param">nombre del parametro a obtener</param>
    /// <returns>valor del paramtro</returns>
    public string GetRequestParam(string param)
    {
        return this.Request.Params.Get(param);
    }
    /// <summary>
    /// Redirecciona a otra pagina
    /// </summary>
    /// <param name="url">pagina a la cual redireccionar</param>
    public void Redirect(string url)
    {
        this.Response.Redirect(url);
    }
    #endregion

    #region Cookies
    /// <summary>
    /// Constante del Idioma
    /// </summary>
    protected string K_COOKIE_IDIOMA
    {
        get { return "K_IDIOMA"; }
    }
    /// <summary>
    /// Crear una cookie
    /// </summary>
    /// <param name="nombre">Nombre de la cookie</param>
    /// <param name="valor">Valor de la cookie</param>
    protected void AgregaCookie(string nombre, string valor)
    {
        System.Web.HttpCookie cookie = new HttpCookie(nombre, valor);
        cookie.Value = valor;
        cookie.Expires = DateTime.Now.AddDays(30);
        this.Response.Cookies.Add(cookie);
    }
    protected string ObtenerCookie(string nombre)
    {
        System.Web.HttpCookie cookie = this.Request.Cookies.Get(nombre);
        if (cookie != null)
        {
            return cookie.Value;
        }
        return string.Empty;
    }
    #endregion

    #region Cultura
    private CultureInfo _cultureInfo = null;
    /// <summary>
    /// Culture Info
    /// </summary>
    protected CultureInfo CultureInfoX
    {
        get
        {
            if (this._cultureInfo == null)
            {
                if (string.IsNullOrEmpty(this.SesionManager.Idioma))
                {
                    this.SesionManager.Idioma = this.ObtenerCookie(this.K_COOKIE_IDIOMA) == "" ? System.Threading.Thread.CurrentThread.CurrentCulture.Name : this.ObtenerCookie(this.K_COOKIE_IDIOMA);
                }
                this._cultureInfo = CultureInfo.CreateSpecificCulture(this.SesionManager.Idioma);
            }
            return this._cultureInfo;
        }
        set { this._cultureInfo = value; }
    }
    protected void CambiarCultura(string cultura)
    {
        this.SesionManager.Idioma = cultura;
        this.CultureInfoX = CultureInfo.CreateSpecificCulture(cultura);
    }
    /// <summary>
    /// Inicializa la cultura par la pagina
    /// </summary>
    protected override void InitializeCulture()
    {
        this.InitializeCultureInfo(this.CultureInfoX);
        System.Threading.Thread.CurrentThread.CurrentCulture = this.CultureInfoX;
        System.Threading.Thread.CurrentThread.CurrentUICulture = this.CultureInfoX;

        base.InitializeCulture();
    }
    /// <summary>
    /// Inicializa las propiedades de la cultura, aplica el Uperrcase a la primer letra de los nombres
    /// para Meses y dias
    /// </summary>
    /// <param name="cultureInfo">Cultura a la cual aplicar el cambio</param>
    protected virtual void InitializeCultureInfo(System.Globalization.CultureInfo cultureInfo)
    {
        //Genero un arreglo con los meses abreviados, con la primer letra en mayuscula, despues lo asigno
        string[] abbreviatedMonthNames = new string[cultureInfo.DateTimeFormat.AbbreviatedMonthNames.Length];
        for (int i = 0; i < cultureInfo.DateTimeFormat.AbbreviatedMonthNames.Length; i++)
        {
            string abbreviatedMonthName = cultureInfo.DateTimeFormat.AbbreviatedMonthNames[i];
            if (!string.IsNullOrEmpty(abbreviatedMonthName))
            {
                abbreviatedMonthName = Char.ToUpper(abbreviatedMonthName[0]) + abbreviatedMonthName.Substring(1);
            }
            abbreviatedMonthNames[i] = abbreviatedMonthName;
        }
        cultureInfo.DateTimeFormat.AbbreviatedMonthNames = abbreviatedMonthNames;

        //Genero un arreglo con los meses, con la primer letra en mayuscula, despues lo asigno
        string[] monthNames = new string[cultureInfo.DateTimeFormat.MonthNames.Length];
        for (int i = 0; i < cultureInfo.DateTimeFormat.MonthNames.Length; i++)
        {
            string monthName = cultureInfo.DateTimeFormat.MonthNames[i];
            if (!string.IsNullOrEmpty(monthName))
            {
                monthName = Char.ToUpper(monthName[0]) + monthName.Substring(1);
            }
            monthNames[i] = monthName;
        }
        cultureInfo.DateTimeFormat.MonthNames = monthNames;

        //Genero un arreglo con los dias abreviados, con la primer letra en mayuscula, despues lo asigno
        string[] abbreviatedDayNames = new string[cultureInfo.DateTimeFormat.AbbreviatedDayNames.Length];
        for (int i = 0; i < cultureInfo.DateTimeFormat.AbbreviatedDayNames.Length; i++)
        {
            string abbreviatedDayName = cultureInfo.DateTimeFormat.AbbreviatedDayNames[i];
            if (!string.IsNullOrEmpty(abbreviatedDayName))
            {
                abbreviatedDayName = Char.ToUpper(abbreviatedDayName[0]) + abbreviatedDayName.Substring(1);
            }
            abbreviatedDayNames[i] = abbreviatedDayName;
        }
        cultureInfo.DateTimeFormat.AbbreviatedDayNames = abbreviatedDayNames;

        //Genero un arreglo con dias de la semana, con la primer letra en mayuscula, despues lo asigno
        string[] dayNames = new string[cultureInfo.DateTimeFormat.DayNames.Length];
        for (int i = 0; i < cultureInfo.DateTimeFormat.DayNames.Length; i++)
        {
            string dayName = cultureInfo.DateTimeFormat.DayNames[i];
            if (!string.IsNullOrEmpty(dayName))
            {
                dayName = Char.ToUpper(dayName[0]) + dayName.Substring(1);
            }
            dayNames[i] = dayName;
        }
        cultureInfo.DateTimeFormat.DayNames = dayNames;
        cultureInfo.Calendar.TwoDigitYearMax = System.DateTime.Now.Year + 25;

        //Este codigo funciona solo para la cultura es-MX
        //cultureInfo.DateTimeFormat.AbbreviatedMonthNames = new string[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic", "" };
        //cultureInfo.DateTimeFormat.MonthNames = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "" };
        //cultureInfo.DateTimeFormat.DayNames = new string[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        //cultureInfo.Calendar.TwoDigitYearMax = System.DateTime.Now.Year + 25;
    }
    #endregion

    #region Metodos para el manejo de Resource Text
    public string GetResourceText(string resourcekey)
    {
        System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(string));
        object result = null;
        try
        {
            //resourcekey = Utilerias.RemoveSpecialChars(resourcekey);
            result = resourceManager.GetString(resourcekey, this.CultureInfoX );
            if (result == null)
            {
                result = "";
            }
            return result.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
    /// <summary>
    /// Aplica un ResourceText a los controles de la pagina que implementen la interfaz
    /// </summary>
    /// <param name="parentControl">Control parentControl, a partir de donde se van a buscar los controles</param>
    public void SetResourceText(System.Web.UI.Control parentControl)
    {
        foreach (System.Web.UI.Control control in parentControl.Controls)
        {
            this.SetResourceText(control);
        }
        if (parentControl.GetType() == typeof(Label))
        {
            (parentControl as Label).Text = this.GetResourceText(parentControl.ID);
        }
        if (parentControl.GetType() == typeof(Button))
        {
            (parentControl as Button).Text = this.GetResourceText(parentControl.ID);
        }
        if (parentControl.GetType() == typeof(ImageButton))
        {
            (parentControl as ImageButton).ToolTip = this.GetResourceText(parentControl.ID);
        }
        if (parentControl.GetType() == typeof(LinkButton))
        {
            (parentControl as LinkButton).Text = this.GetResourceText(parentControl.ID);
        }
        if (parentControl.GetType() == typeof(GridView))
        {
            foreach (GridViewRow row in (parentControl as GridView).Rows)
            {
                if (row.FindControl("confirm") != null)
                {
                    //  ((row.FindControl("confirm")) as AjaxControlToolkit.ConfirmButtonExtender).ConfirmText = "delete item?";
                }
            }
            foreach (DataControlField header in (parentControl as GridView).Columns)
            {
                if (header.GetType().Name.Equals("1 `", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!header.HeaderStyle.CssClass.Equals("hiddencol", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string resource = "lbl" + ((header) as BoundField).DataField;
                        header.HeaderText = this.GetResourceText(resource).Replace(":", "");
                    }
                }
                if (header.GetType().Name.Equals("TemplateField", StringComparison.InvariantCultureIgnoreCase))
                {
                    string resource = "lbl" + ((header) as TemplateField).HeaderText;
                    header.HeaderText = this.GetResourceText(resource).Replace(":", "");
                }
            }
        }


    }
    #endregion
}
