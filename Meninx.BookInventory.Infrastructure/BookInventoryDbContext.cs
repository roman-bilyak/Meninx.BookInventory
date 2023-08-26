using System.Data.Entity;

namespace Meninx.BookInventory
{
    internal class BookInventoryDbContext : DbContext
    {
        public BookInventoryDbContext() : base("name=BookInventoryDbContext")
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
