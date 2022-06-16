using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IBindingDetails
    {
        string Name { get; }

        string? RoutingPattern { get; }

        BrokerExchangeType ExchangeType { get; }
    }

    public class BindingDetails : IBindingDetails
    {
        public string Name { get; init; }
        public string? RoutingPattern { get; init; }
        public BrokerExchangeType ExchangeType { get; init; }

    }
}
