using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omikron.SharedKernel.Infrastructure.Bus;

namespace Omikron.SharedKernel.Extensions
{
    public static class BusExtensions
    {
        public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
        {
            var consumers = GetConsumers(assemblies: assemblies);

            var connectionString = configuration.GetValue<string>(key: "Azure:ServiceBus:ConnectionString");
            var queue = configuration.GetValue<string>(key: "Azure:ServiceBus:Queue");

            var maxConcurrentCalls = configuration.GetValue<int>(key: "Azure:ServiceBus:MaxConcurrentCalls");
            var prefetchCount = configuration.GetValue<int>(key: "Azure:ServiceBus:PrefetchCount");

            services.AddMassTransit(configure: configurator =>
                ConfigureMassTransit(
                    configurator: configurator,
                    consumers: consumers,
                    connectionString: connectionString,
                    queue: queue,
                    maxConcurrentCalls: maxConcurrentCalls,
                    prefetchCount: prefetchCount));

            services.AddSingleton<IHostedService, BusHostedService>();

            return services;
        }

        private static List<Type> GetConsumers(IEnumerable<Assembly> assemblies)
        {
            const string marker = "IConsumer";
            var consumers = assemblies
                .SelectMany(selector: assembly => assembly.GetTypes().Where(predicate: type => !type.IsAbstract && !type.Name.StartsWith(value: "Base") && type.GetInterfaces().Any(predicate: i => i.Name.Contains(value: marker))))
                .Select(selector: t => t).ToList();
            return consumers;
        }

        private static void ConfigureMassTransit(IServiceCollectionBusConfigurator configurator, List<Type> consumers, string connectionString, string queue, int maxConcurrentCalls, int prefetchCount)
        {
            configurator.AddConsumers(types: consumers.ToArray());

            configurator.AddBus(busFactory: provider =>
                Bus.Factory.CreateUsingAzureServiceBus(configure: cfg =>
                    ConfigureBus(cfg: cfg,
                        connectionString: connectionString,
                        queue: queue,
                        maxConcurrentCalls: maxConcurrentCalls,
                        prefetchCount: prefetchCount,
                        provider: provider)));

            configurator.AddServiceBusMessageScheduler();
        }

        private static void ConfigureBus(IServiceBusBusFactoryConfigurator cfg, string connectionString, string queue, int maxConcurrentCalls, int prefetchCount, IBusRegistrationContext provider)
        {
            cfg.Host(connectionString: connectionString);
            cfg.ReceiveEndpoint(queueName: queue, configureEndpoint: x => ConfigureReceiveEndpoint(maxConcurrentCalls: maxConcurrentCalls, prefetchCount: prefetchCount, provider: provider, x: x));
            cfg.UseServiceBusMessageScheduler();
        }

        private static void ConfigureReceiveEndpoint(int maxConcurrentCalls, int prefetchCount, IBusRegistrationContext provider, IServiceBusReceiveEndpointConfigurator x)
        {
            x.UseConcurrencyLimit(concurrentMessageLimit: maxConcurrentCalls);
            x.PrefetchCount = prefetchCount;
            x.MaxConcurrentCalls = maxConcurrentCalls;

            x.EnablePartitioning = true;
            x.UseMessageRetry(configure: m => m.Intervals(100, 300, 1000));
            x.ConfigureConsumers(registration: provider);
        }
    }
}