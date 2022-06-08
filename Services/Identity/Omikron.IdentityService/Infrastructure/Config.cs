using System;
using System.Collections.Generic;
using System.Security.Claims;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Security;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Omikron.IdentityService.Infrastructure
{
    public static class Config
    {
        private const string IdentityServiceApi = "omikron-identity-service-api";
        private const string VaultServiceApi = "omikron-vault-service-api";
        private const string SyncServiceApi = "omikron-sync-service-api";

        private static ICollection<string> AllowedScopes => new List<string>
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            IdentityServerConstants.StandardScopes.OfflineAccess,
            IdentityServiceApi,
            VaultServiceApi,
            SyncServiceApi
        };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.PreferredUserName,
                        ClaimTypes.Name
                    }
                },
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiScope> GetApiScope()
        {
            return new List<ApiScope>
            {
                new ApiScope(IdentityServiceApi),
                new ApiScope(VaultServiceApi),
                new ApiScope(SyncServiceApi),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource(IdentityServiceApi, "The Identity Service API")
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.PreferredUserName,
                        ClaimTypes.Name,
                        ClaimTypes.Role,
                        JwtClaimTypes.Role
                    },
                    Scopes = AllowedScopes
                },
                new ApiResource(VaultServiceApi, "The Vault Service API")
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.PreferredUserName,
                        ClaimTypes.Name,
                        ClaimTypes.Role,
                        JwtClaimTypes.Role
                    },
                    Scopes = AllowedScopes
                },
                new ApiResource(SyncServiceApi, "The Sync Service API")
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.PreferredUserName,
                        ClaimTypes.Name,
                        ClaimTypes.Role,
                        JwtClaimTypes.Role
                    },
                    Scopes = AllowedScopes
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "omikron-webapp-service-api",
                    ClientName = "The Web App Service based on SPA Angular",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("E8ECA697477D4F4CAEF34105C3BE110C".Sha256())
                    },
                    AllowedScopes = AllowedScopes,
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = SharedKernel.Constants.ServiceClientId,
                    ClientName = "The Client for Communication between API's",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("CE603006-9BA2-40AD-9046-35F1520432DB".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServiceApi,
                    }
                }
            };
        }

        public static IEnumerable<Permission> GetPermissions()
        {
            return new List<Permission>()
            {
                new Permission(1, new Guid("18BB49E7-69ED-47BD-BFC6-5372DD15315A"), PermissionConstants.TenantManagement.SearchTenants, PermissionDomain.TenantManagement, "Ability to see a page with a list of tenants alongside with search functionality."),
                new Permission(2, new Guid("9FEB92A5-398F-4069-8F5F-6DA5212AF3F2"), PermissionConstants.TenantManagement.AddTenant, PermissionDomain.TenantManagement, "Ability to see button for add tenant alongside with ability to create tenant. Required Permissions: Search.Tenants"),
                new Permission(3, new Guid("CBA862DC-FD5F-4C70-84A6-3FB16CB868E1"), PermissionConstants.TenantManagement.EditTenant, PermissionDomain.TenantManagement, "Ability to navigate to tenant details page and edit tenant. Required Permissions: Search.Tenants"),
                new Permission(4, new Guid("865DD083-5872-406A-BF8B-BD440F31D6FE"), PermissionConstants.TenantManagement.ReadTenant, PermissionDomain.TenantManagement, "Ability to navigate to tenant details page without ability to edit tenant. Required Permissions: Search.Tenants"),
                new Permission(5, new Guid("E445A667-0A50-470F-84D5-84959ADCA363"), PermissionConstants.TenantManagement.DeleteTenant, PermissionDomain.TenantManagement, "Ability to see delete action alongside with ability to delete tenant. Required Permissions: Search.Tenants, Edit.Tenant"),

                new Permission(6, new Guid("8D5E1370-D20C-4E2E-9A93-84F5D4E620C4"), PermissionConstants.UserManagement.SearchUsers, PermissionDomain.UserManagement, "Ability to see a page with a list of users alongside with search functionality."),
                new Permission(7, new Guid("0EBCB068-FE5B-4696-9392-ECA13615139C"), PermissionConstants.UserManagement.AddUser, PermissionDomain.UserManagement, "Ability to see button for add user alongside with ability to create user. Required Permissions: Search.Users"),
                new Permission(8, new Guid("9C0D6B32-6F5F-41A3-8336-43F33DD61477"), PermissionConstants.UserManagement.EditUser, PermissionDomain.UserManagement, "Ability to navigate to user details page and edit user. Required Permissions: Search.Users"),
                new Permission(9, new Guid("D686C12F-8AC8-4214-A1A2-E8B7D02B18AD"), PermissionConstants.UserManagement.DeleteUser, PermissionDomain.UserManagement, "Ability to see delete action alongside with ability to delete user. Required Permissions: Search.Users, Edit.User"),
                new Permission(10, new Guid("57C57548-266D-4CD3-A35E-DE256C6CEDD8"), PermissionConstants.UserManagement.ReadUser, PermissionDomain.UserManagement, "Ability to navigate to user details page without ability to edit user. Required Permissions: Search.Users"),

                new Permission(11, new Guid("42DC28E4-DECA-41BF-960F-F63D4AD41E43"), PermissionConstants.RoleManagement.SearchRoles, PermissionDomain.RoleManagement, "Ability to see a page with a list of roles alongside with search functionality."),
                new Permission(12, new Guid("DD37CF7C-DA69-4136-8D0B-96F2F40563C4"), PermissionConstants.RoleManagement.AddRole, PermissionDomain.RoleManagement, "Ability to see button for add role alongside with ability to create role. Required Permissions: Search.Roles"),
                new Permission(13, new Guid("E4CC40F5-2100-497D-8B58-33667376A99A"), PermissionConstants.RoleManagement.EditRole, PermissionDomain.RoleManagement, "Ability to navigate to role details page and edit role. Required Permissions: Search.Roles, Edit.Roles"),
                new Permission(14, new Guid("426D5137-C41C-4962-A1E0-3FA5C1DBBC45"), PermissionConstants.RoleManagement.DeleteRole, PermissionDomain.RoleManagement, "Ability to see delete action alongside with ability to delete role. Required Permissions: Search.Roles, Edit.Roles"),
                new Permission(15, new Guid("0D6DAC73-8728-4D9F-951F-EF4DB97C996F"), PermissionConstants.RoleManagement.ReadRole, PermissionDomain.RoleManagement, "Ability to navigate to role details page without ability to edit role. Required Permissions: Search.Roles"),
                new Permission(16, new Guid("68E3C31C-AADE-41D0-88BA-9048E7233B12"), PermissionConstants.RoleManagement.InteractiveView, PermissionDomain.RoleManagement, "Ability to run into interactive view in order to see required permissions for protected parts of the system."),

                new Permission(17, new Guid("77FADD97-22D3-489E-882E-B106F6805BDF"), PermissionConstants.AuditManagement.SearchDataChangeLogs, PermissionDomain.AuditManagement, "Ability to trace data for particular tenant. The permission includes also the ability to see data content what was changed."),
                new Permission(18, new Guid("C5B037E8-1DF3-4BC0-92FD-B1CD5C1A1D17"), PermissionConstants.TenantManagement.SearchDataChangeLogsByTenant, PermissionDomain.TenantManagement, "Ability to trace data for all tenants. The permission includes also the ability to see data content what was changed."),
            };
        }

        public static IEnumerable<PhoneNumber> GetPhoneNumbers()
        {
            return new List<PhoneNumber>()
            {
                new PhoneNumber
                {
                    Id = new Guid("7B84CBC0-8E92-408C-8703-D47DA88BCB66"),
                    Number = "+38761000000",
                    Token = 100000,
                    Verified = true,
                    VerificationAttempts = 0,
                    LockedOut = false,
                    TokenCreationTime = DateTime.UtcNow,
                    TokenExpired = false
                }
            };
        }

        public static IEnumerable<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role
                {
                    Id = 1,
                    ExternalId = new Guid("DFFA36F1-DF2A-419D-9941-A32509A14D69"),
                    Name = RoleConstants.TenantAdministratorRole,
                    Type = RoleType.System,
                    NormalizedName = RoleConstants.TenantAdministratorRole.Normalize().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("n"),
                    Description =
                        "Tenant Administrator Role is a system role and represents the role with the ability to managing users and roles for the tenant. This role cannot be deleted or edit since it's not a user-defined role."
                },
                new Role
                {
                    Id = 2,
                    ExternalId = new Guid("CB6165CB-4CDF-4FDC-9951-71A9A0485FB9"),
                    Name = RoleConstants.SystemTenantAdministratorRole,
                    Type = RoleType.System,
                    NormalizedName = RoleConstants.SystemTenantAdministratorRole.Normalize().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("n"),
                    Description =
                        "System Tenant Administrator Role is a system role and represents the role with the ability to managing tenants users and roles for the main tenant. This role cannot be deleted or edit since it's not a user-defined role."
                },
                new Role
                {
                    Id = 3,
                    ExternalId = new Guid("7BFC4797-714B-4FA2-B369-A5C73206800A"),
                    Name = RoleConstants.ReportingManagementRole,
                    Type = RoleType.System,
                    NormalizedName = RoleConstants.ReportingManagementRole.Normalize().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("n"),
                    Description =
                        "Reporting Management is a system role and represents the role with the ability to manage reports. This role cannot be deleted or edit since it's not a user-defined role."
                },
                new Role
                {
                    Id = 4,
                    ExternalId = new Guid("FC6BD0E0-A573-41CD-87E1-C2BE2CA27D7D"),
                    Name = RoleConstants.OmikronUser,
                    Type = RoleType.Client,
                    NormalizedName = RoleConstants.OmikronUser.Normalize().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("n"),
                    Description =
                        "Omikron user is a default role for Omikron customers."
                }
            };
        }
    }
}