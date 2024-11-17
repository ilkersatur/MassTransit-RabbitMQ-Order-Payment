using System.Diagnostics.CodeAnalysis;
using MassTransit;

namespace CodeApp.Masstransit.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class MassTransitExtensions
{
    public static void RegisterQueue<T>(this IRabbitMqBusFactoryConfigurator config, IBusRegistrationContext context,
        string queueNameBase, Type eventClassType, int? concurrency = null) where T : class, IConsumer
    {
        config.ReceiveEndpoint($"{queueNameBase}.{eventClassType.Name.ToLower()}", endpoint =>
        {
            endpoint.ConcurrentMessageLimit = concurrency ?? endpoint.ConcurrentMessageLimit;

            endpoint.UseMessageRetry(conf =>
                conf.Incremental(
                    2,
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(5)));
            endpoint.ConfigureConsumer<T>(context);
            endpoint.SetQuorumQueue();
            
        });
    }
}