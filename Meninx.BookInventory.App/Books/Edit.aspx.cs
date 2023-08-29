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

        public Edit
        (
            IRepository<Book> bookRepository, 
            IRepository<Category> categoryRepository
        )
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Guid.TryParse(Request.QueryString["id"], out Guid bookId))
                {
                    throw new Exception("Book not found");
                }

                await LoadCategories();

                await LoadBook(bookId);
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
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
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.ISBN = txtISBN.Text;
                book.PublicationYear = Convert.ToInt32(txtPublicationYear.Text);
                book.Quantity = Convert.ToInt32(txtQuantity.Text);
                book.CategoryId = Guid.Parse(ddlCategory.SelectedValue);

                await _bookRepository.UpdateAsync(book);
                await _bookRepository.SaveChangesAsync();

                Response.Redirect("List.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error editing the book: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
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