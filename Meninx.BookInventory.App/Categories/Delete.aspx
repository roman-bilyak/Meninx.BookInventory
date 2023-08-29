<%@ Page Title="Delete Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="Meninx.BookInventory.App.Categories.Delete" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Confirm Deletion</h2>
        <p>Are you sure you want to delete the following category?</p>

        <label class="form-label" for="lblName">Name</label>
        <asp:Label ID="lblNameValue" runat="server" CssClass="form-value" />

        <label class="form-label" for="lblDescription">Description</label>
        <asp:Label ID="lblDescriptionValue" runat="server" CssClass="form-value" />

        <asp:Label ID="lblMessage" runat="server" CssClass="form-error" />

        <div class="form-button-container">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="form-button delete-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" />
        </div>
    </div>
</asp:Content>
