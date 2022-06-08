using System;
using AutoMapper;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Messaging;

namespace Omikron.VaultService.Domain.Commands
{
    public class UpdateInvestment
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string InvestmentName { get; set; }
            public string Category { get; set; }
            public string TickerCode { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal TotalValue { get; set; }
            public bool AutomaticallyRevalueInvestment { get; set; }
            public Guid InvestmentId { get; set; }
            public bool InvestmentValueChanged { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.InvestmentName).NotEmpty().WithMessage("Please enter the investment name.");
                RuleFor(x => x.UnitPrice).NotEmpty().GreaterThan(0).WithMessage("Please enter the unit price.");
                RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0).WithMessage("Please enter the quantity.");
                RuleFor(x => x.TotalValue).NotEmpty().WithMessage("Total value is missing.");
            }
        }
    }

    public class UpdateInvestmentProfile : Profile
    {
        public UpdateInvestmentProfile()
        {
            CreateMap<UpdateInvestment.Command, Investment>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.InvestmentName));
        }
    }
}