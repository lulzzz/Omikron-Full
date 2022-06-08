using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using Omikron.SharedKernel.Utils;
using System;

namespace Omikron.VaultService.Domain.Commands
{
    public class AddInvestment
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Guid Id { get; } = Guid.NewGuid();
            public Guid OwnerId { get; set; }
            public string InvestmentName { get; set; }
            public string Category { get; set; }
            public string TickerCode { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal TotalValue { get; set; }
            public bool AutomaticallyRevalueInvestment { get; set; }
            public decimal? PurchaseValue { get; set; }
            public DateTime? PurchaseDate { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.OwnerId).NotEmpty().WithMessage("The owner ID is missing.");
                RuleFor(x => x.InvestmentName).NotEmpty().WithMessage("Please enter the investment name.");
                RuleFor(x => x.UnitPrice).NotEmpty().GreaterThan(0).WithMessage("Please enter the unit price.");
                RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0).WithMessage("Please enter the quantity.");
                RuleFor(x => x.TotalValue).NotEmpty().WithMessage("Total value is missing.");
                RuleFor(x => x.PurchaseDate).NotEmpty().When(x => x.PurchaseValue.HasValue).WithMessage("Please enter purchase date alongside purchase value.");
                RuleFor(x => x.PurchaseDate).LessThanOrEqualTo(Clock.GetTime()).WithMessage("Please enter purchase date less than or equal to current date.");
                RuleFor(x => x.PurchaseValue).NotEmpty().When(x => x.PurchaseDate.HasValue).WithMessage("Please enter purchase value alongside purchase date.");
                RuleFor(x => x.PurchaseValue).GreaterThan(0).WithMessage("Please enter purchase value grater than zero.");
            }
        }
    }
}