using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Meninx.BookInventory.App.Categories
{
    public partial class List : Page
    {
        private readonly IReadRepository<Category> _categoryRepository;

        public List
        (
            IReadRepository<Category> categoryRepository
        )
        {
            _categoryRepository = categoryRepository;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadCategories();
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add.aspx");
        }

        protected void gwCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string categoryId = gwCategories.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect($"Edit.aspx?Id={categoryId}");
        }

        protected void gwCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string categoryId = gwCategories.DataKeys[e.RowIndex].Value.ToString();
            Response.Redirect($"Delete.aspx?Id={categoryId}");
        }

        #region helper methods

        private async Task LoadCategories()
        {
            ISpecification<Category> specification = new Specification<Category>();
            gwCategories.DataSource = await _categoryRepository.ListAsync(specification);
            gwCategories.DataKeyNames = new string[] { nameof(Category.Id) };
            gwCategories.DataBind();
        }

        #endregion
    }
}