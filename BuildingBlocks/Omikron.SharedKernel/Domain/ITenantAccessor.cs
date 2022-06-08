namespace Omikron.SharedKernel.Domain
{
    public interface ITenantAccessor
    {
        OmikronTenantInfo GetTenant();
        OmikronTenantInfo GetTenant(string identifier);
        OmikronTenantInfo GetTenantOrDefault(string defaultIdentifier = null);
    }
}