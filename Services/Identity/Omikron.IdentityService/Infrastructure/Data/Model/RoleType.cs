namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    public enum RoleType
    {
        /// <summary>
        /// Role type "System" is roles which are defined by default and cannot be deleted or edit.
        /// </summary>
        System = 1,

        /// <summary>
        ///  Role type "Client" is roles which are defined by Tenant Administrator or Role Manager such roles can be deleted or edited.
        /// </summary>
        Client = 2
    }
}