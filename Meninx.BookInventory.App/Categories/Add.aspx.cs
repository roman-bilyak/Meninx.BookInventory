using System;
using System.Web.UI;

namespace Meninx.BookInventory.App.Categories
{
    public partial class Add : Page
    {
        private readonly IRepository<Category> _categoryRepository;

        public Add
        (
            IRepository<Category> categoryRepository
        )
        {
            _categoryRepository = categoryRepository;
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Category category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = txtName.Text,
                    Description = txtDescription.Text
                };

                await _categoryRepository.AddAsync(category);
                await _categoryRepository.SaveChangesAsync();

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
    }
}