using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_Carrera : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.SesionManager.IdEntidadNegocios = 0;           
            this.CargarGrid();
        }
    }

    #region variables privadas
    Carrera _EntidadNegocios = null;
    private Carrera EntidadNegocios
    {
        get
        {
            if (this._EntidadNegocios == null)
            {
                this._EntidadNegocios = new Carrera();
            }
            return this._EntidadNegocios;
        }
    }
    #endregion

    #region Métodos privados  
    private void CargarGrid()
    {
        DataView lista = this.EntidadNegocios.Lista(txtFiltro.Text).DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            lista.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }

        gvCatalogo.DataSource = lista.ToTable();
        gvCatalogo.DataBind();

        if (lista.ToTable().Rows.Count == 0)
        {
            this.InicializaGrid();
        }
    }
    private void InicializaGrid()
    {
        string[] cabecera = { "id", "clave", "nombre" };
        Utilerias.InicializaGV(cabecera, gvCatalogo);
    }
    private void LimpiarControles()
    {
        this.SesionManager.IdEntidadNegocios = this.EntidadNegocios.Id = 0;
        txtNombre.Text = string.Empty;
        txtClave.Text = string.Empty;
    }
    #endregion

    #region Eventos de botones
    protected void btnGuardar_Click(Object sender, EventArgs e)
    {
        try{
        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios;
        this.EntidadNegocios.Nombre = txtNombre.Text;
        this.EntidadNegocios.Clave = txtClave.Text;
        if (this.EntidadNegocios.UpSert())
        {
            mvCatalogo.ActiveViewIndex = 0;
            this.LimpiarControles();
            this.CargarGrid();
            this.Redirect("Carrera.aspx");
        }
        else
        {
            Utilerias.MostrarAlert(this.K_ERROR_UPSERT, this.Page);
        }
        }
        catch (Exception)
        {
            Utilerias.MostrarAlert("Faltan campos por llenar!", this.Page);
        }
    }
    protected void btnFiltro_Click(Object sender, EventArgs e)
    {
        this.CargarGrid();
    }
    protected void btnExportar_Click(Object sender, EventArgs e)
    {
        gvCatalogo.AllowPaging = false;
        this.CargarGrid();
        string mensaje = "Carreras";
        Utilerias.ExportarExcel(this.Response, gvCatalogo, mensaje);
    }
    protected void btnAgregar_Click(Object sender, EventArgs e)
    {
        this.LimpiarControles();
        mvCatalogo.ActiveViewIndex = 1;
    }
    protected void btnRegresar_Click(Object sender, EventArgs e)
    {
        this.LimpiarControles();
        this.Redirect("Carrera.aspx");
        mvCatalogo.ActiveViewIndex = 0;
    }
    #endregion   

    #region Métodos del GridView
    protected void gvCatalogo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.LimpiarControles();

        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(gvCatalogo.DataKeys[e.NewEditIndex].Value);
        if (this.EntidadNegocios.Load())
        {
            txtNombre.Text = this.EntidadNegocios.Nombre;
            txtClave.Text = this.EntidadNegocios.Clave;          
            //   this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios;
        }

        mvCatalogo.ActiveViewIndex = 1;
    }
    protected void gvCatalogo_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = "";
        if (ViewState["sortexpression"] != null)
        {
            sortExpression = ViewState["sortexpression"].ToString();
        }
        ViewState["sortexpression"] = e.SortExpression;
        if (sortExpression != ViewState["sortexpression"].ToString())
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"] == null)
            {
                ViewState["sortdirection"] = "asc";
            }
            else
            {
                if (ViewState["sortdirection"].ToString() == "asc")
                {
                    ViewState["sortdirection"] = "desc";
                }
                else
                {
                    ViewState["sortdirection"] = "asc";
                }
            }
        }
        gvCatalogo.PageIndex = 0;
        CargarGrid();
    }
    protected void gvCatalogo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCatalogo.PageIndex = e.NewPageIndex;
        this.CargarGrid();
    }
    protected virtual void btnEliminar_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(gvCatalogo.DataKeys[gvRow.RowIndex].Value);

        if (this.EntidadNegocios.Delete())
        {
            this.CargarGrid();
        }
        else
        {
            Utilerias.MostrarAlert(this.K_ERROR_DELETE, this.Page);
        }
    }
    #endregion

}