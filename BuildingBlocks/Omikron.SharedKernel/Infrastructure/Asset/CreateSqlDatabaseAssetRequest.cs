namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public sealed class CreateSqlDatabaseAssetRequest : AssetRequest
    {
        public CreateSqlDatabaseAssetRequest(string name, string resourceGroupName,
            string sqlServerName, string databaseEditions, string serviceObjectiveName) : base(AssetType.SqlDatabase, name,
            resourceGroupName)
        {
            SqlServerName = sqlServerName;
            DatabaseEditions = databaseEditions;
            ServiceObjectiveName = serviceObjectiveName;
        }

        public CreateSqlDatabaseAssetRequest(string name) : this(name, null, null, null, null)
        {
        }

        public string SqlServerName { get; }
        public string DatabaseEditions { get; }
        public string ServiceObjectiveName { get; }
    }
}