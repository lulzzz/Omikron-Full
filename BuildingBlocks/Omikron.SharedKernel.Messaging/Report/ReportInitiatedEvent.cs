using System;

namespace Omikron.SharedKernel.Messaging.Report
{
    /// <summary>
    ///     A event which is raised once when report has been initiated by user
    /// </summary>
    public class ReportInitiatedEvent : ReportEvent
    {
        public ReportInitiatedEvent() { }

        public ReportInitiatedEvent(string tenantId) : base(tenantId)
        {
        }

        public ReportInitiatedEvent(string tenantId, Guid id, string type) : base(tenantId, id, type)
        {
        }
    }
}