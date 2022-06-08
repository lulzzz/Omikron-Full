using System;

namespace Omikron.IdentityService
{
    public static class Constants
    {
        public static string ServiceName = "Omikron.IdentityService";
        public static string ServiceDescription = $"Represent the {ServiceName} which is the main responsibility for managing users.";
        public static TimeSpan TokenExpiration => new TimeSpan(24, 0, 0);
        public const string DuplicateUserNameErrorCode = "DuplicateUserName";
        public static TimeSpan PhoneNumberTokenLockoutDuration => TimeSpan.FromMinutes(5);
        public static TimeSpan PhoneNumberTokenResendTime => TimeSpan.FromSeconds(15);
        public const int MaximumAllowedVerificationAttempts = 5;
        public const int RememberMeValidDays = 30;
        public const int VerificationTokenUpperBound = 1000000;
        public const int VerificationTokenLowerBound = 99999;
        


        // Should we move these to appsettings ?
        public const string BudProfileUuid = "edd65610-b1ae-4051-acfc-0fc52d4d5f64";
        public const string BudTransactionReference = "ppl-38";

        public const string KycCompleted = "live";
        public const string KycApproved = "green";
    }
}