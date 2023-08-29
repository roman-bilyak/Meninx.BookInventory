using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Meninx.BookInventory.App.Books
{
    public partial class List : Page
    {
        private readonly IReadRepository<Book> _bookRepository;

        public List
        (
            IReadRepository<Book> bookRepository
        )
        {
            _bookRepository = bookRepository;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadBooks();
            }
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadBooks();
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
            string query = txtSearch.Text.Trim();

            ISpecification<Book> specification = new Specification<Book>()
                .ApplyQuery(query);
            gwBooks.DataSource = await _bookRepository.ListAsync(specification);
            gwBooks.DataKeyNames = new string[] { nameof(Book.Id) };
            gwBooks.DataBind();
        }

        #endregion
    }
}