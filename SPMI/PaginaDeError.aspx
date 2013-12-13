<%@ Page Title="Informe general" MasterPageFile="~/MPManagement.Master" Language="C#" AutoEventWireup="true" CodeFile="PaginaDeError.aspx.cs" Inherits="PaginaDeError" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Informe general de profesores</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="MainContent" ContentPlaceHolderID="MainContent">

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Label ID="lblError" runat="server" Text="Error..." cssclass="lbl"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

