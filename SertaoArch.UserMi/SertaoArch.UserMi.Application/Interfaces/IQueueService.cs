using SertaoArch.Contracts;

namespace SertaoArch.UserMi.Application.Interface
{
    /// <summary>
    /// Interface for services that publish messages to queues.
    /// </summary>
    public interface IQueueService
    {
        /// <summary>
        /// Publishes a message to the specified queue.
        /// </summary>
        /// <typeparam name="T">The type of the message.</typeparam>
        /// <param name="message">The message to publish.</param>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default)
            where T : ContractBase<long>;
    }
}