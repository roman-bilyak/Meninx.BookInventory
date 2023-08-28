using System.Data.Entity.Migrations;

namespace Meninx.BookInventory.Migrations
{
    public partial class spGetBooks : DbMigration
    {
        public override void Up()
        {
            Sql(
@"CREATE PROCEDURE spGetBooks
    @Query NVARCHAR(100) = NULL,
    @Limit INT,
    @Offset INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch sorted data with limit and offset
    SELECT
        Id, Title, Author, ISBN, PublicationYear, Quantity
    FROM (
        SELECT
            Id, Title, Author, ISBN, PublicationYear, Quantity,
            ROW_NUMBER() OVER (ORDER BY Id DESC) AS RowNum
        FROM Books
        WHERE @Query IS NULL OR CONTAINS((Title, Author, ISBN, PublicationYear, Quantity), @Query)
    ) AS RowNumbered
    WHERE RowNum BETWEEN @Offset + 1 AND @Offset + @Limit
    ORDER BY RowNum;
END");
        }
        
        public override void Down()
        {
            Sql("DROP PROCEDURE spGetBooks");
        }
    }
}
