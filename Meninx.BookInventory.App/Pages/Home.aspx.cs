using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Meninx.BookInventory.App.Pages
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            //List<Book> books = BookRepository.GetAllBooks(); // Implement this method to retrieve all books
            //bookRepeater.DataSource = books;
            bookRepeater.DataBind();
        }
    }
}