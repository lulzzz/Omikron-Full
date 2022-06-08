using System;
using Microsoft.AspNetCore.Identity;
using Omikron.SharedKernel.Domain;

namespace Omikron.Sync.Model
{
    [Serializable]
    public sealed class User : IdentityUser<int>, IHasExternalId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BudCustomerId { get; set; }
        public string BudCustomerSecret { get; set; }
		public Guid ExternalId { get; set; }
    }
}