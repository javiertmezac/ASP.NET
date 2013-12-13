using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for exportar
/// </summary>
public class exportar
{
    /// <summary>
    /// Beta 1.0 to Excel by Juan
    /// </summary>
	public exportar()
	{}

    /// <summary>
    /// Antes que cualquier cosa se debe crear el workbook para despues crear las paginas y todo lo demas
    /// opcion 1:ABrir
    /// opcion 2:cerrar
    /// </summary>
    /// <param name="opcion"></param>
    /// <returns></returns>
    public static string WorkBook()
    {
        return "<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:c='urn:schemas-microsoft-com:office:component:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>";
    }

    public static string cerrarWorkBook()
    {
        return "</Workbook>";
    }

    /// <summary>
    /// Si requiere poner informacion de la persona que creo el archivo
    /// </summary>
    /// <param name="Author"></param>
    /// <param name="Fecha"></param>
    /// <param name="Version"></param>
    /// <returns></returns>
    public static string PropiedadesDocumento(string Author, DateTime Fecha, string Version)
    {
        string propiedadDoc = "<DocumentProperties xmlns='urn:schemas-microsoft-com:office:office'><LastAuthor>" + Author.ToString() + "</LastAuthor>";
        propiedadDoc = propiedadDoc + "<Created>" + Fecha.ToString() + "</Created>";
        propiedadDoc = propiedadDoc + "<Version>" + Version + "</Version>";
        return propiedadDoc;
    }

    public static string propiedadesVentana(int WindowHeight, int WindowWidth, int WindowTopX, int WindowTopY, bool ProtegerStructura, bool protegerVentana)
    {
        return "<ExcelWorkbook xmlns='urn:schemas-microsoft-com:office:excel'>  <WindowHeight>" + WindowHeight.ToString() + "</WindowHeight>  <WindowWidth>" + WindowWidth.ToString() + "</WindowWidth>  <WindowTopX>" + WindowTopX.ToString() + "</WindowTopX>  <WindowTopY>" + WindowTopY.ToString() + "</WindowTopY>  <ProtectStructure>" + ProtegerStructura.ToString() + "</ProtectStructure>  <ProtectWindows>" + protegerVentana.ToString() + "</ProtectWindows> </ExcelWorkbook>";
    }

    /// <summary>
    /// Crea Pagina
    /// Opcion 1:Abrir
    /// Opcion 2:Cerrar
    /// </summary>
    /// <param name="Nombre_pagina"></param>
    /// <returns></returns>
    public static string CrearWorksheet(string Nombre_pagina)
    {
        return "<Worksheet ss:Name='" + Nombre_pagina + "'>";
    }

    public static string cerrarWorksheet()
    {
        return "</Worksheet>";
    }

    /// <summary>
    /// MaxColumnas Esto indicara cuanto se expandira la tabla, si se pone algo fuera de este rango no se mostrara
    /// opcion 1:Abrir
    /// opcion 2:Cerrar
    /// ColumnasCompletas: Indica si se usara ajuste automatico o no para las col
    /// RenglonesCompletos similar el tamano por default 27
    /// por ser beta, el colcompletas y renglon seran asignados automaticamente
    /// El tamano de una columna se aplica para toda las columnas de todos los renglones 
    /// </summary>
    /// <param name="MaxColumnas"></param>
    /// <param name="MaxRenglones"></param>
    /// <returns></returns>
    public static string Tabla(int MaxColumnas, int MaxRenglones, int ColumnasCompletas, int RenglonesCompletos, decimal DefaultWidth)
    {
        string Tabla = "";
        Tabla = "<Table ss:ExpandedColumnCount='" + MaxColumnas.ToString() + "' ss:ExpandedRowCount='" + MaxRenglones.ToString() + "' x:FullColumns='" + ColumnasCompletas.ToString() + "' x:FullRows='" + RenglonesCompletos.ToString() + "' ss:DefaultColumnWidth='" + DefaultWidth.ToString() + "'>";
        //Puede que marque error aqui sino hay diferencia entre columnas completas con las declaradas
        for (int i = 0; i < MaxColumnas; i++)
        {
            Tabla = Tabla + "<Column ss:AutoFitWidth='0' ss:Width='" + DefaultWidth.ToString() + "'/>";
        }
        return Tabla;
    }

    public static string cerrarTabla()
    {
        return "</Table>";
    }

    /// <summary>
    /// Crear renglon con tamano automatico
    /// </summary>
    /// <param name="Renglon"></param>
    /// <returns></returns>
    public static string CrearRow(int Renglon)
    {
        string creaRenglon = "";
        if (Renglon == 1)
        {
            creaRenglon = "<Row >";
        }
        else
            creaRenglon = "<Row ss:Index='" + Renglon.ToString() + "'>";

        return creaRenglon;
    }

