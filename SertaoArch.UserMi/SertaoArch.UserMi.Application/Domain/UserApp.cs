using SertaoArch.UserMi.Common.Exceptions;
using SertaoArch.UserMi.Common.Utils;
using SertaoArch.UserMi.Contracts.AppObject;
using SertaoArch.UserMi.Domain.Entities;
using SertaoArch.UserMi.Domain.Interfaces;

namespace SertaoArch.UserMi.Application.Domain
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

            if((await _repo.FindAsync(x => x.Username == user.Username, cancellation)) != null)
                throw new AppBaseException("User already exists.");

            var entity = Resolve(user);
            entity.Password = entity.Password.Encrypt();

            var createdEntityId = await _repo.AddAsync(entity, cancellation);

            return createdEntityId;
        }

        public async Task<UserContract> FindAsync(string username, CancellationToken cancellation)
        {
            var entity = await _repo.FindAsync(x => x.Username == username, cancellation);
            return Resolve(entity);
        }

        public async Task Update(string username, UserContract user, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(username))
                throw new AppBaseException("Username needs to be provided.");

            var oldUser = await _repo.FindAsync(x => x.Username == username, cancellation);

            if (oldUser == null)
                throw new AppBaseException("User already exists.");

            var entity = Resolve(user);
            entity.Id = oldUser!.Id; 
            
            await _repo.UpdateAsync(entity, cancellation);
        }
    }
}
