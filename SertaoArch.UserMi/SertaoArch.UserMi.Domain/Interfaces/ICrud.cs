using SertaoArch.UserMi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SertaoArch.UserMi.Domain.Interfaces
{
    public interface ICrud<T, TIdType>
    {
        Task<long> AddAsync(T obj, CancellationToken cancellationToken);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation);
        Task<T> Get(long id, CancellationToken cancellation);
        Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation);
    }
}
