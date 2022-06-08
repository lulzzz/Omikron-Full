namespace Omikron.SharedKernel.Security
{
    public class Claims
    {
        public const string TenantId = "omikron.claim.tenant.identifier";
        public const string TenantName = "omikron.claim.tenant.name";
        public const string BlobAccessToken = "aaf.claim.blob.access.token";
        public const string ProfilePhoto = "omikron.claim.profile.photo";
        public const string UserId = "omikron.claim.profile.user.id";
        public const string AccountStatus = "omikron.claim.profile.user.account-status";
        public const string Permissions = "omikron.claim.profile.user.permissions";
        public const string ClientId = "client_id";
    }
}