    /// <summary>
    /// Asignar el numero de estilo que se usara en el renglon
    /// </summary>
    /// <param name="Renglon"></param>
    /// <param name="Estilo"></param>
    /// <returns></returns>
    public static string CrearRow_Estilo(int Renglon, int Estilo)
    {
        string creaRenglon = "";
        if (Renglon == 1)
        {
            creaRenglon = "<Row ss:StyleID='s" + Estilo.ToString() + "'>";
        }
        else
            creaRenglon = "<Row ss:Index='" + Renglon.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "'>";

        return creaRenglon;
    }

    /// <summary>
    /// Renglo, permitira crear el renglon a utilizar nada mas
    /// el Heigth si quiere q el renglon tenga un tamano diferente, por default 0
    /// </summary>
    /// <param name="Renglon"></param>
    /// <param name="HeigthR"></param>
    /// <returns></returns>
    public static string CrearRow_pHeight(int Renglon, int HeigthR)
    {
        string newrow;
        if (Renglon == 1)
        {
            newrow = "<Row ss:AutoFitHeight='" + HeigthR.ToString() + "'>";
        }
        else
            newrow = "<Row ss:Index='" + Renglon.ToString() + "' ss:AutoFitHeight='" + HeigthR.ToString() + "'>";

        return newrow;
    }

    /// <summary>
    /// Agrega campo en la columna deseada.
    /// </summary>
    /// <param name="Celda"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string Cell_str(int Celda, string Data)
    {
        string tData = Data.Trim().ToUpper();
        if (tData == "&NBSP;")
        {
            Data = "";
        }
    /*    decimal dDecimalv = 0;
        int nIntv = 0;
        bool dData = decimal.TryParse(Data,out dDecimalv);
        bool nData = int.TryParse(Data,out nIntv);
        string tipo = "String";
        if (dData == true || nData == true)
        {
            tipo = "Number";
        }*/
        return "<Cell ss:Index='" + Celda.ToString() + "'><Data ss:Type='String'>" + Data + "</Data></Cell>";
    }

    public static string Cell_num(int Celda, int Data)
    {
        return "<Cell ss:Index='" + Celda.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>";
    }

    public static string Cell_dec(int Celda, decimal Data)
    {
        return "<Cell ss:Index='" + Celda.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>";
    }

    /// <summary>
    /// tipo dato 1: string
    /// 2: Numerico
    /// </summary>
    /// <param name="Celda"></param>
    /// <param name="tipoDato"></param>
    /// <param name="Data"></param>
    /// <param name="Estilo"></param>
    /// <returns></returns>
    public static string Cell_Estilo(int Celda, int tipoDato, string Data, int Estilo)
    {
        if (Data.ToUpper() == "&NBSP;" || Data.ToUpper() == "&EMP;NBSP;")
        {
            Data = null;
        }
        /*decimal dDecimalv = 0;
        int nIntv = 0;
        bool dData = decimal.TryParse(Data, out dDecimalv);
        bool nData = int.TryParse(Data, out nIntv);
        string tipo = "String";
        if (nData == true)
        {
            tipo = "Number";
        }*/

       string CeldaU = "";
        //switch (tipoDato)
       // {
         //   case 1: 
       if (Data != null)
       {
           CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "' ><Data ss:Type='String'>" + Data + "</Data></Cell>"; 
       }
       else
       {
           CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "' ><Data ss:Type='String'></Data></Cell>"; 
       }
        //   case 2: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "' ><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
         //   case 3: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "' ><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
         //   case 4: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "' ><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
       // }
        return CeldaU; 

       // return "<Cell ss:Index='" + Celda.ToString() + "'ss:StyleID='s" + Estilo.ToString() + "'><Data ss:Type='" + tipo + "'>" + Data + "</Data></Cell>";
    }

    /// <summary>
    /// Data : valor inicial, o default
    /// Formula = example A1 + B2 otro seria (p1,c5)*G5
    /// </summary>
    /// <param name="Celda"></param>
    /// <param name="Formula"></param>
    /// <param name="TipoDato"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string Cell_Formula(int Celda, string Formula, int TipoDato, int Data, int Renglon)
    {
        return "<Cell ss:Index='" + Celda.ToString() + "' ss:Formula='" + CrearFormula(Formula, Renglon, Celda) + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>";
    }

