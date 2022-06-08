using System;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.ViewModel
{
    public class PermissionSummaryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionDomain Domain { get; set; }

        public PermissionSummaryViewModel()
        {
        }

        public PermissionSummaryViewModel(Permission permission)
        {
            Id = permission.ExternalId;
            Name = permission.Name;
            Description = permission.Description;
            Domain = permission.Domain;
        }
    }
}