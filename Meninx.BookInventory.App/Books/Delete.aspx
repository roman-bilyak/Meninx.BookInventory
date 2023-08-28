<%@ Page Title="Delete Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="Meninx.BookInventory.App.Books.Delete" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Confirm Deletion</h2>
    <p>Are you sure you want to delete the following book?</p>
    <table>
        <tr>
            <td><asp:Label ID="lblTitle" runat="server" Text="Title" /></td>
            <td><asp:Label ID="lblTitleValue" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblAuthor" runat="server" Text="Author" /></td>
            <td><asp:Label ID="lblAuthorValue" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblISBN" runat="server" Text="ISBN" /></td>
            <td><asp:Label ID="lblISBNValue" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPublicationYear" runat="server" Text="Publication Year" /></td>
            <td><asp:Label ID="lblPublicationYearValue" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblQuantity" runat="server" Text="Quantity" /></td>
            <td><asp:Label ID="lblQuantityValue" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCategory" runat="server" Text="Category" /></td>
            <td><asp:Label ID="lblCategoryValue" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Label ID="lblMessage" runat="server" CssClass="message-label" /></td>
        </tr>
    </table>
</asp:Content>