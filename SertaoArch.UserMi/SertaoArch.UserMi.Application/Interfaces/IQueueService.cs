using SertaoArch.Contracts;
using SertaoArch.UserMi.Application.Interfaces;

namespace SertaoArch.UserMi.Application.Interface
{
    public interface IQueueService
    {
        Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default)
            where T : ContractBase<long>;

        Task StartListeningAsync(string queueName, string consumerName, IConsumerService consumerService, CancellationToken cancellation);
    }
}