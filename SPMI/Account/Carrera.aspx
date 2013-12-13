<%@ Page Title="Carreras" MasterPageFile="~/MPManagement.Master" Language="C#" AutoEventWireup="true" CodeFile="Carrera.aspx.cs" Inherits="Account_Carrera" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Carreras (Programas)</h2>
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
                    <asp:Table ID="Table1" runat="server" Style="width: 70%">
                        <asp:TableRow>
                            <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFiltro">
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Modificar" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="False" Width="30px" Height="25px" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEditar" runat="server" Text="Editar"></asp:Label>
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="clave" HeaderText="Clave" SortExpression="clave">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                                            <ItemStyle Width="250px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Width="250px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
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
                                    </Columns>
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:View>
                <asp:View ID="vAgregar" runat="server">
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnGuardar">
                        <asp:Table ID="Table2" Style="width: 100%" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Style="width: 100px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblClave" runat="server" Text="Clave:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtClave" runat="server" Width="150px" CssClass="txt" />
                                    <asp:RequiredFieldValidator ID="rfvClave" runat="server" ErrorMessage="*" ValidationGroup="1" ControlToValidate="txtClave">*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Style="width: 100px; vertical-align: top; text-align: left">
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="lbl"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="width: 180px; vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtNombre" runat="server" Width="150px" CssClass="txt" />
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*" ValidationGroup="1" ControlToValidate="txtNombre">*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <hr />
                        <table style="width: 515px">
                            <asp:TableRow>
                                <asp:TableCell Style="width: 110px; vertical-align: top; text-align: left">
                                    <asp:Button ID="btnGuardar" ValidationGroup="1" runat="server" Text="Aceptar" OnClick="btnGuardar_Click" Width="80px" CssClass="btn"></asp:Button>
                                    <asp:Button ID="btnBack" runat="server" Text="Regresar" OnClick="btnRegresar_Click" Width="80px" CssClass="btn"></asp:Button>
                                </asp:TableCell>
                            </asp:TableRow>
                        </table>
                    </asp:Panel>
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


