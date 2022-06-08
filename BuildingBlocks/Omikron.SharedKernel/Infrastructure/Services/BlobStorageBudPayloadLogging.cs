using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Omikron.SharedKernel.Infrastructure.Configuration;
using Omikron.SharedKernel.Infrastructure.Serialization;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Infrastructure.Services
{
	public class BlobStorageBudPayloadLogging : IBudPayloadLogging
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly IJsonSerialization _jsonSerialization;

        public BlobStorageBudPayloadLogging(IJsonSerialization jsonSerialization, IOptionsMonitor<BudConfiguration> budConfigurationOptions)
        {
            _jsonSerialization = jsonSerialization;
            var blobServiceClient = new BlobServiceClient(connectionString: budConfigurationOptions.CurrentValue.LoggingStorageConnectionString);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName: budConfigurationOptions.CurrentValue.Container.ToLower());
            _blobContainerClient.CreateIfNotExists();
        }

        public async Task LogAsync(string customerId, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            var sb = new StringBuilder();
            var requestSerialized = await _jsonSerialization.SerializeAsync(value: requestMessage, cancellationToken: cancellationToken);
            var responseSerialized = await _jsonSerialization.SerializeAsync(value: responseMessage, cancellationToken: cancellationToken);
            sb.AppendLine(value: $"CustomerId: {customerId}");
            sb.AppendLine();

            sb.AppendLine(value: "----------- Http Request ----------");
            sb.AppendLine(value: requestSerialized);
            sb.AppendLine(value: "----------- End HTTP Request ----------");
            sb.AppendLine();

            sb.AppendLine(value: "----------- Http Response ----------");
            sb.AppendLine(value: responseSerialized);
            sb.AppendLine(value: "----------- End HTTP Response ----------");
            sb.AppendLine();

            sb.AppendLine(value: "----------- Http Payload ----------");
            var json = await responseMessage.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
            sb.AppendLine(value: json);
            sb.AppendLine(value: "----------- End Payload ----------");

            var stream = sb.ToString().ToMemoryStream();
            await _blobContainerClient.UploadBlobAsync(blobName: $"{customerId}-{Clock.GetTime():O}.txt", content: stream, cancellationToken: cancellationToken);
        }
    }
}