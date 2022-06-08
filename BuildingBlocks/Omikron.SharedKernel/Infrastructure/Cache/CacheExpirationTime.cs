using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Cache
{
    public class CacheExpirationTime : ValueObject<CacheExpirationTime>
    {
        public static CacheExpirationTime NonExpiration = new CacheExpirationTime(TimeSpan.MaxValue);
        public static CacheExpirationTime DefaultExpiration = new CacheExpirationTime(TimeSpan.FromHours(1));

        public static CacheExpirationTime UserAccountPermissionExpiration =
            new CacheExpirationTime(TimeSpan.FromDays(1));

        public static CacheExpirationTime TenantExpiration = new CacheExpirationTime(TimeSpan.FromDays(5));

        private CacheExpirationTime(TimeSpan value)
        {
            Value = value;
        }

        private TimeSpan Value { get; }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { Value };

        public static CacheExpirationTime BudAccessTokenExpiration(int valueInSeconds) => new(TimeSpan.FromSeconds(valueInSeconds));

        public DateTimeOffset GetOffset()
        {
            return DateTimeOffset.UtcNow.Add(Value);
        }

        public TimeSpan GetTimeSpan()
        {
            return Value;
        }

        public static implicit operator TimeSpan(CacheExpirationTime value)
        {
            return value.GetTimeSpan();
        }
    }
}