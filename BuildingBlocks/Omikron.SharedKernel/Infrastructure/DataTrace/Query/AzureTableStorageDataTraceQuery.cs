using Microsoft.WindowsAzure.Storage.Table;

namespace Omikron.SharedKernel.Infrastructure.DataTrace.Query
{
    public class AzureTableStorageDataTraceQuery : DataTraceQuery
    {
        public TableContinuationToken Token { get; set; }
    }
}