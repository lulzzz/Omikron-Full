using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.Sync
{
    public sealed class SyncResult : ValueObject<SyncResult>
    {
        public SyncResult(SyncStatus status, SyncException exception)
        {
            Status = status;
            Exception = exception;
        }

        public SyncStatus Status { get; set; }
        public SyncException Exception { get; set; }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { Status, Exception };
    }
}