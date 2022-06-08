using System;
using System.Text.Json;
using Omikron.SharedKernel.Infrastructure.Serialization.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Infrastructure.Serialization.IoC.Microsoft
{
    internal class MicrosoftDependencyInjectionJsonSerializationSelector : IDependencyInjectionJsonSerializationSelector
    {
        public MicrosoftDependencyInjectionJsonSerializationSelector(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        private IServiceCollection ServiceCollection { get; }

        public IServiceCollection UseMicrosoftSystemTextJsonProvider()
        {
            return UseMicrosoftSystemTextJsonProvider(options: new JsonSerializerOptions(defaults: JsonSerializerDefaults.General));
        }

        public IServiceCollection UseMicrosoftSystemTextJsonProvider(Action<JsonSerializerOptions> options)
        {
            var instance = new JsonSerializerOptions(defaults: JsonSerializerDefaults.General);
            options(obj: instance);

            return UseMicrosoftSystemTextJsonProvider(options: instance);
        }

        public IServiceCollection UseMicrosoftSystemTextJsonProvider(JsonSerializerOptions options)
        {
            ServiceCollection.AddSingleton(implementationInstance: options ?? new JsonSerializerOptions(defaults: JsonSerializerDefaults.General));
            ServiceCollection.AddSingleton<IJsonSerialization, MicrosoftSystemTextJsonSerialization>();
            return ServiceCollection;
        }
    }
}