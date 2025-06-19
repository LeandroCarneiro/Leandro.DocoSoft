using Microsoft.Extensions.Configuration;
using SertaoArch.UserMi.Application.Interface;
using SertaoArch.Common.Exceptions;
using SertaoArch.Common.Utils;
using SertaoArch.Contracts.AppObject;
using SertaoArch.Domain.Entities;
using SertaoArch.Domain.Interfaces;

namespace SertaoArch.UserMi.Application.Domain
{
    public class UserApp : BaseApp<UserContract, User>
    {
        private new readonly IUserRepository _repo;
        private readonly IConfiguration _configuration;
        private IQueueService _queueService;
        private readonly string _queueUserCreated;

        public UserApp(IUserRepository repo, IQueueService queueService, IConfiguration configuration) : base(repo)
        {
            _queueUserCreated = configuration["RabbitMQ:Queues:user_created"] ?? throw new ArgumentException("Queue name for user_created not found in configuration.");

            _configuration = configuration;
            _queueService = queueService;
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

            await _queueService.PublishAsync(user, _queueUserCreated, cancellation);

            return createdEntityId;
        }

        public async Task<UserContract> FindAsync(string username, CancellationToken cancellation)
        {
            var entity = await _repo.FindAsync(x => x.Username == username, cancellation);
            return Resolve(entity);
        }

        public async Task RepublishAsync(string username, CancellationToken cancellation)
        {
            var entity = await _repo.FindAsync(x => x.Username == username, cancellation);

            if (entity == null)
                throw new AppBaseException("User not found.");

            var contract = Resolve(entity);

            await _queueService.PublishAsync(contract, _queueUserCreated, cancellation);
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
