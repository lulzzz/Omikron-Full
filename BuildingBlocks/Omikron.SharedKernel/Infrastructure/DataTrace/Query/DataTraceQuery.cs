using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Omikron.SharedKernel.Infrastructure.DataTrace.Query
{
    public class DataTraceQuery
    {
        public string TenantIdentifier { get; set; }
        public string Username { get; set; }
        public TableContinuationToken ContinuationToken { get; set; }

        private int _pageSize;
        public int PageSize
        {
            get => _pageSize == default ? 10 : _pageSize;
            set => _pageSize = value;
        }

        private DateTime _to;
        public DateTime To
        {
            get => _to == default ? DateTime.UtcNow : _to;
            set => _to = value;
        }

        private DateTime _from;
        public DateTime From
        {
            get => _from == default ? DateTime.UtcNow.AddDays(-1) : _from;
            set => _from = value;
        }
    }
}