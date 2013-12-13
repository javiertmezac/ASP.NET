<%@ Page Title="Empresas" MasterPageFile="~/MPManagement.Master" Language="C#" AutoEventWireup="true" CodeFile="Empresas.aspx.cs" Inherits="Account_Empresas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Empresas</h2>

                <asp:ImageButton ID="btnExportar" ToolTip="Exportar a Excel" runat="server" OnClick="btnExportar_Click" ImageUrl="~/Images/excel.ico" Width="30px" Height="25px" ImageAlign="Bottom" />
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="MainContent" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:MultiView ID="mvCatalogo" runat="server" ActiveViewIndex="0">

                <asp:View ID="vLista" runat="server">
                    <asp:Table runat="server" Style="width: 515px">
                        <asp:TableRow>
                            <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnFiltro">
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn"></asp:Button>
                                    <asp:Label ID="lblBuscar" runat="server" Text="Buscar:" CssClass="lbl"></asp:Label>
                                    <asp:TextBox ID="txtFiltro" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                    <asp:ImageButton ID="btnFiltro" ToolTip="Filtrar/Buscar" runat="server" OnClick="btnFiltro_Click" ImageUrl="~/Images/icono_buscar18.png" Width="30px" Height="25px" ImageAlign="Bottom" />
                                </asp:Panel>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                <asp:GridView ID="gvCatalogo" AllowPaging="true" AllowSorting="true" runat="server"
                                    AutoGenerateColumns="false" Width="100%" PageSize="20" CssClass="gridview"
                                    DataKeyNames="id"
                                    OnRowEditing="gvCatalogo_RowEditing"
                                    OnSorting="gvCatalogo_Sorting"
                                    OnPageIndexChanging="gvCatalogo_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" 
                                                    ToolTip="Modificar" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" 
                                                    CausesValidation="False" Width="30px" Height="25px" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEditar" runat="server" Text="Editar"></asp:Label>
                                            </HeaderTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="noCliente" HeaderText="Número Tanque" SortExpression="noCliente">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="rfc" HeaderText="RFC" SortExpression="rfc">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="tel">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="colonia" HeaderText="Colonia" SortExpression="colonia">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="calle" HeaderText="Calle" SortExpression="calle">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="noInt" HeaderText="No.Interior" SortExpression="noInt">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="noExt" HeaderText="No.Exterior" SortExpression="noExt">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="cPostal" HeaderText="Cod.Postal" SortExpression="cPostal">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="150px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="fechaRegistro" HeaderText="Registro" SortExpression="fechaRegistro">
                                            <ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                         <asp:BoundField DataField="precio" HeaderText="Precio" SortExpression="precio">
                                            <ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                     <%--   <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status">
                                            <ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField>
                                            <ItemStyle Width="25px" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lbtnEliminar" runat="server" OnClick="btnEliminar_Click" ToolTip="Eliminar"
                                                    ImageUrl="~/Images/del.png" Width="30px" Height="25px" />
                                                <ajax:ConfirmButtonExtender ID="confirm" runat="server" ConfirmText="Esta seguro de Eliminar el registro?"
                                                    TargetControlID="lbtnEliminar">
                                                </ajax:ConfirmButtonExtender>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEliminar" runat="server" Text="Eliminar"></asp:Label>
                                            </HeaderTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton id="btnVerContactos" runat="server" OnClick="btnVerContactos_Click" 
                                                    ImageUrl="~/Images/icono_buscar18.png" Width="30px" Height ="25px"/>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblVerContactos" runat="server" Text="Ver Contactos"></asp:Label>
                                            </HeaderTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:View>

                <asp:View ID="vAgregar" runat="server">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGuardar">
                        <asp:Table Style="width: 100%" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblId" runat="server" Text="Número de empresa:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblNumeroCliente" runat="server" Text="Número de Tanque:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtId" runat="server" Width="150px" CssClass="txt" Enabled="false" />
                                    <%--<asp:RequiredFieldValidator ID="rfvId" runat="server" ErrorMessage="*" ValidationGroup="2" ControlToValidate="txtId">*</asp:RequiredFieldValidator>--%>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtNumeroCliente" runat="server" Width="150px" CssClass="txt" />
                                    <asp:RequiredFieldValidator ID="rfvNumeroCliente" runat="server" ErrorMessage="*" 
                                        ValidationGroup="2" ControlToValidate="txtNumeroCliente">*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblrfc" runat="server" Text="RFC:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lbltelefono" runat="server" Text="Teléfono:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblcolonia" runat="server" Text="Colonia:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtNombre" runat="server" Width="150px" CssClass="txt" />
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                        ErrorMessage="*" ValidationGroup="2" 
                                        ControlToValidate="txtNombre">*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtRfc" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtTelefono" runat="server" Width="150px" CssClass="txt" />
                                    <asp:RequiredFieldValidator ID="rfvtelefono" runat="server" 
                                        ErrorMessage="*" ValidationGroup="2" 
                                        ControlToValidate="txtTelefono">*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtColonia" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblCalle" runat="server" Text="Calle:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblNoInt" runat="server" Text="No.Interior:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblNoExt" runat="server" Text="No.Exterior:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblCodPostal" runat="server" Text="Cod.Postal:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtCalle" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCalle" runat="server" ErrorMessage="*" 
                                        ValidationGroup="2" ControlToValidate="txtCalle">*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtNoInt" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtNoExt" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtCodPostal" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblFechaRegistro" runat="server" Text="Fecha de registro:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblPrecio" runat="server" Text="Tipo de Precio" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtFechaRegistro" runat="server" Width="150px" CssClass="txt"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:CheckBox ID="txtStatus" runat="server" Width="150px" CssClass="txt"></asp:CheckBox>
                                </asp:TableCell>

                                <asp:TableCell>
                                    <asp:DropDownList ID="ddlTipoPrecio" runat="server" Width="200px" AutoPostBack ="true"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:LinkButton ID="btnLigarPedidos" runat="server" 
                                        OnClick="btnLigarPedidos_Click" Text="Pedidos de éste cliente"></asp:LinkButton>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:LinkButton ID="btnLigarContactos" runat="server" 
                                        OnClick="btnLigarContactos_Click" Text="Contactos en ésta empresa"></asp:LinkButton>
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                        <hr />
                        <table style="width: 515px">
                            <asp:TableRow>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Button ID="btnGuardar" ValidationGroup="2" runat="server" 
                                        Text="Aceptar" OnClick="btnGuardar_Click" Width="80px" CssClass="btn"></asp:Button>
                                    <asp:Button ID="btnBack" runat="server" Text="Regresar" 
                                        OnClick="btnRegresar_Click" Width="80px" CssClass="btn"></asp:Button>
                                </asp:TableCell>
                            </asp:TableRow>
                        </table>

                        <asp:Panel ID="pnlFechaRegistro" runat="server" CssClass="popupControl">
                            <asp:UpdatePanel ID="upCalRegistro" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="calFechaRegistro" runat="server" CssClass="cal" OnSelectionChanged="calFechaRegistro_SelectionChanged">
                                        <DayHeaderStyle CssClass="calDayHeader" />
                                        <DayStyle CssClass="calDayStyle" />
                                        <NextPrevStyle CssClass="calNextPrev" />
                                        <SelectedDayStyle CssClass="calSelected" />
                                        <OtherMonthDayStyle CssClass="calOtherMonthDay" />
                                        <TitleStyle CssClass="calTitle" />
                                        <TodayDayStyle CssClass="calTodayDay" />
                                        <WeekendDayStyle CssClass="calWeekendDay" />
                                    </asp:Calendar>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <ajax:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtFechaRegistro"
                                PopupControlID="pnlFechaRegistro">
                            </ajax:PopupControlExtender>
                        </asp:Panel>

                    </asp:Panel>
                </asp:View>

            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
        .item-style {
        }

        .header-style {
        }
    </style>
</asp:Content>

