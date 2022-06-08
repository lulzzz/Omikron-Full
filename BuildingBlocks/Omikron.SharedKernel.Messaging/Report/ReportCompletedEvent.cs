using System;

namespace Omikron.SharedKernel.Messaging.Report
{
    /// <summary>
    ///     A event which is raised once when report has been completed
    /// </summary>
    public class ReportCompletedEvent : ReportEvent
    {
        public ReportCompletedEvent()
        {
        }

        public ReportCompletedEvent(string tenantId, Guid id, string type) : base(tenantId, id, type)
        {
        }

        public ReportCompletedEvent(string tenantId) : base(tenantId)
        {
        }
    }
}