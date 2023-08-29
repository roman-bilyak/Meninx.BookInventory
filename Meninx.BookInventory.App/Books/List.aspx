<%@ Page Title="All Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Meninx.BookInventory.App.Books.List" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="search-bar">
        <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter search term" CssClass="search-input" />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="button search-button" />
        <asp:Button ID="btnAddBook" runat="server" Text="Add Book" OnClick="btnAddBook_Click" CssClass="button add-button" />
    </div>

    <asp:GridView ID="gwBooks" runat="server" AutoGenerateColumns="False" CssClass="search-result"
        OnRowEditing="gwBooks_RowEditing"
        OnRowDeleting="gwBooks_RowDeleting"
        ShowHeaderWhenEmpty="true"
        EmptyDataText="No data"
        EmptyDataRowStyle-HorizontalAlign="Center">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-Width="40%" />
            <asp:BoundField DataField="Author" HeaderText="Author" ItemStyle-Width="20%" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" ItemStyle-Width="15%" />
            <asp:BoundField DataField="PublicationYear" HeaderText="Publication Year" ItemStyle-Width="10%" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity"  ItemStyle-Width="5%" />
            <asp:TemplateField ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:Button CssClass="button edit-button" runat="server" Text="Edit" CommandName="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:Button CssClass="button delete-button" runat="server" Text="Delete" CommandName="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>