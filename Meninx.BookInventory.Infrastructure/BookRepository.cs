using System;
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
            return await _dbContext.Database.SqlQuery<Book>("spGetBooks @Query, @Limit, @Offset, @SortBy, @SortOrder",
                new SqlParameter[]
                {
                    new SqlParameter("@Query", (object)specification.Query ?? DBNull.Value),
                    new SqlParameter("@Limit", specification.Limit),
                    new SqlParameter("@Offset", specification.Offset),
                    new SqlParameter("@SortBy", (object)specification.SortBy ?? DBNull.Value),
                    new SqlParameter("@SortOrder", (object)specification.SortOrder ?? DBNull.Value)
                })
                .ToListAsync();
        }
    }
}