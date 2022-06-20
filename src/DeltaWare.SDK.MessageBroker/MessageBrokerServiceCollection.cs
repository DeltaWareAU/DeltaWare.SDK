using DeltaWare.SDK.MessageBroker;
using DeltaWare.SDK.MessageBroker.Binding;
using DeltaWare.SDK.MessageBroker.Hosting;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;
using DeltaWare.SDK.MessageBroker.Processors;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class MessageBrokerServiceCollection
    {
        public static IServiceCollection UseMessageBroker(this IServiceCollection services)
        {
            services.TryAddSingleton<IMessageSerializer, DefaultMessageSerializer>();
            services.TryAddSingleton<IMessageProcessorManager, MessageProcessorManager>();
            services.TryAddSingleton<IMessagePublisher>(p => p.GetRequiredService<IMessageBroker>());
            services.TryAddSingleton<IBindingManager, BindingManager>();
            services.AddHostedService<MessageBrokerHost>();

            return services;
        }
    }
}
