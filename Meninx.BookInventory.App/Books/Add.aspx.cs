using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;

namespace Meninx.BookInventory.App.Books
{
    public partial class Add : Page
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IReadRepository<Category> _categoryRepository;

        public Add
        (
            IRepository<Book> bookRepository,
            IReadRepository<Category> categoryRepository
        )
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
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

                Response.Redirect("List.aspx");
            }
            catch (Exception ex)
            {
                vsSave.HeaderText = ex.Message;
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

        #endregion
    }
}