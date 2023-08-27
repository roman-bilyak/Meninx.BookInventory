<%@ Page Title="Add Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="Meninx.BookInventory.App.Pages.AddBook" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td><asp:Label ID="lblTitle" runat="server" Text="Title" AssociatedControlID="txtTitle" /></td>
            <td><asp:TextBox ID="txtTitle" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblAuthor" runat="server" Text="Author" AssociatedControlID="txtAuthor" /></td>
            <td><asp:TextBox ID="txtAuthor" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblISBN" runat="server" Text="ISBN" AssociatedControlID="txtISBN" /></td>
            <td><asp:TextBox ID="txtISBN" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPublicationYear" runat="server" Text="Publication Year" AssociatedControlID="txtPublicationYear" /></td>
            <td><asp:TextBox ID="txtPublicationYear" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblQuantity" runat="server" Text="Quantity" AssociatedControlID="txtQuantity" /></td>
            <td><asp:TextBox ID="txtQuantity" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCategory" runat="server" Text="Category" AssociatedControlID="ddlCategory" /></td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server">
                    <asp:ListItem Text="Fiction" Value="Fiction" />
                    <asp:ListItem Text="Non-Fiction" Value="Non-Fiction" />
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Label ID="lblMessage" runat="server" CssClass="message-label" /></td>
        </tr>
    </table>
</asp:Content>