using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Books
{
    public partial class Delete : Page
    {
        private readonly IRepository<Book> _bookRepository;

        public Delete
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
                if (!Guid.TryParse(Request.QueryString["id"], out Guid bookId))
                {
                    throw new Exception("Book not found");
                }

                Book book = await _bookRepository.SingleOrDefaultAsync(bookId);
                if (book == null)
                {
                    throw new Exception("Book not found");
                }

                lblTitleValue.Text = book.Title;
                lblAuthorValue.Text = book.Author;
                lblISBNValue.Text = book.ISBN;
                lblPublicationYearValue.Text = book.PublicationYear.ToString();
                lblQuantityValue.Text = book.Quantity.ToString();
                lblCategoryValue.Text = book.Category.Name;

            }
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(Request.QueryString["id"], out Guid bookId))
            {
                throw new Exception("Book not found");
            }

            Book book = await _bookRepository.SingleOrDefaultAsync(bookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            try
            {
                await _bookRepository.DeleteAsync(book);
                await _bookRepository.SaveChangesAsync();

                Response.Redirect("List.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting the book: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}