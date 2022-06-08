using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System.Linq;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class ManualAccountViewModel
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Notes { get; set; }
        public string OwnerId { get; set; }
        public string ReferenceNumber { get; set; }
        public string LoanType { get; set; }
        public string AssetId { get; set; }
        public string AssetType { get; set; }
    }

    public class ManualAccountViewModelProfile : Profile
    {
        public ManualAccountViewModelProfile()
        {
            CreateMap<Account, ManualAccountViewModel>()
                .ForMember(x => x.ReferenceNumber, x => x.MapFrom(s => s.ReferenceNumber))
                .ForMember(x => x.Balance, x => x.MapFrom(s => s.AccountBalances.OrderByDescending(x => x.EntryDate).FirstOrDefault().Amount))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.Type, x => x.MapFrom(s => s.Type.Id))
                .ForMember(x => x.OwnerId, x => x.MapFrom(s => s.OwnerId.Id))
                .ForMember(x => x.LoanType, x => x.MapFrom(s => s.LoanType != null ? s.LoanType.Name : null));
        }
    }
}