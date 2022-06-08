namespace Omikron.SharedKernel.Infrastructure.Email
{
    public abstract class BaseEmailModel
    {
        public string TenantIdentifier { get; set; }
        public string TenantName { get; set; }
    }
}