using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.DataTrace.Query;
using Omikron.SharedKernel.Infrastructure.DataTrace.ViewModel;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class AzureTableStorageDataTraceManager : IDataTraceManager<AzureTableStorageDataTraceQuery>
    {
        private readonly ITenantAccessor _tenantAccessor;
        private OmikronTenantInfo _tenant;

        public AzureTableStorageDataTraceManager(IConfiguration configuration, ITenantAccessor tenantAccessor)
        {
            _tenantAccessor = tenantAccessor;
            var connectionString = configuration.GetValue<string>(key: "AzureBlobStorage:ConnectionString");
            if (string.IsNullOrWhiteSpace(value: connectionString))
            {
                throw new ArgumentNullException(paramName: "The connection string for azure table storage data trace manager cannot be null or empty.");
            }

            var storageAccount = CloudStorageAccount.Parse(connectionString: connectionString);
            TableClient = storageAccount.CreateCloudTableClient();
            SetupTableStorage(tenantAccessor: tenantAccessor);
        }

        private CloudTable Table { get; set; }
        private CloudTableClient TableClient { get; }

        public virtual Task SaveAsync(IList<DataChangeLog> dataChangeLogs)
        {
            if (dataChangeLogs == null || !dataChangeLogs.Any() || dataChangeLogs.Count > 100)
            {
                throw new ArgumentException(message: "The params data change logs cannot be bull and max size of array needs to be 100 items.");
            }

            var batchInsert = new TableBatchOperation();
            foreach (var dataChangeLog in dataChangeLogs)
            {
                batchInsert.Insert(entity: dataChangeLog);
            }

            return Table.ExecuteBatchAsync(batch: batchInsert);
        }

        public virtual async Task<DataChangeLogViewModel> Get(AzureTableStorageDataTraceQuery query)
        {
            var tenantName = Tenant.SystemTenant.Identifier;
            var tableQuery = new TableQuery<DataChangeLog>().Take(take: query.PageSize);
            var fromQuery = TableQuery.GenerateFilterCondition(propertyName: "RowKey", operation: QueryComparisons.GreaterThanOrEqual, givenValue: query.From.Ticks.ToString(format: "D16"));
            var toQuery = TableQuery.GenerateFilterCondition(propertyName: "RowKey", operation: QueryComparisons.LessThanOrEqual, givenValue: query.To.Ticks.ToString(format: "D16"));
            var computedQuery = TableQuery.CombineFilters(filterA: fromQuery, operatorString: TableOperators.And, filterB: toQuery);

            if (!string.IsNullOrWhiteSpace(value: query.Username))
            {
                computedQuery = TableQuery.CombineFilters(filterA: computedQuery, operatorString: TableOperators.And, filterB: TableQuery.GenerateFilterCondition(propertyName: "PartitionKey", operation: QueryComparisons.Equal, givenValue: query.Username));
            }

            tableQuery = tableQuery.Where(filter: computedQuery);
            var result = await Table.ExecuteQuerySegmentedAsync(query: tableQuery, token: query.ContinuationToken);
            query.ContinuationToken = result.ContinuationToken;

            var dataChangeLogs = result.Results.Select(selector: log => new DataChangeLogDto(dataChangeLog: log, tenant: tenantName)).ToList();
            return new DataChangeLogViewModel(query: query, logs: dataChangeLogs);
        }

        private void GetOrCreateTableReference(DataChangeLogTableName tableName)
        {
            Table = TableClient.GetTableReference(tableName: tableName.ToString());
            Table.CreateIfNotExistsAsync().Wait();
        }

        private void SetupTableStorage(ITenantAccessor tenantAccessor)
        {
            var tableName = DataChangeLogTableName.Parse(tenant: Tenant.SystemTenant);
            GetOrCreateTableReference(tableName: tableName);
        }
    }
}