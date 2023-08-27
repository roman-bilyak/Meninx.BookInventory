using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Pages
{
    public partial class EditBook : Page
    {
        private readonly IRepository<Book> _bookRepository;

        public EditBook
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
                    txtTitle.Text = book.Title;
                    txtAuthor.Text = book.Author;
                    txtISBN.Text = book.ISBN;
                    txtPublicationYear.Text = book.PublicationYear.ToString();
                    txtQuantity.Text = book.Quantity.ToString();
                    ddlCategory.SelectedValue = book.CategoryId.ToString();
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            Guid bookId = Guid.Parse(Request.QueryString["id"]);
            Book book = await _bookRepository.SingleOrDefaultAsync(bookId, default);
            if (book != null)
            {
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.ISBN = txtISBN.Text;
                book.PublicationYear = Convert.ToInt32(txtPublicationYear.Text);
                book.Quantity = Convert.ToInt32(txtQuantity.Text);
                book.CategoryId = Guid.Parse(ddlCategory.SelectedValue);

                await _bookRepository.UpdateAsync(book, default);

                lblMessage.Text = "Book updated successfully!";
            }
        }
    }
}