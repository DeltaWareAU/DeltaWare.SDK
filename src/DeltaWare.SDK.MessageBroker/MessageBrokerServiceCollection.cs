using DeltaWare.SDK.MessageBroker;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class MessageBrokerServiceCollection
    {
        public static IServiceCollection UseMessageBroker(this IServiceCollection services)
        {
            services.TryAddSingleton<IMessageSerializer, DefaultMessageSerializer>();
            services.TryAddSingleton<IMessageBrokerManager, MessageBrokerManager>();
            services.TryAddSingleton<IBindingManager, BindingManager>();

            return services;
        }
    }
}
