using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Domain
{
    public static class DomainEvents
    {
        private static List<Type> _handlers;

        public static void Init(Assembly[] assemblies)
        {
            _handlers = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
                .ToList();
        }

        public static void Dispatch(IDomainEvent domainEvent, IServiceProvider serviceProvider)
        {
            foreach (var handlerType in _handlers)
            {
                var canHandleEvent = handlerType.GetInterfaces()
                    .Any(x => x.IsGenericType
                              && x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)
                              && x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    dynamic handler = ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
                    handler.HandleAsync((dynamic) domainEvent).Wait();
                }
            }
        }

        public static void Raise(IHasDomainEvents events, IServiceProvider serviceProvider)
        {
            foreach (var @event in events.DomainEvents) Dispatch(@event, serviceProvider);
        }
    }
}