using System;
using System.Linq;
using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class InvestmentViewModel
    {
        public string InvestmentName { get; set; }
        public string Category { get; set; }
        public string TickerCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public bool AutomaticallyRevalueInvestment { get; set; }
    }

    public class InvestmentVieWModelProfile : Profile
    {
        public InvestmentVieWModelProfile()
        {
            CreateMap<Investment, InvestmentViewModel>()
                .ForMember(x => x.InvestmentName, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.Category, x => x.MapFrom(s => s.Category))
                .ForMember(x => x.TickerCode, x => x.MapFrom(s => s.TickerCode))
                .ForMember(x => x.AutomaticallyRevalueInvestment, x => x.MapFrom(s => s.AutomaticallyRevalueInvestment))
                .ForMember(x => x.Quantity, x => x.MapFrom(s => s.Quantity))
                .ForMember(x => x.TotalValue, x => x.MapFrom(s => s.TotalValue))
                .ForMember(x => x.UnitPrice, x => x.MapFrom(s => s.UnitPrice));
        }
    }
}