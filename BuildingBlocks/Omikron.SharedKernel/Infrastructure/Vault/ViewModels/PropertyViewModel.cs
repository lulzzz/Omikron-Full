using System;
using System.Linq;
using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class PropertyViewModel
    {
        public string PropertyName { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string Postcode { get; set; }
        public decimal PropertyValue { get; set; }
        public bool AutomaticallyReValueProperty { get; set; }
        public string Address { get; set; }
        public string FinanceAgreementName { get; set; }
        public string Reference { get; set; }
        public decimal? NewFinanceBalance { get; set; }
        public string Notes { get; set; }
        public Guid PropertyId { get; set; }
    }

    public class PropertyViewModalProfile : Profile
    {
        public PropertyViewModalProfile()
        {
            CreateMap<Property, PropertyViewModel>()
                .ForMember(x => x.PropertyName, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.NumberOfBedrooms, x => x.MapFrom(s => s.NumberOfBedrooms))
                .ForMember(x => x.Postcode, x => x.MapFrom(s => s.Postcode))
                .ForMember(x => x.AutomaticallyReValueProperty, x => x.MapFrom(s => s.AutomaticallyReValueProperty))
                .ForMember(x => x.Address, x => x.MapFrom(s => s.Address))
                .ForMember(x => x.PropertyValue, x => x.MapFrom(s => s.PropertyValues.OrderByDescending(p => p.CreatedAt).First().Amount))
                .ForMember(x => x.PropertyId, x => x.MapFrom(s => s.Id))

                .ForMember(x => x.Reference, x => x.MapFrom(s => s.Mortgage == null ? "" : s.Mortgage.ReferenceNumber))
                .ForMember(x => x.FinanceAgreementName, x => x.MapFrom(s => s.Mortgage == null ? "" : s.Mortgage.Name))
                .ForMember(x => x.Notes, x => x.MapFrom(s => s.Mortgage == null ? "" : s.Mortgage.Notes))
                .ForMember(x => x.NewFinanceBalance, x =>
                {
                    x.NullSubstitute(0m);
                    x.MapFrom(s => s.Mortgage.AccountBalances.OrderByDescending(transaction => transaction.CreatedAt).First().Amount);
                });
        }
    }
}