<%@ Page Title="All Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Meninx.BookInventory.App.Categories.List" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="search-bar">
        <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" CssClass="button add-button" />
    </div>

    <asp:GridView ID="gwCategories" runat="server" AutoGenerateColumns="False" CssClass="search-result"
        OnRowEditing="gwCategories_RowEditing"
        OnRowDeleting="gwCategories_RowDeleting"
        ShowHeaderWhenEmpty="true"
        EmptyDataText="No data"
        EmptyDataRowStyle-HorizontalAlign="Center">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="35%" />
            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="55%" />
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