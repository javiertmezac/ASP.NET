<%@ Page Title="Inicio de sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h3><%: Title %>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </h3>
    </hgroup>
    <section id="loginForm">

        <asp:UpdatePanel ID="upPrincipal" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>

                <asp:Table ID="Table1" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label1" runat="server" CssClass="lbl"> Usuario: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txt" />
                            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="*" ValidationGroup="1" ControlToValidate="txtUserName">*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell RowSpan="4">
                            <asp:Image ID="Image2" ImageUrl="~/Images/userMas.png" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" CssClass="lbl"> Contraseña: </asp:Label>
                        </asp:TableCell><asp:TableCell>
                            <asp:TextBox TextMode="Password" ID="txtPassWord" runat="server" CssClass="txt" />
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*" ValidationGroup="1" ControlToValidate="txtPassWord"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                        </asp:TableCell><asp:TableCell>
                            <asp:Button ID="Button1" ValidationGroup="1" runat="server" OnClick="Button1_Click" Text="Entrar" CssClass="btn" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </section>                                             
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
