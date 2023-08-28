using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;

namespace Meninx.BookInventory.App.Pages
{
    public partial class AddBook : Page
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Category> _categoryRepository;

        public AddBook()
        {
            _bookRepository = new BookRepository(new BookInventoryDbContext());
            _categoryRepository = new BaseRepository<BookInventoryDbContext, Category>(new BookInventoryDbContext());
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadCategories();
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    ISBN = txtISBN.Text,
                    PublicationYear = Convert.ToInt32(txtPublicationYear.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    CategoryId = Guid.Parse(ddlCategory.SelectedValue)
                };

                await _bookRepository.AddAsync(book);
                await _bookRepository.SaveChangesAsync();

                lblMessage.Text = "Book added successfully!";
                lblMessage.CssClass = "success-message";

                //TODO: Redirect to Home Page or perform other actions
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error adding the book: " + ex.Message;
                lblMessage.CssClass = "error-message";
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

        #endregion
    }
}