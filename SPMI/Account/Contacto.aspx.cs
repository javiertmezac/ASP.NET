using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_Contacto : BasePage
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
    Contacto _EntidadNegocios = null;
    private Contacto EntidadNegocios
    {
        get
        {
            if (this._EntidadNegocios == null)
            {
                this._EntidadNegocios = new Contacto();
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
        string[] cabecera = { "id", "nombre", "apellidoP","apellidoM","cell","radio","email","status" };
        Utilerias.InicializaGV(cabecera, gvCatalogo);
    }
    private void LimpiarControles()
    {
        this.SesionManager.IdEntidadNegocios = this.EntidadNegocios.Id = 0;

        txtNombre.Text = string.Empty;
        txtApPaterno.Text = string.Empty;
        txtApMaterno.Text = string.Empty;
        txtCelular.Text = string.Empty;
        txtRadio.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtStatus.Checked = false;
    }
    #endregion

    #region Eventos de botones
    protected void btnGuardar_Click(Object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                this.EntidadNegocios.Id = this.SesionManager.IdEntidadNegocios;
                this.EntidadNegocios.Nombre = txtNombre.Text;
                this.EntidadNegocios.ApPaterno = txtApPaterno.Text;
                this.EntidadNegocios.ApMaterno = txtApMaterno.Text;
                this.EntidadNegocios.Celular = txtCelular.Text;
                this.EntidadNegocios.Radio = txtRadio.Text;
                this.EntidadNegocios.Email = txtEmail.Text;
                this.EntidadNegocios.Status = txtStatus.Checked;

                if (this.EntidadNegocios.UpSert())
                {
                    mvCatalogo.ActiveViewIndex = 0;
                    this.LimpiarControles();
                    this.CargarGrid();
                    this.Redirect("Contacto.aspx");
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
        else
        {
            Utilerias.MostrarAlert("Faltan campos por llenar", this.Page);
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
        string mensaje = "Contactos";
        Utilerias.ExportarExcel(this.Response, gvCatalogo, mensaje);
    }

    protected void btnAgregar_Click(Object sender, EventArgs e)
    {
        this.LimpiarControles();
        mvCatalogo.ActiveViewIndex = 1;
        txtId.Text = "0";
    }

    protected void btnRegresar_Click(Object sender, EventArgs e)
    {
        this.LimpiarControles();
        this.Redirect("Contacto.aspx");
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
            txtId.Text = this.EntidadNegocios.Id.ToString() ;
            txtNombre.Text = this.EntidadNegocios.Nombre;
            txtApPaterno.Text = this.EntidadNegocios.ApPaterno;
            txtApMaterno.Text = this.EntidadNegocios.ApMaterno;
            txtCelular.Text = this.EntidadNegocios.Celular;
            txtRadio.Text = this.EntidadNegocios.Radio;
            txtEmail.Text = this.EntidadNegocios.Email;
            txtStatus.Checked = this.EntidadNegocios.Status;
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