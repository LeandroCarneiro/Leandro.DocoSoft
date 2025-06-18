using SertaoArch.UserMi.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace SertaoArch.UserMi.Domain.Interfaces
{
    public interface IUserRepository : ICrud<User, long>
    {
        Task UpdateAsync(User entity, CancellationToken cancellation);
    }
}