namespace Omikron.SharedKernel.Security
{
    public static class PermissionConstants
    {
        public static class TenantManagement
        {
            public const string SearchTenants = "Search.Tenants";
            public const string AddTenant = "Add.Tenant";
            public const string EditTenant = "Edit.Tenant";
            public const string ReadTenant = "Read.Tenant";
            public const string DeleteTenant = "Delete.Tenant";
            public const string SearchDataChangeLogsByTenant = "Search.DataChangeLogs.ByTenant";
        }

        public static class UserManagement
        {
            public const string SearchUsers = "Search.Users";
            public const string AddUser = "Add.User";
            public const string EditUser = "Edit.User";
            public const string DeleteUser = "Delete.User";
            public const string ReadUser = "Read.User";
        }

        public static class RoleManagement
        {
            public const string SearchRoles = "Search.Roles";
            public const string AddRole = "Add.Role";
            public const string EditRole = "Edit.Role";
            public const string DeleteRole = "Delete.Role";
            public const string ReadRole = "Read.Role";
            public const string InteractiveView = "Interactive.View";
        }

        public static class AuditManagement
        {
            public const string SearchDataChangeLogs = "Search.DataChangeLogs";
        }

        public static class ReportManagement
        {
            public const string ReportSampleReport = "Report.SampleReport";
        }
    }
}