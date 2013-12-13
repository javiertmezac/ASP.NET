using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Collections;
using System.Linq;



/// <summary>
/// Summary description for Utilerias
/// </summary>
public static class Utilerias
{
    
    #region Utilerias para llenar e inicializar combos y grids

    public static void LlenarDDL(DropDownList ddlT, DataTable dtT, string valueField, string textField)
    {
        ddlT.DataSource = dtT;
        ddlT.DataValueField = valueField;
        ddlT.DataTextField = textField;
        ddlT.DataBind();
        ddlT.SelectedIndex = -1;
        if (ddlT.Items.Count == 0)

            InicializaDDL(ddlT);
    }
    public static void InicializaDDL(DropDownList ddlT)
    {
        ListItem lista = new ListItem("No Existe Información. . .", "0");
        ddlT.Items.Add(lista);
    }   

    public static void LlenarDDL(DropDownList ddl, DataTable table, string textField, string valueField, string valorInicial)
    {
        ListItem itemCero = new ListItem("Seleccione ...", valorInicial);
        ddl.DataSource = table;
        int cant = table.Rows.Count;
        ddl.DataTextField = textField;
        ddl.DataValueField = valueField;
        ddl.DataBind();
        ddl.Items.Insert(0, itemCero);
        ddl.SelectedIndex = 0;
    }

    public static void LlenarDdlNS(DropDownList DdlTemp, DataTable dtConsulta, string texto, string valor)
    {
        DdlTemp.DataSource = dtConsulta;
        DdlTemp.DataTextField = texto;
        DdlTemp.DataValueField = valor;
        DdlTemp.DataBind();
    }

    public static void InicializarddlHoras(DropDownList ddl, int rango)
    {
        ddl.Items.Clear();
        for (int i = 0; i <= 23; i++)
            for (int j = 0; j < 60; j = j + rango)
                if (i < 10)
                    if (j == 0)
                        ddl.Items.Add("0" + i.ToString() + ":" + "0" + j.ToString());
                    else
                        ddl.Items.Add("0" + i.ToString() + ":" + j.ToString());
                else
                    if (j == 0)
                        ddl.Items.Add(i.ToString() + ":" + "0" + j.ToString());
                    else
                        ddl.Items.Add(i.ToString() + ":" + j.ToString());
    }

    public static void InicializaGV(string[] cabecera, GridView gvTemp)
    {
        DataTable dtT = new DataTable();
        foreach (object obj in cabecera)
        {
            
            dtT.Columns.Add(obj.ToString());

        }
        dtT.Rows.Add(dtT.NewRow());
        gvTemp.DataSource = dtT;
        gvTemp.DataBind();
        gvTemp.Rows[0].Enabled = false;
    }

    public static DataTable InicializarGridView(object[] campos)
    {
        DataTable dt = new DataTable();
        for (int i = 0; i < campos.Length; i++)
        {
            dt.Columns.Add(new DataColumn(campos[i].ToString()));
        }
        dt.Rows.Add(dt.NewRow());
        return dt;
    }
    #endregion

    #region AJAX
    public static void MostrarAlert(string mensaje, Page page)
    {
        Type cstype = page.GetType();
        String csname = "AlertaErrorScript";
        ClientScriptManager cs = page.ClientScript;
        if (!cs.IsStartupScriptRegistered(cstype, csname))
        {
            string alertaErrorScript = "alert('" + mensaje + "');";
            ScriptManager.RegisterClientScriptBlock(page, cstype, csname, alertaErrorScript, true);
        }
    }

    public static void EstablecerFocoAjax(Control controlFoco, Page page)
    {
        Type cstype = page.GetType();
        String csname1 = "FocusScript1";
        String csname2 = "FocusScript2";
        ClientScriptManager cs = page.ClientScript;
        string focusScript = "<script language='JavaScript'>" +
        "document.getElementById('" + controlFoco.ClientID +
        "').focus();</script>";
        ScriptManager.RegisterClientScriptBlock(controlFoco, cstype, csname1, focusScript, false);
        ScriptManager.RegisterStartupScript(controlFoco, cstype, csname2, focusScript, false);
    }
    #endregion   

    #region Fechas
    /// <summary>
    /// Convierte una fecha a string en formato yyyy.mm.dd
    /// </summary>
    public static string FechaToString(DateTime fecha)
    {
        return string.Format("{0}.{1}.{2}", fecha.Year, fecha.Month.ToString().PadLeft(2, '0'), fecha.Day.ToString().PadLeft(2, '0'));
    }
    /// <summary>
    /// Convierte una fecha a string en formato dd.MMM.yyyy
    /// </summary>
    public static string FechaToStringMes(DateTime fecha)
    {
        return fecha.ToString("dd/MMM/yyyy");
    }
    #endregion 

