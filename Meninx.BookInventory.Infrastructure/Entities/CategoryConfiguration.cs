using System.Data.Entity.ModelConfiguration;

namespace Meninx.BookInventory
{
    internal class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("tblCategories");

            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired().HasMaxLength(Category.NameMaxLength);
            Property(x => x.Description).HasMaxLength(Category.DescriptionMaxLength);
        }
    }
}