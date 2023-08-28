<%@ Page Title="Edit Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="Meninx.BookInventory.App.Pages.EditBook" Async="true" %>

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
                    <asp:ListItem Text="Fiction" Value="5c365c44-4007-4473-b22b-5b88358bf475" />
                    <asp:ListItem Text="Non-Fiction" Value="927c8608-fe6b-4a81-856d-7bb0e8986794" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Label ID="lblMessage" runat="server" CssClass="message-label" /></td>
        </tr>
    </table>
</asp:Content>