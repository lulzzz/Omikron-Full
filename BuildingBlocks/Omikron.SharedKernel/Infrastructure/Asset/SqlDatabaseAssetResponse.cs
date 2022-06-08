namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public class SqlDatabaseAssetResponse : AssetResponse
    {
        public string ConnectionString { get; set; }

        public SqlDatabaseAssetResponse(string assetId, AssetCreationStatus creationStatus, AssetType assetType, string connectionString) : base(assetId, creationStatus, assetType)
        {
            ConnectionString = connectionString;
        }

        public SqlDatabaseAssetResponse(string error) : base(error)
        {
        }
    }
}