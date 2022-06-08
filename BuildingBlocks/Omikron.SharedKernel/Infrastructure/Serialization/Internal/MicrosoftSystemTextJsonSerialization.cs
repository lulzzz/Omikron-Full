using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Serialization.Internal
{
    internal sealed class MicrosoftSystemTextJsonSerialization : IJsonSerialization
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public MicrosoftSystemTextJsonSerialization(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions;
        }

        public async Task<string> SerializeAsync<TValue>(TValue value, CancellationToken cancellationToken = default)
        {
            await using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(utf8Json: stream, value: value, options: _serializerOptions, cancellationToken: cancellationToken);
            stream.Position = 0;
            using var reader = new StreamReader(stream: stream);
            return await reader.ReadToEndAsync();
        }

        public async Task<string> SerializeAsync(object value, Type inputType, CancellationToken cancellationToken = default)
        {
            await using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(utf8Json: stream, value: value, inputType: inputType, options: _serializerOptions, cancellationToken: cancellationToken);
            stream.Position = 0;
            using var reader = new StreamReader(stream: stream);
            return await reader.ReadToEndAsync();
        }

        public string Serialize<TValue>(TValue value)
        {
            return JsonSerializer.Serialize(value: value, options: _serializerOptions);
        }

        public string Serialize(object value, Type inputType)
        {
            return JsonSerializer.Serialize(value: value, inputType: inputType, options: _serializerOptions);
        }

        public async Task<TObject> DeserializeAsync<TObject>(string value, CancellationToken cancellationToken = default)
        {
            await using var stream = new MemoryStream(buffer: Encoding.UTF8.GetBytes(s: value));
            return await JsonSerializer.DeserializeAsync<TObject>(utf8Json: stream, options: _serializerOptions, cancellationToken: cancellationToken);
        }

        public async Task<object> DeserializeAsync(string value, Type returnType, CancellationToken cancellationToken = default)
        {
            await using var stream = new MemoryStream(buffer: Encoding.UTF8.GetBytes(s: value));
            return await JsonSerializer.DeserializeAsync(utf8Json: stream, returnType: returnType, options: _serializerOptions, cancellationToken: cancellationToken);
        }

        public TObject Deserialize<TObject>(string value)
        {
            return JsonSerializer.Deserialize<TObject>(json: value, options: _serializerOptions);
        }

        public object Deserialize(string value, Type returnType)
        {
            return JsonSerializer.Deserialize(json: value, returnType: returnType, options: _serializerOptions);
        }
    }
}