using SertaoArch.UserMi.Domain;
using SertaoArch.UserMi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SertaoArch.UserMi.Repository
{
    public abstract class AppRepository<TEntity> : ICrud<TEntity, long> where TEntity : EntityBase<long>
    {
        protected readonly IDbContext _uow;

        public AppRepository(IDbContext dbContext)
        {
            _uow = dbContext;         
        }

        public async Task<long> AddAsync(TEntity obj, CancellationToken cancellationToken)
        {
            obj.CreatedDate = DateTime.Now;

            await _uow.GetEntity<TEntity>().AddAsync(obj, cancellationToken);

            await _uow.SaveChangesAsync(cancellationToken);
            return obj.Id;
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation)
        {
            return await _uow.GetEntity<TEntity>().FirstOrDefaultAsync(expression, cancellation);
        }

        public async Task<TEntity> Get(long id, CancellationToken cancellation)
        {
            return await _uow.GetEntity<TEntity>().FirstOrDefaultAsync(x=>x.Id == id, cancellation);
        }

        public async Task<IReadOnlyCollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation)
        {
            return await _uow.GetEntity<TEntity>().Where(expression).ToListAsync(cancellation);
        }
    }
}