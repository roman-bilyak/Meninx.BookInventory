<%@ Page Title="All Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Meninx.BookInventory.App.Books.List" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnAddBook" runat="server" Text="Add Book" OnClick="btnAddBook_Click" />

    <asp:GridView ID="gwBooks" runat="server" AutoGenerateColumns="False"
        OnRowEditing="gwBooks_RowEditing"
        OnRowDeleting="gwBooks_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="PublicationYear" HeaderText="Publication Year" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>