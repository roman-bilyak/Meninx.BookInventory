<%@ Page Title="All Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Meninx.BookInventory.App.Pages.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>All Books</h1>
    <table>
        <tr>
            <th>Title</th>
            <th>Author</th>
            <th>ISBN</th>
            <th>Publication Year</th>
            <th>Quantity</th>
        </tr>
        <asp:Repeater ID="bookRepeater" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Title") %></td>
                    <td><%# Eval("Author") %></td>
                    <td><%# Eval("ISBN") %></td>
                    <td><%# Eval("PublicationYear") %></td>
                    <td><%# Eval("Quantity") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>