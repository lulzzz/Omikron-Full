using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public class LocalSqlServerAssetManager : IAssetManager
    {
        private readonly AzureAssetManagerConfiguration _configuration;

        public LocalSqlServerAssetManager(AzureAssetManagerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteDatabaseAsync(string databaseName)
        {
            var (dbContext, connectionString) = FactoryFakeDbContext(databaseName);
            await dbContext.Database.EnsureDeletedAsync();
        }

        public async Task<Uri> GetDatabaseResourceUriAsync(string databaseName)
        {
            return new Uri($"db://local.db/{databaseName}");
        }

        public async Task<string> GetDatabaseNameByTenantIdentityAsync(string databaseName)
        {
            return databaseName;
        }

        public async Task<SqlDatabaseAssetResponse> CreateDatabaseAsync(CreateSqlDatabaseAssetRequest request)
        {
            var databaseName = GetDatabaseName(request.Name);
            var (dbContext, connectionString) = FactoryFakeDbContext(databaseName);
            await dbContext.Database.EnsureCreatedAsync();
            return new SqlDatabaseAssetResponse(request.Name, AssetCreationStatus.Success, AssetType.SqlDatabase, connectionString);
        }

        private (DbContext, string) FactoryFakeDbContext(string databaseName)
        {
            var connectionStringTemplate = _configuration.DefaultDatabaseSettings.ConnectionStringTemplate;
            var connectionString = string.Format(connectionStringTemplate, databaseName);

            var options = new DbContextOptionsBuilder<DbContext>();
            options.UseSqlServer(connectionString);

            var context = new DbContext(options.Options);
            return (context, connectionString);
        }

        private string GetDatabaseName(string databaseName)
        {
            return $"{_configuration.DefaultDatabaseSettings.DatabasePrefix}{databaseName}";
        }
    }
}