using System;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Infrastructure.Serialization.IoC
{
    public interface IDependencyInjectionJsonSerializationSelector
    {
        /// <summary>
        ///     Provides high-performance, low-allocating, and standards-compliant capabilities to process JavaScript Object
        ///     Notation (JSON), which includes serializing objects to JSON text and deserializing JSON text to objects, with UTF-8
        ///     support built-in using Microsoft System.Text.Json implementation with default params.
        /// </summary>
        /// <returns>The service collation chain.</returns>
        IServiceCollection UseMicrosoftSystemTextJsonProvider();

        /// <summary>
        ///     Provides high-performance, low-allocating, and standards-compliant capabilities to process JavaScript Object
        ///     Notation (JSON), which includes serializing objects to JSON text and deserializing JSON text to objects, with UTF-8
        ///     support built-in using Microsoft System.Text.Json implementation with default params.
        /// </summary>
        /// <param name="options">Override the default JSON serialization options</param>
        /// <returns>The service collation chain.</returns>
        IServiceCollection UseMicrosoftSystemTextJsonProvider(JsonSerializerOptions options);

        /// <summary>
        ///     Provides high-performance, low-allocating, and standards-compliant capabilities to process JavaScript Object
        ///     Notation (JSON), which includes serializing objects to JSON text and deserializing JSON text to objects, with UTF-8
        ///     support built-in using Microsoft System.Text.Json implementation with default params.
        /// </summary>
        /// <param name="options">Override the default JSON serialization options</param>
        /// <returns>The service collation chain.</returns>
        IServiceCollection UseMicrosoftSystemTextJsonProvider(Action<JsonSerializerOptions> options);
    }
}