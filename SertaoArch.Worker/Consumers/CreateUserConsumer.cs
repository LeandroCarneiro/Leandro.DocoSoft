using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SertaoArch.Contracts.AppObject;
using SertaoArch.Domain.Entities;

namespace SertaoArch.Worker.Comsumers
{
    public class CreateUserConsumer : Consumer<UserContract>
    {
        private IMongoCollection<UserContract> _usersCollection;

        public CreateUserConsumer(IConfiguration configuration, ILogger<CreateUserConsumer> logger, IMongoClient client) : base(configuration, logger, client, "user_created")
        {
            _usersCollection = _database.GetCollection<UserContract>(nameof(UserContract));
        }

        public override async Task Execute(UserContract message, CancellationToken cancellation)
        {
            await _usersCollection.InsertOneAsync(message, options: null, cancellationToken: cancellation);
            await PublishAsync(new AckMessage() { Message = "User was imported successfully!" }, "user_imported", cancellation);
        }   
    }
}