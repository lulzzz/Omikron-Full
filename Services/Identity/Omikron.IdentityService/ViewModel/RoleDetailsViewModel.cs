using System.Collections.Generic;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.ViewModel
{
    public class RoleDetailsViewModel : RoleSummaryViewModel
    {
        public IReadOnlyList<RolePermissionViewModel> Permissions { get; set; }
        public bool CanBeDeleted => NumberOfUsersWithRole == 0 && Type == RoleType.Client;
        public int NumberOfUsersWithRole { get; set; }

        public RoleDetailsViewModel(Role role) : base(role)
        {
        }
    }
}