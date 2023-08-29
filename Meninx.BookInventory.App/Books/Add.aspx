<%@ Page Title="Add Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Meninx.BookInventory.App.Books.Add" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Add Book</h2>

        <label class="form-label" for="txtTitle">Title</label>
        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-input" />

        <label class="form-label" for="txtAuthor">Author</label>
        <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-input" />

        <label class="form-label" for="txtISBN">ISBN</label>
        <asp:TextBox ID="txtISBN" runat="server" CssClass="form-input" />

        <label class="form-label" for="txtPublicationYear">Publication Year</label>
        <asp:TextBox ID="txtPublicationYear" runat="server" CssClass="form-input" />

        <label class="form-label" for="txtQuantity">Quantity</label>
        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-input" />

        <label class="form-label" for="ddlCategory">Category</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-input" />

        <asp:Label ID="lblMessage" runat="server" CssClass="form-error" />

        <div class="form-button-container">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="form-button add-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" />
        </div>
    </div>
</asp:Content>