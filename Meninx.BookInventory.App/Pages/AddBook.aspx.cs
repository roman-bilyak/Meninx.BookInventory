using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Pages
{
    public partial class AddBook : Page
    {
        private readonly IRepository<Book> _bookRepository;

        public AddBook()
        {
            _bookRepository = new BookRepository(new BookInventoryDbContext());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: load list of categories
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

                await _bookRepository.AddAsync(book, default);
                await _bookRepository.SaveChangesAsync(default);

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
    }
}