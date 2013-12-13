using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_Curso : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.SesionManager.IdEntidadNegocios = 0;
            this.CargarComboContacto();
            this.CargarFecha();
            this.CargarComboEmpresa();
            this.cboEmpresa.SelectedValue = this.GetRequestParam("idEmpresa");
            this.CargarGrid(false);
        }
    }

    #region variables privadas
    ContactoEmpresa _EntidadNegocios = null;
    private ContactoEmpresa EntidadNegocios
    {
        get
        {
            if (this._EntidadNegocios == null)
            {
                this._EntidadNegocios = new ContactoEmpresa();
            }
            return this._EntidadNegocios;
        }
    }
    #endregion

    #region Eventos de DDL
    protected void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {        
        this.CargarGrid(false);
    }
    #endregion

    #region Métodos privados
    private void CargarFecha()
    {
        this.txtFechaInicio.Text = Utilerias.FechaToStringMes(calFechaInicio.SelectedDate);
    }
    private void CargarComboEmpresa()
    {
        Empresa empresa = new Empresa();
        Utilerias.LlenarDDL(cboEmpresa, empresa.Lista(""), "id", "nombre");
    }
    private void CargarComboContacto()
    {
        Contacto contacto = new Contacto();
        Utilerias.LlenarDDL(cboContacto, contacto.ListaNombreCompleto(""), "id", "nombre");
    }    
    private void CargarGrid(bool filtro)
    {
        int idEmpresa = Convert.ToInt32(cboEmpresa.SelectedValue);
        string pista = txtFiltro.Text;
        DataView lista;
        if (filtro)
        {
             lista = this.EntidadNegocios.ListaFiltro(idEmpresa,pista).DefaultView;
        }
        else
        {
             lista = this.EntidadNegocios.Lista(idEmpresa).DefaultView;
        }
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
        string[] cabecera = { "id", "nombre", "fechaRegistro", "celular", "radio"};
        Utilerias.InicializaGV(cabecera, gvCatalogo);
    }

    private void LimpiarControles()
    {
        this.SesionManager.IdEntidadNegocios = this.EntidadNegocios.Id = 0;
        //txtId.Text = string.Empty;
        //txtProfesor.Text = string.Empty;
        //txtGrado.Text = string.Empty;
        //txtTipoGrado.Text = string.Empty;
        //txtCedula.Text = string.Empty;
        //txtInstitucion.Text = string.Empty;
        //txtFecha.Text = string.Empty;        
    }
    #endregion

    #region Métodos del GridView
    protected void gvCatalogo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.LimpiarControles();

        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(gvCatalogo.DataKeys[e.NewEditIndex].Value);
        if (this.EntidadNegocios.Load())
        {
            calFechaInicio.SelectedDate = this.EntidadNegocios.FechaInicio;
            cambioFechaInicio();
            txtStatus.Checked = this.EntidadNegocios.Status;
            cboContacto.SelectedValue = this.EntidadNegocios.IdContacto.ToString() ;         
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
        CargarGrid(false);
    }
    protected void gvCatalogo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCatalogo.PageIndex = e.NewPageIndex;
        this.CargarGrid(false);
    }
    protected virtual void btnEliminar_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios = Convert.ToInt32(gvCatalogo.DataKeys[gvRow.RowIndex].Value);

        if (this.EntidadNegocios.Delete())
        {
            this.CargarGrid(false);
        }
        else
        {
            Utilerias.MostrarAlert(this.K_ERROR_DELETE, this.Page);
        }
    }
    #endregion

    #region Eventos de botones
    protected void btnGuardar_Click(Object sender, EventArgs e)
    {
        try{
        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios;
        this.EntidadNegocios.IdContacto = Convert.ToInt32(cboContacto.SelectedValue);
        this.EntidadNegocios.IdEmpresa = Convert.ToInt32(cboEmpresa.SelectedValue);
        this.EntidadNegocios.FechaInicio = calFechaInicio.SelectedDate;
        this.EntidadNegocios.Status = Convert.ToBoolean(txtStatus.Checked);
             
        if (this.EntidadNegocios.UpSert())
        {
            mvCatalogo.ActiveViewIndex = 0;
            this.LimpiarControles();
            this.CargarGrid(false);
            this.Redirect("ContactoEmpresa.aspx?idEmpresa=" + this.EntidadNegocios.IdEmpresa);
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
        this.CargarGrid(true);
    }
    protected void btnExportar_Click(Object sender, EventArgs e)
    {
        gvCatalogo.AllowPaging = false;
        this.CargarGrid(false);
        string mensaje = "Contactos de la empresa  " + cboEmpresa.SelectedValue;
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
        this.Redirect("ContactoEmpresa.aspx?idEmpresa=" + this.EntidadNegocios.IdEmpresa);
        mvCatalogo.ActiveViewIndex = 0;
    }
    #endregion

    #region Calendar Events
    protected void calFechaInicio_SelectionChanged(object sender, EventArgs e)
    {
        cambioFechaInicio();
    }
    protected void cambioFechaInicio()
    {
        string fecha = Utilerias.FechaToStringMes(calFechaInicio.SelectedDate);
        this.PopupControlExtender1.Commit(fecha);
        this.txtFechaInicio.Text = fecha;
    }
    #endregion
}