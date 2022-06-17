namespace DeltaWare.SDK.MessageBroker.RabbitMQ.Options
{
    public interface IRabbitMqMessageBrokerOptions
    {
        string UserName { get; }

        string Password { get; }

        string HostName { get; }

        string VirtualHost { get; }

        int Port { get; }
    }

    public class RabbitMqMessageBrokerOptions : IRabbitMqMessageBrokerOptions
    {
        public string UserName { get; set; }
        public string Password { get; }
        public string HostName { get; }
        public string VirtualHost { get; }
        public int Port { get; set; }
    }
}
