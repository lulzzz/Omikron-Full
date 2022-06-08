using Omikron.SharedKernel.Domain;
using System;

namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    public class PhoneNumber : BaseEntity<Guid>
    {
        public string Number { get; set; }
        public int Token { get; set; }
        public DateTime TokenCreationTime { get; set; }
        public bool TokenExpired { get; set; }
        public bool Verified { get; set; }
        public int VerificationAttempts { get; set; }
        public bool LockedOut { get; set; }
        public DateTime LockoutTime { get; set; }
        public bool IdentityTokenAvailable { get; set; }

        public User Owner { get; set; }
    }
}
