<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Sistema de gestión - Planta Oasis</h2>
            </hgroup>
            <p>
            </p>
        </div>
    </section>   
</asp:Content>
<asp:Content runat="server" ID="MainContent" ContentPlaceHolderID="MainContent">
 <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upPrincipal" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
           <h3> Click en iniciar sesión para ingresar al sistema...</h3>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
