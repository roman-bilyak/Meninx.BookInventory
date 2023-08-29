using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Categories
{
    public partial class Delete : Page
    {
        private readonly IRepository<Category> _categoryRepository;

        public Delete
        (
            IRepository<Category> categoryRepository
        )
        {
            _categoryRepository = categoryRepository;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Guid.TryParse(Request.QueryString["id"], out Guid bookId))
                {
                    throw new Exception("Category not found");
                }

                Category category = await _categoryRepository.SingleOrDefaultAsync(bookId);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }

                lblNameValue.Text = category.Name;
                lblDescriptionValue.Text = category.Description;
            }
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(Request.QueryString["id"], out Guid bookId))
            {
                throw new Exception("Category not found");
            }

            Category category = await _categoryRepository.SingleOrDefaultAsync(bookId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            try
            {
                await _categoryRepository.DeleteAsync(category);
                await _categoryRepository.SaveChangesAsync();

                Response.Redirect("List.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting the category: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}