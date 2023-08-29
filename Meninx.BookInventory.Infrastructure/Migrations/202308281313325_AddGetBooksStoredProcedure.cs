namespace Meninx.BookInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGetBooksStoredProcedure : DbMigration
    {
        public override void Up()
        {
            Sql(
@"CREATE PROCEDURE [spGetBooks]
    @Query NVARCHAR(100) = NULL,
    @Limit INT = 100,
    @Offset INT = 0,
    @SortBy NVARCHAR(50) = 'Id',
    @SortOrder NVARCHAR(4) = 'asc'
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch sorted data with limit and offset
    SELECT
        Id, Title, Author, ISBN, PublicationYear, Quantity, CategoryId
    FROM (
        SELECT
            Id, Title, Author, ISBN, PublicationYear, Quantity, CategoryId,
            ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum
        FROM [tblBooks]
        WHERE @Query IS NULL OR CONTAINS((Title, Author, ISBN), @Query)
    ) AS RowNumbered
    WHERE RowNum BETWEEN @Offset + 1 AND @Offset + @Limit
    ORDER BY RowNum;
END
");
        }
        
        public override void Down()
        {
            Sql("DROP PROCEDURE [spGetBooks]");
        }
    }
}
