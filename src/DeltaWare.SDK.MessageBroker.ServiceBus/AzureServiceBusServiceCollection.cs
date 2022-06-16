using Azure.Messaging.ServiceBus;
using DeltaWare.SDK.MessageBroker;
using DeltaWare.SDK.MessageBroker.ServiceBus;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureServiceBusServiceCollection
    {
        public static IServiceCollection UseServiceBus(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddSingleton(new ServiceBusClient(connectionString));

            serviceCollection.UseMessageBroker();

            serviceCollection.AddHostedService<ServiceBusMessageBrokerHost>();

            serviceCollection.AddTransient<IMessageBroker, ServiceBusMessageBroker>();

            return serviceCollection;
        }
    }
}