    /// <summary>
    /// Tipo Dato
    /// 1:String, 2- Entero, 3- Decimal
    /// Celdas a Unir ingresar el entero de las celdas q se van a colapsar dos celdas poner 2 y asi
    /// </summary>
    /// <param name="Celda"></param>
    /// <param name="TipoDato"></param>
    /// <param name="CeldasAUnir"></param>
    /// <returns></returns>
    public static string Cell_unir(int Celda, int TipoDato, int CeldasAUnir, string Data)
    {
        string CeldaU = "";
        CeldasAUnir = CeldasAUnir - 1;
        switch (TipoDato)
        {
            case 1: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "'><Data ss:Type='String'>" + Data + "</Data></Cell>"; break;
            case 2: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
            case 3: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
            case 4: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;

        }
        return CeldaU;
    }

    /// <summary>
    /// Tipo Dato
    /// 1:String, 2- Entero, 3- Decimal
    /// Celdas a Unir ingresar el entero de las celdas q se van a colapsar dos celdas poner 2 y asi
    /// </summary>
    /// <param name="Celda"></param>
    /// <param name="TipoDato"></param>
    /// <param name="CeldasAUnir"></param>
    /// <returns></returns>
    public static string Cell_unirEstilo(int Celda, int TipoDato, int CeldasAUnir, string Data, int Estilo)
    {
        string CeldaU = "";
        CeldasAUnir = CeldasAUnir - 1;
        switch (TipoDato)
        {
            case 1: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "'><Data ss:Type='String'>" + Data + "</Data></Cell>"; break;
            case 2: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
            case 3: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
            case 4: CeldaU = "<Cell ss:Index='" + Celda.ToString() + "' ss:MergeAcross='" + CeldasAUnir.ToString() + "' ss:StyleID='s" + Estilo.ToString() + "'><Data ss:Type='Number'>" + Data + "</Data></Cell>"; break;
        }
        return CeldaU;
    }

    /// <summary>
    /// Cierra renglon
    /// </summary>
    /// <returns></returns>
    public static string CerrarRow()
    {
        return "</Row>";
    }

    /// <summary>
    /// Opciones por default de proteccion son False, para que el usuario pueda modificar el archivo
    /// esta opcion no es muy necesaria
    /// </summary>
    /// <param name="HeaderMargin"></param>
    /// <param name="FooterMargin"></param>
    /// <param name="margenIzq"></param>
    /// <param name="margenFondo"></param>
    /// <param name="margenTop"></param>
    /// <param name="margenDer"></param>
    /// <param name="protegerObjetos"></param>
    /// <param name="protegerScenario"></param>
    /// <returns></returns>
    public static string PropiedadesWorkSheet(int HeaderMargin, int FooterMargin, int margenIzq, int margenFondo, int margenTop, int margenDer, bool protegerObjetos, bool protegerScenario)
    {
        string propiedades;
        propiedades = "<WorksheetOptions xmlns='urn:schemas-microsoft-com:office:excel'><PageSetup><Header x:Margin='" + HeaderMargin.ToString() + "'/>";
        propiedades += "<Footer x:Margin='" + FooterMargin.ToString() + "'/>";
        propiedades += "<PageMargins x:Bottom='" + margenFondo.ToString() + "' x:Left='" + margenIzq.ToString() + "' x:Right='" + margenDer.ToString() + "' x:Top='" + margenTop.ToString() + "'/> </PageSetup>";
        propiedades += "<ProtectObjects>" + protegerObjetos.ToString() + "</ProtectObjects><ProtectScenarios>" + protegerScenario.ToString() + "</ProtectScenarios><x:ViewableRange>R1:R262144</x:ViewableRange><x:Selection>R1C1</x:Selection></WorksheetOptions><c:WorksheetOptions></c:WorksheetOptions>";
        return propiedades;
    }

    /* falta crear los components */

    /// <summary>
    /// Asignarle un numero de Estilo,
    /// Horizonta: 1- left, 2- Center , 3- Right
    /// Vertical: 1- Top, 2-middle, 3-Bottom  //el middle ocasiona problemas
    /// </summary>
    /// <param name="nEstilo"></param>
    /// <returns></returns>
    public static string CrearEstilo(int nEstilo, string Font, int tamano, string colorRGB, bool Bold, int Horizontal, int Vertical)
    {
        string Estilo;
        int Negritas = 0;
        if (Bold) Negritas = 1;
        string alH = "Left";
        string alV = "Bottom";
        switch (Horizontal)
        {
            case 1: alH = "Left"; break;
            case 2: alH = "Center"; break;
            case 3: alH = "Right"; break;
        }

        switch (Vertical)
        {
            case 1: alV = "Top"; break;
            case 2: alV = "Middle"; break;
            case 3: alV = "Bottom"; break;
        }

        Estilo = "<Style ss:ID='s" + nEstilo.ToString() + "'>";
        //  Estilo = Estilo + "<Font ss:FontName='" + Font + "' x:Family='" + Font + "' ss:Size='" + tamano.ToString()+ "' ss:Color='#" + colorRGB + "' ss:Bold='" + Negritas.ToString() +"' ss:Horizontal='" + alH + "' ss:Vertical='" + alV + "'>";
        Estilo = Estilo + "<Font ss:FontName='" + Font + "' ss:Size='" + tamano.ToString() + "' ss:Color='#" + colorRGB + "' ss:Bold='" + Negritas.ToString() + "'/>";
        Estilo = Estilo + "<Alignment ss:Horizontal='" + alH + "' ss:Vertical='" + alV + "'/>";
        Estilo = Estilo + "</Style>";
        return Estilo;
    }

