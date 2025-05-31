using Leandro.DocoSoft.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Leandro.DocoSoft.Domain.Interfaces
{
    public interface IUserRepository : ICrud<User, long>
    {
        Task UpdateAsync(User entity, CancellationToken cancellation);
    }
}