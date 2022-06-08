using System;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.ViewModel
{
    public class UserAccountViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhoto { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string BudCustomerId { get; set; }
        public string BudCustomerSecret { get; set; }
        public string [] Roles { get; set; }

        public UserAccountViewModel(User user)
        {
            Id = user.ExternalId;
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            ProfilePhoto = user.ProfilePhoto;
            AccountStatus = user.AccountStatus;
            EmailConfirmed = user.EmailConfirmed;
            PhoneNumber = user.PhoneNumber;
            BudCustomerId = user.BudCustomerId;
            BudCustomerSecret = user.BudCustomerSecret;
        }
    }
}