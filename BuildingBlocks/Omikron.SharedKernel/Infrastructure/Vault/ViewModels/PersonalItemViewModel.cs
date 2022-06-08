using System.Linq;
using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class PersonalItemViewModel
    {
        public string ItemName { get; set; }
        public decimal Value { get; set; }
        public string FinanceAgreementName { get; set; }
        public decimal? NewFinanceBalance { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
    }

    public class PersonalItemViewModelProfile : Profile
    {
        public PersonalItemViewModelProfile()
        {
            CreateMap<PersonalItem, PersonalItemViewModel>()
                .ForMember(x => x.ItemName, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.Value, x => x.MapFrom(s => s.PersonalItemValues.OrderByDescending(x => x.CreatedAt).First().Amount))
                .ForMember(x => x.Notes, x => x.MapFrom(s => s.FinancialAgreement.Notes))
                .ForMember(x => x.FinanceAgreementName, x => x.MapFrom(s => s.FinancialAgreement == null ? "" : s.FinancialAgreement.Name))
                .ForMember(x => x.Reference, x => x.MapFrom(s => s.FinancialAgreement == null ? "" : s.FinancialAgreement.ReferenceNumber))
                .ForMember(x => x.NewFinanceBalance, x =>
                {
                    x.NullSubstitute(0m);
                    x.MapFrom(s => s.FinancialAgreement.AccountBalances.OrderByDescending(transaction => transaction.CreatedAt).First().Amount);
                });
        }
    }
}