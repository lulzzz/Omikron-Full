using System.Reflection;
using Omikron.SharedKernel.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class EventsExtensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection serviceCollection, Assembly[] assemblies)
        {
            DomainEvents.Init(assemblies);
            return serviceCollection;
        }
    }
}