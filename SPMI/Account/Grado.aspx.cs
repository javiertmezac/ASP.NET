using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_Grado : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.SesionManager.IdEntidadNegocios = 0;
            this.calFecha.SelectedDate = DateTime.Now;
            this.CargarFecha();
            this.CargarComboTipoGrado();
            this.CargarComboProfesor();
            this.cboProfesor.SelectedValue = this.GetRequestParam("idProfesor");
            this.CargarGrid();
        }
    }

    #region variables privadas
    Grados _EntidadNegocios = null;
    private Grados EntidadNegocios
    {
        get
        {
            if (this._EntidadNegocios == null)
            {
                this._EntidadNegocios = new Grados();
            }
            return this._EntidadNegocios;
        }
    }
    #endregion

    #region Eventos de DDL
    protected void cboProfesor_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  this.CargarComboProfesor();
        this.CargarGrid();
    }
    #endregion

    #region Métodos privados
    private void CargarFecha()
    {
        this.txtFecha.Text = Utilerias.FechaToStringMes(calFecha.SelectedDate);
    }
    private void CargarComboProfesor()
    {
        Empresa empresa = new Empresa();
        Utilerias.LlenarDDL(cboProfesor, empresa.Lista(""), "id", "nombre");
    }
    private void CargarComboTipoGrado()
    {
        TipoGrado tipoGrado = new TipoGrado();
        Utilerias.LlenarDDL(cboTipoGrado, tipoGrado.Lista(""), "id", "descripcion");
    }
    private void CargarGrid()
    {
        int idProfesor = Convert.ToInt32(cboProfesor.SelectedValue);
        DataView lista = this.EntidadNegocios.Lista(idProfesor).DefaultView;
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
        string[] cabecera = { "id", "profesor", "grado", "descripcion", "cedula", "institucion", "fecha"};
        Utilerias.InicializaGV(cabecera, gvCatalogo);
    }
    private void LimpiarControles()
    {
        this.SesionManager.IdEntidadNegocios = this.EntidadNegocios.Id = 0;
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
            txtGrado.Text = this.EntidadNegocios.Grado;
            txtCedula.Text = this.EntidadNegocios.Cedula;
            txtInstitucion.Text = this.EntidadNegocios.Institucion;
            cboTipoGrado.SelectedValue = this.EntidadNegocios.TipoGrado.ToString();
            calFecha.SelectedDate = this.EntidadNegocios.Fecha;
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

    #region Eventos de botones
    protected void btnGuardar_Click(Object sender, EventArgs e)
    {
        try{
        this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios;
        this.EntidadNegocios.Profesor = Convert.ToInt32(cboProfesor.SelectedValue);
        this.EntidadNegocios.Grado = txtGrado.Text;
        this.EntidadNegocios.TipoGrado = Convert.ToInt32(cboTipoGrado.SelectedValue);
        this.EntidadNegocios.Cedula = txtCedula.Text;
        this.EntidadNegocios.Institucion = txtInstitucion.Text;
        this.EntidadNegocios.Fecha =calFecha.SelectedDate;
        if (this.EntidadNegocios.UpSert())
        {
            mvCatalogo.ActiveViewIndex = 0;
            this.LimpiarControles();
            this.CargarGrid();
            //this.Redirect("Grado.aspx?idProfesor=" + this.SesionManager.IdEntidadNegocios);
            this.Redirect("Grado.aspx?idProfesor=" + this.EntidadNegocios.Profesor);
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
        string mensaje = "Grados del profesor  " + cboProfesor.SelectedValue; 
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
       // this.Redirect("Grado.aspx?idProfesor=" + this.SesionManager.IdEntidadNegocios);
        this.Redirect("Grado.aspx?idProfesor=" + this.EntidadNegocios.Profesor);
        mvCatalogo.ActiveViewIndex = 0;
    }
    #endregion   

    #region Calendar Events
    protected void calFecha_SelectionChanged(object sender, EventArgs e)
    {
        string fecha = Utilerias.FechaToStringMes(calFecha.SelectedDate);
        this.PopupControlExtender1.Commit(fecha);
        this.txtFecha.Text = fecha;

    }
    #endregion
}