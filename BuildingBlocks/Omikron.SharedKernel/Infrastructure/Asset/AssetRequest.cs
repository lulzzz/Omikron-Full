namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public abstract class AssetRequest
    {
        protected AssetRequest(AssetType assetType, string name, string resourceGroupName)
        {
            AssetType = assetType;
            Name = name;
            ResourceGroupName = resourceGroupName;
        }

        public AssetType AssetType { get; }
        public string Name { get; }
        public string ResourceGroupName { get; }
    }
}