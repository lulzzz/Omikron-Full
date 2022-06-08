using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class AccountViewModel : AssetViewModel
    {
        public string Provider { get; set; }
        public string IdentificationNumber { get; set; }
        public AuthorizationStatus AuthorizationStatus { get; set; }
		public string AccountType { get; set; }
		public string AccountSource { get; set; }
        public AssetType AssetType { get; set; }
        public string ProviderColour { get; set; }
	}

    public class AccountViewModelProfile : Profile
    {
        public AccountViewModelProfile()
        {
            CreateMap<Account, AccountViewModel>()
                .ForMember(x => x.IdentificationNumber, opt => opt.MapFrom(x => x.IdentificationNumber.ToString()))
                .ForMember(x => x.AuthorizationStatus, opt => opt.MapFrom(x => AuthorizationStatus.Parse(x.ExpiryDate.Value)))
                .ForMember(x => x.AccountType, opt => opt.MapFrom(x => x.Type.ToString()))
                .ForMember(x => x.HostId, opt => opt.MapFrom(x => x.ExternalId));
        }
    }
}