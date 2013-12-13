using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Account_Empresas : BasePage
{
    #region variables privadas
    Empresa _EntidadNegocios = null;
    private Empresa EntidadNegocios
    {
        get
        {
            if (this._EntidadNegocios == null)
            {
                this._EntidadNegocios = new Empresa();
            }
            return this._EntidadNegocios;
        }
    }
    #endregion

    #region Eventos de la página
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.CargarFecha();
            this.SesionManager.IdEntidadNegocios = 0;
            this.CargarGrid();
        }                          
    }
    #endregion

    #region Métodos de botones
    protected void btnGuardar_Click(Object sender, EventArgs e)
    {
        try
        {
            this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(txtId.Text);
            this.EntidadNegocios.NoCliente = txtNumeroCliente.Text;
            this.EntidadNegocios.Nombre = txtNombre.Text;
            this.EntidadNegocios.RFC = txtRfc.Text;
            this.EntidadNegocios.Telefono = txtTelefono.Text;
            this.EntidadNegocios.Colonia = txtColonia.Text;
            this.EntidadNegocios.Calle = txtCalle.Text;
            this.EntidadNegocios.FechaRegistro = calFechaRegistro.SelectedDate;
            int noInt = 0;
            int.TryParse(txtNoInt.Text, out noInt);
            this.EntidadNegocios.NoInterior = noInt;
            int noExt = 0;
            int.TryParse(txtNoExt.Text, out noExt);
            this.EntidadNegocios.NoExterior = noExt;
            int CP = 0;
            int.TryParse(txtCodPostal.Text, out CP);
            this.EntidadNegocios.CodPostal = CP;
            this.EntidadNegocios.Status = txtStatus.Checked;
            this.EntidadNegocios.Precio = int.Parse(ddlTipoPrecio.SelectedValue);

            if (this.EntidadNegocios.UpSert())
            {
                mvCatalogo.ActiveViewIndex = 0;
                this.LimpiarControles();
                this.CargarGrid();
                this.Response.Redirect("Empresas.aspx");
            }
            else
            {
                Utilerias.MostrarAlert(this.K_ERROR_UPSERT, this.Page);
            }
            }catch(Exception)
            {
                Utilerias.MostrarAlert("Faltan campos por llenar!",this.Page);
            }        
    }
    protected void btnRegresar_Click(Object sender, EventArgs e)
    {
        this.CargarGrid();
        this.LimpiarControles();        
        mvCatalogo.ActiveViewIndex = 0;
        this.Response.Redirect("Empresas.aspx");
    }
    protected void btnAgregar_Click(Object sender, EventArgs e)
    {
        this.LimpiarControles();
        mvCatalogo.ActiveViewIndex = 1;
        txtId.Text = "0";
        
        //cargar informacino en el ddl de tipo de precio
        CargarInformacionDDL();
    }
    protected void btnFiltro_Click(Object sender, EventArgs e)
    {
        this.CargarGrid();
    }
    protected void btnExportar_Click(Object sender, EventArgs e)
    {
        gvCatalogo.AllowPaging = false;
        this.CargarGrid();
        string mensaje = "Catálogo de empresas"; 
        Utilerias.ExportarExcel(this.Response, gvCatalogo, mensaje);
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
    protected void btnLigarPedidos_Click(Object sender, EventArgs e)
    {
        this.Redirect("Pedido.aspx?idEmpresa=" + this.SesionManager.IdEntidadNegocios);
    }
    protected void btnLigarContactos_Click(Object sender, EventArgs e)
    {
        this.Redirect("ContactoEmpresa.aspx?idEmpresa=" + this.SesionManager.IdEntidadNegocios);
    }
    #endregion

    #region Métodos del GridView   
    protected void gvCatalogo_RowEditing(object sender, GridViewEditEventArgs e)
    {
         this.LimpiarControles();
         mvCatalogo.ActiveViewIndex = 1;
         CargarInformacionDDL();

        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(gvCatalogo.DataKeys[e.NewEditIndex].Value);
        if(this.EntidadNegocios.Load())
        {
            txtId.Text = this.EntidadNegocios.Id.ToString();
            txtNombre.Text = this.EntidadNegocios.Nombre;
            txtNumeroCliente.Text = this.EntidadNegocios.NoCliente;
            txtRfc.Text = this.EntidadNegocios.RFC;
            txtTelefono.Text = this.EntidadNegocios.Telefono;
            txtColonia.Text = this.EntidadNegocios.Colonia;
            txtCalle.Text = this.EntidadNegocios.Calle;
            txtNoInt.Text = this.EntidadNegocios.NoInterior.ToString();
            txtNoExt.Text = this.EntidadNegocios.NoExterior.ToString();
            txtCodPostal.Text = this.EntidadNegocios.CodPostal.ToString();
            calFechaRegistro.SelectedDate = this.EntidadNegocios.FechaRegistro;
            cambioFechaRegistro();
            txtStatus.Checked = this.EntidadNegocios.Status;
            ddlTipoPrecio.SelectedValue = this.EntidadNegocios.Precio.ToString();
            this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios;           
        }

        
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
#endregion

    #region Métodos privados

    private void CargarInformacionDDL()
    {
        PrecioGranel entidadPrecio = new PrecioGranel();
        DataTable dt = entidadPrecio.Lista("");

        Utilerias.LlenarDDL(ddlTipoPrecio, dt, "id", "precio");
    }

    private void CargarFecha()
    {
        this.txtFechaRegistro.Text = Utilerias.FechaToStringMes(calFechaRegistro.SelectedDate);
    }
        
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
        string[] cabecera = { "noCliente", "nombre", "rfc", "tel", "colonia", "calle", "noInt", "noExt", "cPostal", "fechaRegistro"};
        Utilerias.InicializaGV(cabecera, gvCatalogo);
    }
    private void LimpiarControles()
    {
        this.SesionManager.IdEntidadNegocios = 0 ;
      
        txtId.Text = string.Empty;
        txtNumeroCliente.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtRfc.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        txtColonia.Text = string.Empty;
        txtNoInt.Text = string.Empty;
        txtNoExt.Text = string.Empty;
        txtCodPostal.Text = string.Empty;
        txtFechaRegistro.Text = string.Empty;
        txtStatus.Checked = false;
        //ddlTipoPrecio.Items.Clear();
    }
    #endregion

    #region Calendar Events
    protected void calFechaRegistro_SelectionChanged(object sender, EventArgs e)
    {
        cambioFechaRegistro();
    }
    protected void cambioFechaRegistro()
    {
        string fecha = Utilerias.FechaToStringMes(calFechaRegistro.SelectedDate);
        this.PopupControlExtender1.Commit(fecha);
        this.txtFechaRegistro.Text = fecha;
    }   
    #endregion

    protected void btnVerContactos_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(gvCatalogo.DataKeys[gvRow.RowIndex].Value);
        this.Redirect("ContactoEmpresa.aspx?idEmpresa=" + this.SesionManager.IdEntidadNegocios);
    }
}