using Leandro.DocoSoft.Domain.Entities;
using Leandro.DocoSoft.Domain.Interfaces;
using Leandro.DocoSoft.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Leandro.DocoSoft.Business.Domain
{
    public class UserRepository : AppRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<User> FindAsync(Expression<Func<User, bool>> value, CancellationToken cancellation)
        {
            var obj = await _uow.TblUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(value, cancellation);

            return obj;
        }

        public async Task UpdateAsync(User user, CancellationToken cancellation)
        {
            user.ModifiedDate = DateTime.Now;

            await _uow.TblUsers
                .Where(x => x.Id == user.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.FirstName, user.FirstName)
                    .SetProperty(x => x.LastName, user.LastName)
                    .SetProperty(x => x.Age, user.Age)
                    .SetProperty(x => x.Username, user.Username)
                    .SetProperty(x => x.Password, user.Password)
                    .SetProperty(x => x.ModifiedDate, DateTime.UtcNow),
                    cancellation);

            await _uow.SaveChangesAsync(cancellation);
        }
    }
}