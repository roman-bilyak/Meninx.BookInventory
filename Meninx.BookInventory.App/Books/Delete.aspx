<%@ Page Title="Delete Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="Meninx.BookInventory.App.Books.Delete" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Confirm Deletion</h2>
        <p>Are you sure you want to delete the following book?</p>

        <label class="form-label" for="lblTitle">Title</label>
        <asp:Label ID="lblTitleValue" runat="server" CssClass="form-value" />

        <label class="form-label" for="lblAuthor">Author</label>
        <asp:Label ID="lblAuthorValue" runat="server" CssClass="form-value" />

        <label class="form-label" for="lblISBN">ISBN</label>
        <asp:Label ID="lblISBNValue" runat="server" CssClass="form-value" />

        <label class="form-label" for="lblPublicationYear">Publication Year</label>
        <asp:Label ID="lblPublicationYearValue" runat="server" CssClass="form-value" />

        <label class="form-label" for="lblQuantity">Quantity</label>
        <asp:Label ID="lblQuantityValue" runat="server" CssClass="form-value" />

        <label class="form-label" for="lblCategory">Category</label>
        <asp:Label ID="lblCategoryValue" runat="server" CssClass="form-value" />

        <asp:Label ID="lblMessage" runat="server" CssClass="form-error" />

        <div class="form-button-container">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="form-button delete-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" />
        </div>
    </div>
</asp:Content>
