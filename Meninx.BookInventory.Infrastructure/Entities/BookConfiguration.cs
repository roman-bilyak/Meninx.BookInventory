using System.Data.Entity.ModelConfiguration;

namespace Meninx.BookInventory
{
    internal class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            ToTable("tblBooks");

            HasKey(x => x.Id);

            Property(x => x.Title).IsRequired().HasMaxLength(Book.TitleMaxLength);
            Property(x => x.Author).IsRequired().HasMaxLength(Book.AuthorMaxLength);
            Property(x => x.ISBN).IsRequired().HasMaxLength(Book.ISBNMaxLength);
            Property(x => x.PublicationYear).IsRequired();
            Property(x => x.Quantity).IsRequired();
            Property(x => x.CategoryId).IsRequired();

            HasRequired(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
        }
    }
}