using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    public class Permission : AggregateRoot<int>, IHasExternalId
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public PermissionDomain Domain { get; set; }    
        public virtual ICollection<RolePermission> AssignedRoles { get; set; }

        public Permission()
        {
        }

        public Permission(int id, Guid externalId, string name, PermissionDomain domain, string description)
        {
            Id = id;
            ExternalId = externalId;
            Name = name;
            NormalizedName = name.Normalize().ToUpper();
            Description = description;
            Domain = domain;
        }
    }
}