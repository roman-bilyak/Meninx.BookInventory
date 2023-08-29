<%@ Page Title="Edit Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Meninx.BookInventory.App.Categories.Edit" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Edit Category</h2>

        <label class="form-label" for="txtName">Name</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-input" />

        <label class="form-label" for="txtAuthor">Description</label>
        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-input" />

        <asp:Label ID="lblMessage" runat="server" CssClass="form-error" />

        <div class="form-button-container">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="form-button edit-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" />
        </div>
    </div>
</asp:Content>