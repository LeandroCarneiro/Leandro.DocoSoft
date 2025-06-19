
namespace SertaoArch.QueueServiceRMQ.Interfaces
{
    public interface IConsumerService
    {
        Task ReadMessgaesAsync(CancellationToken cancellation);
    }
}
