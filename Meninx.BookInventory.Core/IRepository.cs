﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Meninx.BookInventory
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>, IReadRepository<TEntity>
        where TEntity : Entity<Guid>
    {
    }

    public interface IRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}