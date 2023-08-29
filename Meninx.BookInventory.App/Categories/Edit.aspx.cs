using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace Meninx.BookInventory.App.Categories
{
    public partial class Edit : Page
    {
        private readonly IRepository<Category> _categoryRepository;

        public Edit
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
                if (!Guid.TryParse(Request.QueryString["id"], out Guid categoryId))
                {
                    throw new Exception("Category not found");
                }

                await LoadCategory(categoryId);
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(Request.QueryString["id"], out Guid categoryId))
            {
                throw new Exception("Category not found");
            }

            Category category = await _categoryRepository.SingleOrDefaultAsync(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            try
            {
                category.Name = txtName.Text;
                category.Description = txtDescription.Text;

                await _categoryRepository.UpdateAsync(category);
                await _categoryRepository.SaveChangesAsync();

                Response.Redirect("List.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error editing the category: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

        #region helper methods

        private async Task LoadCategory(Guid id)
        {
            Category category = await _categoryRepository.SingleOrDefaultAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            txtName.Text = category.Name;
            txtDescription.Text = category.Description;
        }

        #endregion
    }
}