    #region Excel
    public static string ExportarCSV(System.Web.HttpResponse response, GridView gv, string titulo)
    {
        StringBuilder Exportar = new StringBuilder();

        foreach (DataControlField header in gv.Columns)
        {
            if (!header.HeaderStyle.CssClass.Equals("hiddencol", StringComparison.InvariantCultureIgnoreCase))
            {
                Exportar.Append(Utilerias.RemoveSpecialChars(header.HeaderText) + ",");
            }
        }

        foreach (GridViewRow row in gv.Rows)
        {
            Exportar.AppendLine();
            for (int j = 0; j < gv.Columns.Count; j++)
            {
                if (!gv.Columns[j].ItemStyle.CssClass.Equals("hiddencol", StringComparison.InvariantCultureIgnoreCase))
                {
                    string text = string.IsNullOrEmpty(Utilerias.PrepararControlesForExport(row.Cells[j])) ? row.Cells[j].Text : Utilerias.PrepararControlesForExport(row.Cells[j]);
                    Exportar.Append(Utilerias.RemoveSpecialChars(text) + ",");
                }
            }
        }

        Utilerias.ExportToCSV(response, Exportar.ToString());
        return Exportar.ToString();
    }
    public static void ExportToCSV(System.Web.HttpResponse Response, string datos)
    {
        Response.Clear();
        Response.Write(datos);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Exportar.csv");
        Response.End();
    }
    /// <summary>
    /// Exporta a Excel los datos de un GridView
    /// </summary>
    /// <param name="gv">Grid a Exportar</param>
    /// <param name="Titulo">Titulo del Documento</param>
    /// <returns></returns>
    public static string ExportarExcel(System.Web.HttpResponse response, GridView gv, string titulo)
    {
        string Exportar = "<table style='border-style:solid;'>";

        Exportar += "<tr>";
        Exportar += "<td align='center' colspan='5'>" + Utilerias.RemoveSpecialChars(titulo) + "</td>";
        Exportar += "</tr>";
        
        Exportar += "<tr>";
        foreach (DataControlField header in gv.Columns)
        {
            if (!header.HeaderStyle.CssClass.Equals("hiddencol", StringComparison.InvariantCultureIgnoreCase))
            {
                Exportar += "<td>" + Utilerias.RemoveSpecialChars(header.HeaderText) + "</td>";
            }
        }
        Exportar += "</tr>";

        foreach (GridViewRow row in gv.Rows)
        {
            Exportar += "<tr>";
            for (int j = 0; j < gv.Columns.Count; j++)
            {
                if (!gv.Columns[j].ItemStyle.CssClass.Equals("hiddencol", StringComparison.InvariantCultureIgnoreCase))
                {
                    string text = string.IsNullOrEmpty(Utilerias.PrepararControlesForExport(row.Cells[j])) ? row.Cells[j].Text : Utilerias.PrepararControlesForExport(row.Cells[j]);
                    Exportar += "<td>" + Utilerias.RemoveSpecialChars(text) + "</td>";
                }
            }
            Exportar += "</tr>";
        }
        Exportar += "</table>";

        Utilerias.ExportToExcel(response, Exportar);
        return Exportar;
    }
    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static string PrepararControlesForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is Label)
            {
                return (current as Label).Text;
            }

            if (current.HasControls())
            {
                return Utilerias.PrepararControlesForExport(current);
            }
        }

        return string.Empty; 
    }
    public static void ExportToExcel(System.Web.HttpResponse Response, string datos)
    {
        Response.Clear();
        Response.Write(datos);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Informe.xls");
        Response.End();
    }
    public static string ExportarExcel(int columnas, GridView gv, int ColIgnInicio, int FilIgnInicio, int ColIgnFinal, int FilIgnFinal, string Titulo, string encabezado)
    {
        string libro = exportar.WorkBook();
        libro = libro + exportar.crearAreaEstilo();
        libro = libro + exportar.CrearEstilo(1, "Arial", 12, "000000", true, 2, 3);
        libro = libro + exportar.CrearEstilo(2, "Arial", 12, "000000", false, 1, 3);
        libro = libro + exportar.cerrarAreaEstilo();
        libro = libro + exportar.CrearWorksheet("Informe");
        int maxColumnas = columnas + 10;
        int maxRenglones = gv.Rows.Count + 10;
        libro = libro + exportar.Tabla(maxColumnas, maxRenglones, 1, 1, 60);
        libro = libro + exportar.CrearRow(2);
        libro = libro + exportar.Cell_unirEstilo(2, 1, 2, Titulo, 1);
        libro = libro + exportar.CerrarRow();
        int r = 4;
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            if (i != FilIgnInicio && i != FilIgnFinal)
            {
                libro = libro + exportar.CrearRow(r + i);
                for (int j = 0; j < columnas; j++)
                {
                    if (j != ColIgnInicio && j != ColIgnFinal)
                        libro = libro + exportar.Cell_Estilo(j + 1, 1, gv.Rows[i].Cells[j].Text.ToString(), 2);
                }
                libro = libro + exportar.CerrarRow();
            }
        }
        libro = libro + exportar.cerrarTabla();
        libro = libro + exportar.cerrarWorksheet();
        libro = libro + exportar.cerrarWorkBook();
        return libro;
    }
    #endregion

    #region Caracteres Especiales
    /// <summary>
    /// Quita caracteres acentuados ( sustituye áéíóúüñÑ por aeiounN )
    /// </summary>
    /// <param name="value">Texto que contiene caracteres acentuados</param>
    /// <returns>Texto sin caracteres acentuados</returns>
    public static string RemoveSpecialChars(string value)
    {
        String normalizedText = value.Normalize(System.Text.NormalizationForm.FormD);
        StringBuilder resultValue = new StringBuilder();

        for (int Ptr = 0; (Ptr < normalizedText.Length); Ptr++)
        {
            Char character = normalizedText[Ptr];
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(character) != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                resultValue.Append(character);
            }
        }

        return resultValue.ToString();
    }
    #endregion

    #region Subir imagenes al servidor
    public static bool CargaImagenAlServidor(string pathServer, FileUpload fu, Page p)
    {
        Boolean fileOK = false;
        String path = p.Server.MapPath(pathServer);
        if (fu.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(fu.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg",".ico" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
        }

        if (fileOK)
        {
            try
            {
                fu.PostedFile.SaveAs(path
                    + fu.FileName);
                MostrarAlert("Archivo guardado!", p) ;
            }
            catch (Exception ex)
            {
                MostrarAlert("No se pudo guardar el archivo!", p);
            }
        }
        else
        {
            MostrarAlert("No se aceptan archivos de este tipo!", p);
        }
        return true;
    }
    #endregion
}
