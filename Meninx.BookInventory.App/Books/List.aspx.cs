﻿using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Meninx.BookInventory.App.Books
{
    public partial class List : Page
    {
        private readonly IReadRepository<Book> _bookRepository;

        public List()
        {
            _bookRepository = new BookRepository(new BookInventoryDbContext());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadBooks();
            }
        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Books/Add.aspx");
        }

        protected void gwBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string bookId = gwBooks.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect($"~/Books/Edit.aspx?Id={bookId}");
        }

        protected void gwBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string bookId = gwBooks.DataKeys[e.RowIndex].Value.ToString();
            Response.Redirect($"~/Books/Delete.aspx?Id={bookId}");
        }

        #region helper methods

        private async Task LoadBooks()
        {
            gwBooks.DataSource = await _bookRepository.ListAsync(new Specification<Book>() { });
            gwBooks.DataKeyNames = new string[] { nameof(Book.Id) };
            gwBooks.DataBind();
        }

        #endregion
    }
}