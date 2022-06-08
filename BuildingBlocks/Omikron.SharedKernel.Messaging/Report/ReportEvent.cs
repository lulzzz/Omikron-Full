using System;

namespace Omikron.SharedKernel.Messaging.Report
{
    /// <summary>
    ///     A base report event.
    /// </summary>
    public abstract class ReportEvent : ITopicMessage
    {
        protected ReportEvent() { }

        protected ReportEvent(string tenantId)
        {
            TenantId = tenantId;
        }

        protected ReportEvent(string tenantId, Guid id, string type) : this(tenantId)
        {
            Id = id;
            Type = type;
        }

        public Guid Id { get; set; }
        public string Type { get; set; }
        public string TenantId { get; set; }
    }
}