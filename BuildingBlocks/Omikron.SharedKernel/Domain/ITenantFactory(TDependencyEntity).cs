using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Domain
{
    public interface ITenantFactory<out TDependencyEntity> : IFactory<TDependencyEntity, Tenant>
    {
    }
}