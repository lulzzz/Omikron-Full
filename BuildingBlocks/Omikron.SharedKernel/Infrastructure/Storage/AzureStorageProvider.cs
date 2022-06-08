using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Infrastructure.Storage
{
    public class AzureStorageProvider : IStorageProvider<Blob, Uri>
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly Dictionary<string, string> _connectionStringSegments;
        private readonly string _containerName;

        public AzureStorageProvider(string connectionString, string containerName)
        {
            _containerName = containerName;
            _connectionStringSegments = connectionString.Split(separator: ';').Where(predicate: x => x.Length > 0).Select(selector: x => x.Split(separator: new[] { '=' }, count: 2)).ToDictionary(keySelector: x => x[0], elementSelector: x => x[1]);
            var blobServiceClient = new BlobServiceClient(connectionString: connectionString);

            _blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName: containerName);
            _blobContainerClient.CreateIfNotExists();
        }

        public async Task<Uri> SaveAsync(Blob blob)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName: blob.Name);

            await blobClient.UploadAsync(content: blob.Stream, overwrite: true);

            return blobClient.Uri;
        }

        public async Task<bool> Delete(string blobName)
        {
            return await _blobContainerClient.DeleteBlobIfExistsAsync(blobName: blobName);
        }

        public async Task<string> GetAccessToken()
        {
            var sasToken = string.Empty;
            try
            {
                var sharedKeyCredential = new StorageSharedKeyCredential(accountName: _connectionStringSegments[key: "AccountName"], accountKey: _connectionStringSegments[key: "AccountKey"]);
                var now = Clock.GetTime();
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = _containerName,
                    Resource = "c",
                    StartsOn = now,
                    ExpiresOn = now.AddHours(value: 1)
                };

                sasBuilder.SetPermissions(permissions: BlobContainerSasPermissions.Read);
                sasToken = $"?{sasBuilder.ToSasQueryParameters(sharedKeyCredential: sharedKeyCredential)}";
            }
            catch (Exception)
            {
                //ignore
            }

            return await Task.FromResult(result: sasToken);
        }
    }
}