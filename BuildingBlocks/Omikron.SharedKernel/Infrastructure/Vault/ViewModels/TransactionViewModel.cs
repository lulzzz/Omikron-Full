using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
	public class TransactionViewModel
	{
		public string MerchantLogo { get; set; }
		public string MerchantName { get; set; }
		public string TransactionInformation { get; set; }
		public string Category { get; set; }
		public decimal Amount { get; set; }
		public string Currency { get; set; }
		public string CreditDebitIndicator { get; set; }
	}

	public class TransactionViewModelProfile : Profile
	{
		public TransactionViewModelProfile()
		{
			CreateMap<Transaction, TransactionViewModel>()
				.ForMember(x => x.MerchantLogo, opt => opt.MapFrom(x => x.Merchant.Logo))
				.ForMember(x => x.MerchantName, opt => opt.MapFrom(x => x.Merchant.DisplayName ?? x.Merchant.Name))
				.ForMember(x => x.CreditDebitIndicator, opt => opt.MapFrom(x => x.CreditDebitIndicator != null ? x.CreditDebitIndicator.ToString() : ""));

			CreateMap<Transaction, VaultItemValueViewModel>()
				.ForMember(x => x.Type, x => x.MapFrom(s => s.Category));
		}
	}
}
