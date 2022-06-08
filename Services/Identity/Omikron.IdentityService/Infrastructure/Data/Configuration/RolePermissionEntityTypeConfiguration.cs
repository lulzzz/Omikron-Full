using System.Linq;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Omikron.IdentityService.Infrastructure.Data.Configuration
{
    public class RolePermissionEntityTypeConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(p => new { p.PermissionId, p.RoleId });
            var roles = Config.GetRoles();
            var permissions = Config.GetPermissions();

            var systemTenantAdministratorRole = roles.FirstOrDefault(r => r.Name == RoleConstants.SystemTenantAdministratorRole);
            var tenantAdministratorRole = roles.FirstOrDefault(r => r.Name == RoleConstants.TenantAdministratorRole);
            var reportingManagementRole = roles.FirstOrDefault(r => r.Name == RoleConstants.ReportingManagementRole);

            var permissionsForSystemTenantAdministratorRole = permissions.Select(p => new RolePermission { PermissionId = p.Id, RoleId = systemTenantAdministratorRole.Id }).ToList();
            var permissionsForTenantAdministratorRole = permissions.Where(x => x.Domain == PermissionDomain.UserManagement || x.Domain == PermissionDomain.RoleManagement || x.Name == PermissionConstants.AuditManagement.SearchDataChangeLogs)
                .Select(p => new RolePermission { PermissionId = p.Id, RoleId = tenantAdministratorRole.Id }).ToList();
            var permissionsForReportingManagementRole = permissions.Where(x => x.Domain == PermissionDomain.ReportManagement)
                .Select(p => new RolePermission { PermissionId = p.Id, RoleId = reportingManagementRole.Id }).ToList();

            builder.HasData(permissionsForSystemTenantAdministratorRole);
            builder.HasData(permissionsForTenantAdministratorRole);
            builder.HasData(permissionsForReportingManagementRole);
        }
    }
}