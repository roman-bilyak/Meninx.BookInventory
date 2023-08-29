namespace Meninx.BookInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableCategoryCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblBooks", "CategoryId", "dbo.tblCategories");
            AddForeignKey("dbo.tblBooks", "CategoryId", "dbo.tblCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblBooks", "CategoryId", "dbo.tblCategories");
            AddForeignKey("dbo.tblBooks", "CategoryId", "dbo.tblCategories", "Id", cascadeDelete: true);
        }
    }
}
