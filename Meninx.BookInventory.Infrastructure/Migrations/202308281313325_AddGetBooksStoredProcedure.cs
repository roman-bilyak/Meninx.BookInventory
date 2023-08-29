namespace Meninx.BookInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGetBooksStoredProcedure : DbMigration
    {
        public override void Up()
        {
            Sql(
@"CREATE PROCEDURE [dbo].[spGetBooks]
    @Query NVARCHAR(100) = NULL,
    @Limit INT = 100,
    @Offset INT = 0,
    @SortBy NVARCHAR(50) = 'Id',
    @SortOrder NVARCHAR(4) = 'ASC'
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @WhereCondition NVARCHAR(MAX) = N'1 = 1'
    IF @Query IS NOT NULL AND LTRIM(RTRIM(@Query)) <> ''
    BEGIN
        SET @WhereCondition = N'FREETEXT(([Title], [Author], [ISBN]), @Query)'
    END

    DECLARE @OrderBy NVARCHAR(100)
    SET @OrderBy = QUOTENAME(@SortBy) + ' ' + @SortOrder

    DECLARE @SQL NVARCHAR(MAX)
    SET @SQL = N'
        SELECT *
        FROM (
            SELECT
                *,
                ROW_NUMBER() OVER (ORDER BY ' + @OrderBy + N') AS [RowNum]
            FROM [tblBooks]
            WHERE ' + @WhereCondition + N'
        ) AS [RowNumbered]
        WHERE [RowNum] BETWEEN @Offset + 1 AND @Offset + @Limit
        ORDER BY [RowNum]'

    EXEC sp_executesql
        @SQL,
        N'@Query NVARCHAR(100), @Offset INT, @Limit INT',
        @Query, @Offset, @Limit
END");
        }
        
        public override void Down()
        {
            Sql("DROP PROCEDURE [spGetBooks]");
        }
    }
}
