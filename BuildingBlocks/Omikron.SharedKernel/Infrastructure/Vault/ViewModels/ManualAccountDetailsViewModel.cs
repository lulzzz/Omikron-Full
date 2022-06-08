using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Investment;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class ManualAccountDetailsViewModel
    {
        public string Name { get; set; }
        public string FinanceName { get; set; }
        public AssetType FinanceType { get; set; }
        public Guid? FinanceId { get; set; }
        public Guid? AccountId { get; set; }
        public List<IBaseManualAccountDetailsVaultItem> Details { get; set; }
        public decimal TotalBalance { get; set; }
        public IEnumerable<VaultItemValueViewModel> Transactions { get; set; }
        public string CurrencyCode { get; set; }
        public string Notes { get; set; }
        public CreditDebitIndicator CreditDebitIndicator { get; set; }
        public string ItemPhoto { get; set; }
    }

    public class ManualAccountDetailsViewModelProfile : Profile
    {
        public ManualAccountDetailsViewModelProfile()
        {
            var AccountId = Guid.Empty;

            CreateMap<Account, ManualAccountDetailsViewModel>()
                .Include<Account, MortgageManualAccountViewModel>()
                .Include<Account, VehicleFinanceViewModel>()
                .Include<Account, PersonalItemFinanceViewModel>()
                .ForMember(x => x.TotalBalance, x =>
                {
                    x.Condition(s => s.AccountBalances != null && s.AccountBalances.Any());
                    x.MapFrom(account => account.AccountBalances.OrderByDescending(transaction => transaction.EntryDate).First().Amount);
                })
                .ForMember(x => x.CreditDebitIndicator, x =>
                {
                    x.Condition(s => s.AccountBalances != null && s.AccountBalances.Any());
                    x.MapFrom(account => account.AccountBalances.OrderByDescending(transaction => transaction.EntryDate).FirstOrDefault().CreditDebitIndicator);
                })
                .ForMember(x => x.Details, x => x.MapFrom(s => new List<IBaseManualAccountDetailsVaultItem>(){new ReferenceValueObject(s)}))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.Notes, x => x.MapFrom(s => s.Notes))
                .ForMember(x => x.FinanceId, x => x.MapFrom(s => s.Id))
                .ForMember(x => x.CurrencyCode, x => x.MapFrom(s => s.Currency))
                .ForMember(x => x.Transactions, x => x.MapFrom(s => s.Transactions));

            CreateMap<Account, MortgageManualAccountViewModel>()
                .ForMember(x => x.FinanceType, x => x.MapFrom(s => AssetType.Property))
                .ForMember(x => x.FinanceName, x =>
                {
                    x.Condition(s => s.Properties != null && s.Properties.Any(p => p.Id == AccountId));
                    x.MapFrom(s => s.Properties.First(property => property.Id == AccountId).Name);
                })
                .ForMember(x => x.AccountId, x => x.MapFrom(s => AccountId));

            CreateMap<Account, VehicleFinanceViewModel>()
                .IncludeAllDerived()
                .ForMember(x => x.FinanceType, x => x.MapFrom(s => AssetType.Vehicle))
                .ForMember(x => x.FinanceName, x =>
                {
                    x.Condition(s => s.Vehicles != null && s.Vehicles.Any(p => p.Id == AccountId));
                    x.MapFrom(s => s.Vehicles.First(vehicle => vehicle.Id == AccountId).Name);
                })
                .ForMember(x => x.AccountId, x => x.MapFrom(s => AccountId));

            CreateMap<Account, PersonalItemFinanceViewModel>()
                .IncludeAllDerived()
                .ForMember(x => x.FinanceType, x => x.MapFrom(s => AssetType.PersonalItem))
                .ForMember(x => x.FinanceName, x =>
                {
                    x.Condition(s => s.PersonalItems != null && s.PersonalItems.Any(p => p.Id == AccountId));
                    x.MapFrom(s => s.PersonalItems.First(personalItem => personalItem.Id == AccountId).Name);
                })
                .ForMember(x => x.AccountId, x => x.MapFrom(s => AccountId));

            CreateMap<Vehicle, ManualAccountDetailsViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.FinanceName, x => x.MapFrom(s => s.FinancialAgreement.Name))
                .ForMember(x => x.FinanceType, x => x.MapFrom(s => AssetType.VehicleFinance))
                .ForMember(x => x.FinanceId, x => x.MapFrom(s => s.FinancialAgreement.Id))
                .ForMember(x => x.AccountId, x => x.MapFrom(s => s.Id))
                .ForMember(x => x.CurrencyCode, x => x.MapFrom(s => s.Currency))
                .ForMember(x => x.ItemPhoto, x => x.MapFrom(s => s.ImageUrl))
                .ForMember(x => x.Details, x =>
                {
                    x.MapFrom(s => new List<IBaseManualAccountDetailsVaultItem>()
                    {
                        new MileageValueObject(s.Mileage),
                        new RegistrationValueObject(s.Registration),
                        new ValueItems.Vehicle.ValueValueObject(s),
                        new FinanceBalanceValueObject(s.FinancialAgreement),
                        new ReferenceValueObject(s.FinancialAgreement)
                    });
                })
                .ForMember(x => x.Transactions, x => x.MapFrom(s => s.VehicleValues))
                .ForMember(x => x.Notes, x =>
                {
                    x.Condition(s => s.FinancialAgreement != null);
                    x.MapFrom(s => s.FinancialAgreement.Notes);
                });

            CreateMap<Property, ManualAccountDetailsViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.FinanceName, x => x.MapFrom(s => s.Mortgage.Name))
                .ForMember(x => x.FinanceType, x => x.MapFrom(s => AssetType.Mortgage))
                .ForMember(x => x.FinanceId, x => x.MapFrom(s => s.Mortgage.Id))
                .ForMember(x => x.AccountId, x => x.MapFrom(s => s.Id))
                .ForMember(x => x.CurrencyCode, x => x.MapFrom(s => s.Currency))
                .ForMember(x => x.ItemPhoto, x => x.MapFrom(s => s.ImageUrl))
                .ForMember(x => x.Details, x =>
                {
                    x.MapFrom(s => new List<IBaseManualAccountDetailsVaultItem>()
                    {
                        new AddressValueObject(s.Address),
                        new ValueItems.Property.ValueValueObject(s),
                        new MortgageBalanceValueObject(s.Mortgage),
                        new BedroomsValueObject(s.NumberOfBedrooms),
                        new ReferenceValueObject(s.Mortgage)
                    });
                })
                .ForMember(x => x.Transactions, x => x.MapFrom(s => s.PropertyValues))
                .ForMember(x => x.Notes, x =>
                {
                    x.Condition(s => s.Mortgage != null);
                    x.MapFrom(s => s.Mortgage.Notes);
                });

            CreateMap<Investment, ManualAccountDetailsViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.TotalBalance, x => x.MapFrom(s => Convert.ToDecimal(s.TotalValue)))
                .ForMember(x => x.AccountId, x => x.MapFrom(s => s.Id))
                .ForMember(x => x.CurrencyCode, x => x.MapFrom(s => s.Currency))
                .ForMember(x => x.Details, x => x.MapFrom(s => new List<IBaseManualAccountDetailsVaultItem>()
                    {
                        new CategoryValueObject(s.Category),
                        new TickerCodeValueObject(s.TickerCode),
                        new UnitPriceValueObject(s.UnitPrice),
                        new QuantityValueObject(s.Quantity)
                    })
                )
                .ForMember(x => x.Transactions, x => x.MapFrom(s => s.InvestmentValues.OrderByDescending(x => x.EntryDate)));

            CreateMap<PersonalItem, ManualAccountDetailsViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.FinanceType, x => x.MapFrom(s => AssetType.PersonalItemFinance))
                .ForMember(x => x.FinanceName, x => x.MapFrom(s => s.FinancialAgreement.Name))
                .ForMember(x => x.AccountId, x => x.MapFrom(s => s.Id))
                .ForMember(x => x.FinanceId, x => x.MapFrom(s => s.FinancialAgreement.Id))
                .ForMember(x => x.CurrencyCode, x => x.MapFrom(s => s.Currency))
                .ForMember(x => x.ItemPhoto, x => x.MapFrom(s => s.ImageUrl))
                .ForMember(x => x.Details, x =>
                {
                    x.MapFrom(s => new List<IBaseManualAccountDetailsVaultItem>()
                    {
                        new FinanceBalanceValueObject(s.FinancialAgreement),
                        new ReferenceValueObject(s.FinancialAgreement)
                    });
                })
                .ForMember(x => x.Transactions, x => x.MapFrom(s => s.PersonalItemValues.OrderByDescending(v => v.EntryDate)))
                .ForMember(x => x.Notes, x =>
                {
                    x.Condition(s => s.FinancialAgreement != null);
                    x.MapFrom(s => s.FinancialAgreement.Notes);
                });

            CreateMap<VaultItemValue<Guid>, VaultItemValueViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(s => s.EntryDate))
                .ForMember(x => x.Currency, x => x.MapFrom(s => "GBP"))
                .ForMember(x => x.Type, x => x.MapFrom(s => "Value Change"));

            CreateMap<Transaction, VaultItemValueViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(s => s.Date))
                .ForMember(x => x.Amount, x => x.MapFrom(s => s.Amount))
                .ForMember(x => x.Type, x => x.MapFrom(s => s.Category))
                .ForMember(x => x.Currency, x => x.MapFrom(s => s.Currency));
        }
    }
}