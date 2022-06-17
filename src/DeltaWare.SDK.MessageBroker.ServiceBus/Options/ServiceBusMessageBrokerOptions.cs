namespace DeltaWare.SDK.MessageBroker.ServiceBus.Options
{
    public interface IServiceBusMessageBrokerOptions
    {
        public string ConnectionString { get; }
    }

    public class ServiceBusMessageBrokerOptions : IServiceBusMessageBrokerOptions
    {
        public string ConnectionString { get; set; }
    }
}
