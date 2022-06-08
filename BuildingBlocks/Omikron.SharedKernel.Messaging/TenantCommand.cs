using Omikron.SharedKernel.Attributes;
using MediatR;

namespace Omikron.SharedKernel.Messaging
{
    public abstract class TenantCommand<TResponse> : IRequest<TResponse>
    {
        [LoggerField(Name = "TenantId")]
        public string TenantIdentifier { get; set; }

        protected TenantCommand()
        {
        }

        protected TenantCommand(string tenantIdentifier)
        {
            TenantIdentifier = tenantIdentifier;
        }
    }

    public abstract class TenantCommand : IRequest
    {
        public string TenantIdentifier { get; set; }

        protected TenantCommand()
        {
        }

        protected TenantCommand(string tenantIdentifier)
        {
            TenantIdentifier = tenantIdentifier;
        }
    }
}