using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Meninx.BookInventory
{
    public class BookRepository : BaseRepository<BookInventoryDbContext, Book>
    {
        public BookRepository(BookInventoryDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<List<Book>> ListAsync(ISpecification<Book> specification, CancellationToken cancellationToken)
        {
            return await _dbContext.Database.SqlQuery<Book>("spGetBooks @PageNumber, @PageSize, @SearchText, @SortColumn, @SortOrder",
                new SqlParameter[]
                {
                    //new SqlParameter("@PageNumber", pageNumber),
                    //new SqlParameter("@PageSize", pageSize),
                    //new SqlParameter("@SearchText", searchText),
                    //new SqlParameter("@SortColumn", sortColumn),
                    //new SqlParameter("@SortOrder", sortAscending ? 1 : 0)
                })
                .ToListAsync();
        }
    }
}