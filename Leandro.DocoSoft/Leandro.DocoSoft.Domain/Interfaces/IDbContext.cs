using System;
using System.Threading.Tasks;
using System.Threading;
using Leandro.DocoSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leandro.DocoSoft.Domain.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<User> TblUsers { get; set; }
        DbSet<TEntity> GetEntity<TEntity>() where TEntity : EntityBase<long>;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
