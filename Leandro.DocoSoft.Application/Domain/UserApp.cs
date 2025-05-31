using Leandro.DocoSoft.Common.Exceptions;
using Leandro.DocoSoft.Contracts.AppObject;
using Leandro.DocoSoft.Domain.Entities;
using Leandro.DocoSoft.Domain.Interfaces;

namespace Leandro.DocoSoft.Application.Domain
{
    public class UserApp : BaseApp<UserContract, User>
    {
        private new readonly IUserRepository _repo;
        public UserApp(IUserRepository repo) : base(repo as IUserRepository)
        {
            _repo = repo;
        }

        public async Task<long> Create(UserContract user, CancellationToken cancellation)
        {
            if (user == null)
                throw new AppBaseException("User data needs to be populated.");

            if(_repo.FindAsync(x => x.Username == user.Username, cancellation).Result != null)
                throw new AppBaseException("User already exists.");

            var entity = Resolve(user);
            var createdEntityId = await _repo.AddAsync(entity, cancellation);

            return createdEntityId;
        }

        public async Task<UserContract> FindAsync(string username, CancellationToken cancellation)
        {
            var entity = await _repo.FindAsync(x => x.Username == username, cancellation);
            return Resolve(entity);
        }

        public async Task Update(UserContract user, CancellationToken cancellation)
        {
            if (user == null)
                throw new AppBaseException("User data needs to be populated.");

            if (_repo.FindAsync(x => x.Username == user.Username, cancellation).Result != null)
                throw new AppBaseException("User already exists.");

            var entity = Resolve(user);
            await _repo.UpdateAsync(entity, cancellation);
        }
    }
}
