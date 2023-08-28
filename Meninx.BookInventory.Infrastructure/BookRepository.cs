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
            return await _dbContext.Database.SqlQuery<Book>("spGetBooks @Query, @Limit, @Offset",
                new SqlParameter[]
                {
                    new SqlParameter("@Query", "net"),
                    new SqlParameter("@Limit", "10"),
                    new SqlParameter("@Offset", "0")
                })
                .ToListAsync();
        }
    }
}