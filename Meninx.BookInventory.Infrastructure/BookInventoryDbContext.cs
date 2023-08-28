using System.Data.Entity;

namespace Meninx.BookInventory
{
    public class BookInventoryDbContext : DbContext
    {
        public BookInventoryDbContext() : base("name=BookInventoryDbContext")
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
        }
    }
}
