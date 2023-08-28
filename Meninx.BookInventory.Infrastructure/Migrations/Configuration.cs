using System.Data.Entity.Migrations;

namespace Meninx.BookInventory.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BookInventoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookInventoryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