    /// <summary>
    /// Si se utilizaran estilos en necesario crear el area sino marcara error
    /// </summary>
    /// <returns></returns>
    public static string crearAreaEstilo()
    {
        return "<Styles>";
    }

    public static string cerrarAreaEstilo()
    {
        return "</Styles>";
    }

    /// <summary>
    /// Opciones que se desplegaran cuando el usuario le de guardar
    /// MaxAlto y MaxAncho son para porcentajes
    /// Hojasiguiente: ejemplo si se utilizan 3 hojas y la siguiente q se va a crear sera la cuarta enviar el 4 (solo para referencia)
    /// </summary>
    /// <returns></returns>
    public static string OpcionesComponentes(string desComponente, int MxAltoP, int MaxAnchoP, int HojaSiguiente)
    {
        string Comp = "<c:ComponentOptions><c:Label><c:Caption>" + desComponente + "</c:Caption></c:Label>";
        Comp = Comp + "<c:MaxHeight>" + MxAltoP.ToString() + "%</c:MaxHeight><c:MaxWidth>" + MaxAnchoP.ToString() + "%</c:MaxWidth><c:NextSheetNumber>" + HojaSiguiente.ToString() + "</c:NextSheetNumber></c:ComponentOptions>";
        return Comp;
    }

    /// <summary>
    /// Referencia de version del objeto q produjo el xml por ejemplo
    /// </summary>
    /// <param name="Referencia"></param>
    /// <returns></returns>
    public static string OpcionesWorkBookReferencias(string objeto, string Referencia)
    {
        return "<x:WorkbookOptions><c:" + objeto + ">" + Referencia + "</c:" + objeto + "></x:WorkbookOptions>";
    }

    public static string CrearFormula(string formula, int RenglonInicial, int CeldaInicial)
    {
        string conversion = "=";
        int Renglon = 0;
        int Celda = 0;
        int LetraR = 0;
       // int suma = 0;
        string RenglonStr;
        string ColumnStr;
        int contador = 0;
        string[] arreglo = formula.Split(new char[] { '*', '(', ')', '/', '+', '-', '[', ']', '{', '}', '%', ':' });
        string operadores = "*()/+-[]{}%:";
        if (operadores.Contains(formula.Substring(0, 1)))
            contador++;

        foreach (string s in arreglo)
        {
            switch (s.Substring(0, 1))
            {
                case "A": LetraR = 1; break;
                case "B": LetraR = 2; break;
                case "C": LetraR = 3; break;
                case "D": LetraR = 4; break;
                case "E": LetraR = 5; break;
                case "F": LetraR = 6; break;
                case "G": LetraR = 7; break;
                case "H": LetraR = 8; break;
                case "I": LetraR = 9; break;
                case "J": LetraR = 10; break;
                case "K": LetraR = 11; break;
                case "L": LetraR = 12; break;
                case "M": LetraR = 13; break;
                case "N": LetraR = 14; break;
                case "O": LetraR = 15; break;
                case "P": LetraR = 16; break;
                case "Q": LetraR = 17; break;
                case "R": LetraR = 18; break;
                case "S": LetraR = 19; break;
                case "T": LetraR = 20; break;
                case "U": LetraR = 21; break;
                case "V": LetraR = 22; break;
                case "W": LetraR = 23; break;
                case "X": LetraR = 24; break;
                case "Y": LetraR = 25; break;
                case "Z": LetraR = 26; break;
            }
            Renglon = LetraR - RenglonInicial;
            Celda = Convert.ToInt32(s.Substring(1, (s.Length - 1))) - CeldaInicial;

            if (Renglon == 0) RenglonStr = "R";
            else RenglonStr = "R[" + Renglon.ToString() + "]";
            if (Celda == 0) ColumnStr = "C";
            else ColumnStr = "C[" + Celda.ToString() + "]";

            contador = contador + s.Length;
            if ((contador + 1) < formula.Length)
                conversion = conversion + RenglonStr + ColumnStr + formula.Substring(contador, 1);
            else
                conversion = conversion + RenglonStr + ColumnStr;
            contador++;
        }
        return conversion;
    }
}
