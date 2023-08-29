<%@ Page Title="Add Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Meninx.BookInventory.App.Categories.Add" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Add Category</h2>

        <label class="form-label" for="txtTitle">Name</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-input" />

        <label class="form-label" for="txtAuthor">Description</label>
        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-input" />

        <asp:Label ID="lblMessage" runat="server" CssClass="form-error" />

        <div class="form-button-container">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="form-button add-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" />
        </div>
    </div>
</asp:Content>