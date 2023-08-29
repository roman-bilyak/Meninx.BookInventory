using Meninx.BookInventory.Migrations;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Meninx.BookInventory.App
{
    public static  class InfrastructureConfig
    {
        public static void Configure()
        {
            BookInventoryDbContextMigrationsConfiguration configuration = new BookInventoryDbContextMigrationsConfiguration();
            DbMigrator migrator = new DbMigrator(configuration);
            IEnumerable<string> pendingMigrations = migrator.GetPendingMigrations();
            foreach (string migration in pendingMigrations)
            {
                migrator.Update(migration);
            }
        }
    }
}