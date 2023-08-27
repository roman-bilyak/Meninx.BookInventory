﻿using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Pages
{
    public partial class DeleteBook : Page
    {
        private readonly IRepository<Book> _bookRepository;

        public DeleteBook
        (
            IRepository<Book> bookRepository
        )
        {
            _bookRepository = bookRepository;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid bookId = Guid.Parse(Request.QueryString["id"]);
                Book book = await _bookRepository.SingleOrDefaultAsync(bookId, default);
                if (book != null)
                {
                    lblTitleValue.Text = book.Title;
                    lblAuthorValue.Text = book.Author;
                    lblISBNValue.Text = book.ISBN;
                    lblPublicationYearValue.Text = book.PublicationYear.ToString();
                    lblQuantityValue.Text = book.Quantity.ToString();
                    lblCategoryValue.Text = book.Category.Name;
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            Guid bookId = Guid.Parse(Request.QueryString["id"]);
            Book book = await _bookRepository.SingleOrDefaultAsync(bookId, default);

            if (book != null)
            {
                await _bookRepository.DeleteAsync(book, default);
                await _bookRepository.SaveChangesAsync(default);

                lblMessage.Text = "Book deleted successfully!";
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}