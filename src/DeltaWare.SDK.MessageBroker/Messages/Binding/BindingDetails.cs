using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Messages.Binding
{
    public class BindingDetails : IBindingDetails
    {
        public string Name { get; init; }
        public string? RoutingPattern { get; init; }
        public BrokerExchangeType ExchangeType { get; init; }

    }
}
