using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLog : TableEntity
    {
        public string CommandName { get; set; }
        public string Payload { get; set; }
        public DateTime ExecutedAtUtc { get; set; }
        public string Domain { get; set; }

        public DataChangeLog(
            DataChangeLogPartitionKey logPartitionKey, 
            DataChangeLogRowKey logRowKey,
            DataChangeLogCommandName commandName,
            DataChangeLogDomain domain,
            DataChangeLogJsonPayload payload, 
            DateTime executedAtUtc)
        {
            PartitionKey = logPartitionKey.ToString();
            RowKey = logRowKey.ToString();
            CommandName = commandName.ToString();
            Domain = domain.ToString();
            Payload = payload.ToString();
            ExecutedAtUtc = executedAtUtc;
        }

        /// <summary>
        /// The public constructor needed to fetch data from table storage.
        /// </summary>
        public DataChangeLog()
        {
        }
    }
}