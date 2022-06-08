using AutoMapper;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.ViewModel
{
    public class ProfileDetailsViewModel
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool MarketingCommunications { get; set; }
        public bool AccountNotifications { get; set; }
    }

    public class UserProfileDetailsViewModel : Profile
    {
        public UserProfileDetailsViewModel()
        {
            CreateMap<User, ProfileDetailsViewModel>()
                .ForMember(dest => dest.PhoneNumber, x => x.MapFrom(src => src.PhoneNumberForVerification.Number));
        }
    }
}
