using System;
using System.Collections.Generic;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.ViewModel
{
    public class RoleSummaryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public RoleType Type { get; set; }

        public bool Enabled { get; set; }
        public bool IsSystemRole => Type == RoleType.System;

        public IReadOnlyList<string> AssignedPermissions { get; set; }

        public RoleSummaryViewModel(Role role)
        {
            Id = role.ExternalId;
            Name = role.Name;
            Enabled = role.Enabled;
            Description = role.Description;
            Type = role.Type;
        }
    }
}