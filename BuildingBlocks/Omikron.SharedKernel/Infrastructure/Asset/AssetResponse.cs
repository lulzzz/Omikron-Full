namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public class AssetResponse
    {
        public AssetResponse(string assetId, AssetCreationStatus creationStatus, AssetType assetType)
        {
            AssetId = assetId;
            CreationStatus = creationStatus;
            AssetType = assetType;
        }

        public AssetResponse(string error)
        {
            Error = error;
            CreationStatus = AssetCreationStatus.Fail;
            AssetType = AssetType.SqlDatabase;
        }

        public string Error { get; }
        public string AssetId { get; }
        public AssetCreationStatus CreationStatus { get; }
        public AssetType AssetType { get; }
    }
}