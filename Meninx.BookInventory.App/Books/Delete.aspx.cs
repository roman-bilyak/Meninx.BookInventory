using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Books
{
    public partial class Delete : Page
    {
        private readonly IRepository<Book> _bookRepository;

        public Delete()
        {
            _bookRepository = new BookRepository(new BookInventoryDbContext());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid bookId = Guid.Parse(Request.QueryString["id"]);
                Book book = await _bookRepository.SingleOrDefaultAsync(bookId);
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
            Book book = await _bookRepository.SingleOrDefaultAsync(bookId);

            if (book != null)
            {
                await _bookRepository.DeleteAsync(book);
                await _bookRepository.SaveChangesAsync();

                lblMessage.Text = "Book deleted successfully!";
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}