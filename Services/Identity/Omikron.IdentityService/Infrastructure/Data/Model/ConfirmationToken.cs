using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Utils;
using Finbuckle.MultiTenant;

namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    [MultiTenant]
    public class ConfirmationToken : ValueObject<ConfirmationToken>
    {
        public int Id { get; set; }
        public ConfirmationTokenType Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public  DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsExpired { get; set; }

        public ConfirmationToken()
        {
            CreatedAt = Clock.GetTime();
            Expiration = Clock.GetTime().Add(Constants.TokenExpiration);
            IsExpired = false;
        }

        public ConfirmationToken(ConfirmationTokenType type, string value) : this()
        {
            Type = type;
            Value = value;
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object>() { Value, Type };
    }
}