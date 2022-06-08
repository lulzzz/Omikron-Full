namespace Omikron.SharedKernel.Infrastructure.Logging
{
    public interface ITrackingLogger
    {
        public string CorrelationId { get; set; }

        public string ParentId { get; set; }
    }
}