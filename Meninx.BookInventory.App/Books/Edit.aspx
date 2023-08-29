<%@ Page Title="Edit Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Meninx.BookInventory.App.Books.Edit" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2>Edit Book</h2>

        <asp:ValidationSummary ID="vsSave" runat="server" CssClass="form-error" />

        <label class="form-label" for="txtTitle">Title</label>
        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="txtTitleRequiredFieldValidator" runat="server" ControlToValidate="txtTitle" Display="None" ErrorMessage="Title is required." />
        <asp:RegularExpressionValidator ID="txtTitleRegularExpressionValidator" runat="server" ControlToValidate="txtTitle" ValidationExpression="^.{1,255}$" Display="None" ErrorMessage="Title can have a maximum of 255 characters." />

        <label class="form-label" for="txtAuthor">Author</label>
        <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="txtAuthorRequiredFieldValidator" runat="server" ControlToValidate="txtAuthor" Display="None" ErrorMessage="Author is required." />
        <asp:RegularExpressionValidator ID="txtAuthorRegularExpressionValidator" runat="server" ControlToValidate="txtAuthor" ValidationExpression="^.{1,255}$" Display="None" ErrorMessage="Author can have a maximum of 255 characters." />

        <label class="form-label" for="txtISBN">ISBN</label>
        <asp:TextBox ID="txtISBN" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="txtISBNRequiredFieldValidator" runat="server" ControlToValidate="txtISBN" Display="None" ErrorMessage="ISBN is required." />
        <asp:RegularExpressionValidator ID="txtISBNRegularExpressionValidator" runat="server" ControlToValidate="txtISBN" Display="None" ErrorMessage="Invalid ISBN format." ValidationExpression="^.{1,20}$" />

        <label class="form-label" for="txtPublicationYear">Publication Year</label>
        <asp:TextBox ID="txtPublicationYear" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="txtPublicationYearRequiredFieldValidator" runat="server" ControlToValidate="txtPublicationYear" Display="None" ErrorMessage="Publication Year is required." />
        <asp:RegularExpressionValidator ID="txtPublicationYearRegularExpressionValidator" runat="server" ControlToValidate="txtPublicationYear" Display="None" ErrorMessage="Publication Year must be a valid year." ValidationExpression="^\d{1,4}$" />

        <label class="form-label" for="txtQuantity">Quantity</label>
        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="txtQuantityRequiredFieldValidator" runat="server" ControlToValidate="txtQuantity" Display="None" ErrorMessage="Quantity is required." />
        <asp:RegularExpressionValidator ID="txtQuantityRegularExpressionValidator" runat="server" ControlToValidate="txtQuantity" Display="None" ErrorMessage="Quantity must be a non-negative integer." ValidationExpression="^\d+$" />

        <label class="form-label" for="ddlCategory">Category</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-input" />
        <asp:RequiredFieldValidator ID="ddlCategoryRequiredFieldValidator" runat="server" ControlToValidate="ddlCategory" Display="None" InitialValue="" ErrorMessage="Category is required." />

        <div class="form-button-container">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="form-button edit-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-button cancel-button" CausesValidation="false" />
        </div>
    </div>
</asp:Content>