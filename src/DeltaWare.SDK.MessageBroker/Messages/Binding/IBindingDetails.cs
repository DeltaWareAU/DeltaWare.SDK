using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Messages.Binding
{
    public interface IBindingDetails
    {
        string Name { get; }

        string? RoutingPattern { get; }

        BrokerExchangeType ExchangeType { get; }
    }
}
