using System;

namespace Omikron.SharedKernel.Messaging.Report
{
    /// <summary>
    ///     A event which is raised once when report fail
    /// </summary>
    public class ReportFailEvent : ReportEvent
    {
        public string CorrelationId { get; set; }

        public ReportFailEvent(string tenantId, string correlationId, Guid id, string type) : base(tenantId, id, type)
        {
            CorrelationId = correlationId;
        }

        public ReportFailEvent(string tenantId) : base(tenantId)
        {
        }
    }
}