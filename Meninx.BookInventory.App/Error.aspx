<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Meninx.BookInventory.App.Error" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-top: 100px;">
        <h1>An Error Occurred</h1>
        <p class="form-error"><%= Session["ErrorMessage"] %></p>
        <p>Please contact support for assistance.</p>
        <p><a href="Default.aspx">Go back to the home page</a></p>
    </div>
</asp:Content>
