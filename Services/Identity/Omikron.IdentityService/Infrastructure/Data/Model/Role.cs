using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;

namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    public class Role : IdentityRole<int>, IHasExternalId
    {
        public Guid ExternalId { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public RoleType Type { get; set; }
        public virtual ICollection<RolePermission> AssignedPermissions { get; set; }

        public Role()
        {
            ExternalId = Guid.NewGuid();
            Type = RoleType.Client;
            Enabled = true;
        }
    }
}