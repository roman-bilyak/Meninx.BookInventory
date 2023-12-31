﻿using System.Collections.Generic;
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
                    new SqlParameter("@Query", specification.Query ?? ""),
                    new SqlParameter("@Limit", specification.Limit ?? 100),
                    new SqlParameter("@Offset", specification.Offset ?? 0),
                    new SqlParameter("@SortBy", specification.SortBy ?? nameof(Book.Id)),
                    new SqlParameter("@SortOrder", specification.SortOrder ?? "ASC")
                })
                .ToListAsync();
        }
    }
}