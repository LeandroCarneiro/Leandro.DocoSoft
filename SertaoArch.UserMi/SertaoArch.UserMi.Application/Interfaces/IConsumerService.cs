
using SertaoArch.Contracts;

namespace SertaoArch.UserMi.Application.Interfaces
{
    public interface IConsumerService
    {
        Task ProcessAsync(string message, CancellationToken cancellation);
    }
}
