using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Infrastructure.DataTrace.Query;

namespace Omikron.SharedKernel.Infrastructure.DataTrace.ViewModel
{
    public class DataChangeLogViewModel
    {
        public DataTraceQuery Query { get; }
        public IReadOnlyList<DataChangeLogDto> Logs { get; }

        public DataChangeLogViewModel(DataTraceQuery query, IReadOnlyList<DataChangeLogDto> logs)
        {
            Query = query;
            Logs = logs;
        }
    }

    public class DataChangeLogDto
    {
        public string Tenant { get; set; }
        public string Username { get; set; }
        public string CommandName { get; set; }
        public string Payload { get; set; }
        public DateTime ExecutedAtUtc { get; set; }
        public string Domain { get; set; }

        public DataChangeLogDto(DataChangeLog dataChangeLog, string tenant)
        {
            CommandName = dataChangeLog.CommandName;
            Payload = dataChangeLog.Payload;
            ExecutedAtUtc = dataChangeLog.ExecutedAtUtc;
            Domain = dataChangeLog.Domain;
            Username = dataChangeLog.PartitionKey;
            Tenant = tenant;
        }
    }
}