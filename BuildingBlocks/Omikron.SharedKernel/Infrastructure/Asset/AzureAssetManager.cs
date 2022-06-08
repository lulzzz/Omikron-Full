using System;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using FluentAzure = Microsoft.Azure.Management.Fluent.Azure;

namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public class AzureAssetManager : IAssetManager
    {
        private readonly AzureAssetManagerConfiguration _configuration;
        private readonly LoggerContext _loggerContext;
        private readonly IAzure _azure;

        public AzureAssetManager(AzureAssetManagerConfiguration configuration, LoggerContext loggerContext)
        {
            _configuration = configuration;
            _loggerContext = loggerContext;

            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(configuration.AppRegistrationId,
                    configuration.AppRegistrationSecret, configuration.TenantId, AzureEnvironment.AzureGlobalCloud)
                .WithDefaultSubscription(configuration.SubscriptionId);

            _azure = FluentAzure
                .Configure()
                .Authenticate(credentials)
                .WithDefaultSubscription();
        }

        public async Task<SqlDatabaseAssetResponse> CreateDatabaseAsync(CreateSqlDatabaseAssetRequest request)
        {
            try
            {
                var databaseName = await GetDatabaseNameWithPrefix(request.Name);
                var exists = await DatabaseExists(databaseName);
                if (exists)
                {
                    throw new Exception($"The database {databaseName} already exists.");
                }

                const string successStatus = "online";
                var sqlServer = _azure.SqlServers.GetByResourceGroup(request.ResourceGroupName ?? _configuration.ResourceGroupName,
                    request.SqlServerName ?? _configuration.DefaultDatabaseSettings.SqlServer);
                
                ISqlDatabase database;
                
                if (_configuration.DefaultDatabaseSettings.ElasticPool.IsNotNullOrWhiteSpace())
                {
                    database = await sqlServer
                        .Databases.Define(databaseName)
                        .WithExistingElasticPool(_configuration.DefaultDatabaseSettings.ElasticPool)
                        .CreateAsync();
                }
                else
                {
                    database = await sqlServer
                        .Databases.Define(databaseName)
                        .WithEdition(DatabaseEdition.Parse(request.DatabaseEditions ?? _configuration.DefaultDatabaseSettings.DatabaseEditions))
                        .WithServiceObjective(ServiceObjectiveName.Parse(request.ServiceObjectiveName ?? _configuration.DefaultDatabaseSettings.ServiceObjectiveName))
                        .CreateAsync();
                }

                var connectionString = GenerateConnectionString(database);
                return new SqlDatabaseAssetResponse(database.Id,
                    database.Status.Equals(successStatus, StringComparison.CurrentCultureIgnoreCase)
                        ? AssetCreationStatus.Success
                        : AssetCreationStatus.Fail, AssetType.SqlDatabase, connectionString);
            }
            catch (Exception e)
            {
                _loggerContext.ErrorLogger.Error(e);
                return new SqlDatabaseAssetResponse(e.Message);
            }
        }

        public async Task DeleteDatabaseAsync(string databaseName)
        {
            var exists = await DatabaseExists(databaseName);
            if (exists)
            {
                var sqlServer = _azure.SqlServers.GetByResourceGroup(_configuration.ResourceGroupName,
                    _configuration.DefaultDatabaseSettings.SqlServer);
                await sqlServer.Databases.DeleteAsync(databaseName);
            }
        }

        public async Task<Uri> GetDatabaseResourceUriAsync(string databaseName)
        {
            return new Uri($"{_configuration.PortalAzureTenantUri}/resource{databaseName}/overview");
        }

        public async Task<string> GetDatabaseNameWithPrefix(string identifier)
        {
            return identifier.StartsWith(_configuration.DefaultDatabaseSettings.DatabasePrefix)
                ? identifier
                : $"{_configuration.DefaultDatabaseSettings.DatabasePrefix}{identifier}";
        }

        private string GenerateConnectionString(IHasName database)
        {
            return string.Format(_configuration.DefaultDatabaseSettings.ConnectionStringTemplate, database.Name);
        }

        private async Task<bool> DatabaseExists(string databaseName)
        {
            var db = await GetDatabaseByName(databaseName);
            return db != null;
        }

        private async Task<ISqlDatabase> GetDatabaseByName(string databaseName)
        {
            var sqlServer = _azure.SqlServers.GetByResourceGroup(_configuration.ResourceGroupName,
                _configuration.DefaultDatabaseSettings.SqlServer);
            return await sqlServer.Databases.GetAsync(databaseName);
        }
    }
}