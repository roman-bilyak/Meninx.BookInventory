using System;
using System.Web.UI;

namespace Meninx.BookInventory.App
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Books/List.aspx");
        }
    }
}