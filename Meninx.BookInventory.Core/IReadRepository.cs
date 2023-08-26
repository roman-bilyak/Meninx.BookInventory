using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Meninx.BookInventory
{
    public interface IReadRepository<TEntity> : IReadRepository<TEntity, Guid>
        where TEntity : Entity<Guid>
    {
    }

    public interface IReadRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        Task<TEntity> SingleOrDefaultAsync(TId id, CancellationToken cancellationToken);

        Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
    }
}