using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;

namespace Meninx.BookInventory.App.Books
{
    public partial class Edit : Page
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Category> _categoryRepository;

        public Edit()
        {
            _bookRepository = new BookRepository(new BookInventoryDbContext());
            _categoryRepository = new BaseRepository<BookInventoryDbContext, Category>(new BookInventoryDbContext());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadCategories();

                Guid bookId = Guid.Parse(Request.QueryString["id"]);
                await LoadBook(bookId);
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            Guid bookId = Guid.Parse(Request.QueryString["id"]);
            Book book = await _bookRepository.SingleOrDefaultAsync(bookId);
            if (book != null)
            {
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.ISBN = txtISBN.Text;
                book.PublicationYear = Convert.ToInt32(txtPublicationYear.Text);
                book.Quantity = Convert.ToInt32(txtQuantity.Text);
                book.CategoryId = Guid.Parse(ddlCategory.SelectedValue);

                await _bookRepository.UpdateAsync(book);
                await _bookRepository.SaveChangesAsync();

                lblMessage.Text = "Book updated successfully!";
            }
        }

        #region helper methods

        private async Task LoadCategories()
        {
            List<Category> categories = await _categoryRepository.ListAsync(new Specification<Category>() { });

            ddlCategory.DataSource = categories;

            ddlCategory.DataTextField = nameof(Category.Name);
            ddlCategory.DataValueField = nameof(Category.Id);

            ddlCategory.DataBind();
        }

        private async Task LoadBook(Guid id)
        {
            Book book = await _bookRepository.SingleOrDefaultAsync(id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtISBN.Text = book.ISBN;
            txtPublicationYear.Text = book.PublicationYear.ToString();
            txtQuantity.Text = book.Quantity.ToString();
            ddlCategory.SelectedValue = book.CategoryId.ToString();
        }

        #endregion
    }
}