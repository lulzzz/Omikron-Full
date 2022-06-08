namespace Omikron.SharedKernel.Domain
{
    public interface ITenantHolder
    {
        public void SetTenant(string tenant);
        public string GetTenant();
    }
}