namespace Meninx.BookInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullTextIndex : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE FULLTEXT INDEX ON [dbo].[tblBooks] ([Title], [Author], [ISBN]) KEY INDEX [PK_dbo.tblBooks];", suppressTransaction: true);
        }
        
        public override void Down()
        {
            Sql("DROP FULLTEXT INDEX ON [dbo].[tblBooks];", suppressTransaction: true);
        }
    }
}
