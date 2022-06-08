using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Serialization
{
    /// <summary>
    ///     Provides functionality to serialize objects or value types to JSON and
    ///     deserialize JSON into objects or value types.
    /// </summary>
    public interface IJsonSerialization
    {
        /// <summary>
        ///     Convert the provided value to JSON text in async manner.
        /// </summary>
        /// <returns>Converted value to JSON text.</returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="cancellationToken">The which may be used to cancel the operation.</param>
        Task<string> SerializeAsync<TValue>(TValue value, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Convert the provided value to JSON text in async manner.
        /// </summary>
        /// <returns>Converted value to JSON text.</returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="inputType">The type of the <paramref name="value" /> to convert.</param>
        /// <param name="cancellationToken">The which may be used to cancel the operation.</param>
        Task<string> SerializeAsync(object value, Type inputType, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Convert the provided value to JSON text.
        /// </summary>
        /// <returns>Converted value to JSON text.</returns>
        /// <param name="value">The value to convert.</param>
        string Serialize<TValue>(TValue value);

        /// <summary>
        ///     Convert the provided value to JSON text.
        /// </summary>
        /// <returns>Converted value to JSON text.</returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="inputType">The type of the <paramref name="value" /> to convert.</param>
        string Serialize(object value, Type inputType);

        /// <summary>
        ///     Convert the provided value from JSON text to object instance in async manner.
        /// </summary>
        /// <typeparam name="TObject">The type for return.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="cancellationToken">The which may be used to cancel the operation.</param>
        /// <returns>Converted value from JSON text to <typeparamref name="TObject" /> instance. </returns>
        Task<TObject> DeserializeAsync<TObject>(string value, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Convert the provided value from JSON text to object instance in async manner.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="returnType"></param>
        /// <param name="cancellationToken">The which may be used to cancel the operation.</param>
        /// <returns>Converted value from JSON text to <paramref name="returnType" /> instance. </returns>
        Task<object> DeserializeAsync(string value, Type returnType, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Convert the provided value from JSON text to object instance.
        /// </summary>
        /// <typeparam name="TObject">The type for return.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>Converted value from JSON text to <typeparamref name="TObject" /> instance. </returns>
        TObject Deserialize<TObject>(string value);

        /// <summary>
        ///     Convert the provided value from JSON text to object instance.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="returnType"></param>
        /// <returns>Converted value from JSON text to <paramref name="returnType" /> instance. </returns>
        object Deserialize(string value, Type returnType);
    }
}