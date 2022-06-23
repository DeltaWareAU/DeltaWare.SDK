using DeltaWare.SDK.MessageBroker.Broker;
using DeltaWare.SDK.MessageBroker.RabbitMQ.Broker;
using DeltaWare.SDK.MessageBroker.RabbitMQ.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureServiceBusServiceCollection
    {
        public static IServiceCollection UseRabbitMQ(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IRabbitMqMessageBrokerOptions>(new RabbitMqMessageBrokerOptions
                {
                })
                .UseMessageBroker()
                .AddSingleton<IMessageBroker, RabbitMqMessageBroker>();

            return serviceCollection;
        }
    }
}
