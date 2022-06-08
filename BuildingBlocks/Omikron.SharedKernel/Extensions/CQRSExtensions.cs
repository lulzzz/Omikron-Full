using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.SharedKernel.Extensions
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection serviceCollection, Assembly[] assemblies, IConfiguration configuration)
        {
            return serviceCollection
                .AddMediatR(assemblies: assemblies)
                .AddBus(configuration: configuration, assemblies: assemblies)
                .AddScoped<IDispatcher, CommandDispatcher>();
        }

        public static IServiceCollection AddDataTraceCommandHandlers(this IServiceCollection serviceCollection, Assembly[] assemblies, IConfiguration configuration)
        {
            return serviceCollection
                .AddMediatR(assemblies: assemblies)
                .AddBus(configuration: configuration, assemblies: assemblies)
                .AddScoped<IDispatcher, DataTraceCommandDispatcher>()
                .AddTransient(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(LoggingBehavior<,>))
                .AddAzureTableStorageDataTraceManager(configuration: configuration);
        }
    }
}