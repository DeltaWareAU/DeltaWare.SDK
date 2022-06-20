using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IMessageBroker : IMessagePublisher
    {
        bool Initiated { get; }

        bool IsListening { get; }

        bool IsProcessing { get; }

        void InitiateBindings();

        Task StartListeningAsync(CancellationToken cancellationToken = default);

        Task StopListeningAsync(CancellationToken cancellationToken = default);
    }
}
