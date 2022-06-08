using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;
using Microsoft.AspNetCore.Identity;

namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    public class User : IdentityUser<int>, IHasExternalId
    {
        public Guid ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string ProfilePhoto { get; set; }
        public string BudCustomerId { get; set; }
        public string BudCustomerSecret { get; set; }
        public UserTitle? Title { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public Guid PhoneNumberId { get; set; }
        public PhoneNumber PhoneNumberForVerification { get; set; }
        public DateTime? RememberMeAt { get; set; }
        public bool MarketingCommunications { get; set; }
        public bool AccountNotifications { get; set; }
		public DateTime RegistrationDate { get; set; }
		public virtual ICollection<ConfirmationToken> ConfirmationTokens { get; set; }

        public User()
        {
            ExternalId = Guid.NewGuid();
            ConfirmationTokens = new List<ConfirmationToken>();
            AccountStatus = AccountStatus.New;
        }
    }
}