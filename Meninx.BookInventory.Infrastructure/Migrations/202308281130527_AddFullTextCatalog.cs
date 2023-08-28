namespace Meninx.BookInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullTextCatalog : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE FULLTEXT CATALOG [FullTextCatalog] AS DEFAULT;", suppressTransaction: true);
        }
        
        public override void Down()
        {
            Sql("DROP FULLTEXT CATALOG [FullTextCatalog];", suppressTransaction: true);
        }
    }
}
