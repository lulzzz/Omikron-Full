using Omikron.IdentityService.Infrastructure.Data.Model;
using System.Text.Json.Serialization;

namespace Omikron.IdentityService.ViewModel
{
    public class RolePermissionViewModel : PermissionSummaryViewModel
    {
        [JsonIgnore]
        public int PermissionId { get; set; }

        public bool IsAssigned { get; set; }

        public RolePermissionViewModel()
        {
        }

        public RolePermissionViewModel(Permission permission) : base(permission)
        {
            PermissionId = permission.Id;
        }
    }
}