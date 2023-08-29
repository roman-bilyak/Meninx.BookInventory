<%@ Page Title="Add Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Meninx.BookInventory.App.Categories.Add" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Add Category</h2>

        <asp:ValidationSummary ID="vsSave" runat="server" CssClass="form-error" />

        <label class="form-label" for="txtName">Name</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="txtNameRequiredFieldValidator" runat="server" ControlToValidate="txtName" Display="None" ErrorMessage="Name is required." />
        <asp:RegularExpressionValidator ID="txtNameRegularExpressionValidator" runat="server" ControlToValidate="txtName" ValidationExpression="^.{1,100}$" Display="None" ErrorMessage="Name can have a maximum of 100 characters." />

        <label class="form-label" for="txtDescription">Description</label>
        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-input" />
        <asp:RegularExpressionValidator ID="txtDescriptionRegularExpressionValidator" runat="server" ControlToValidate="txtDescription" ValidationExpression="^.{1,255}$" Display="None" ErrorMessage="Description can have a maximum of 255 characters." />

        <div class="form-button-container">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="form-button add-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" />
        </div>
    </div>
</asp:Content>