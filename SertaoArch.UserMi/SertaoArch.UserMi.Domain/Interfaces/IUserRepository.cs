using SertaoArch.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace SertaoArch.Domain.Interfaces
{
    public interface IUserRepository : ICrud<User, long>
    {
        Task UpdateAsync(User entity, CancellationToken cancellation);
    }
}