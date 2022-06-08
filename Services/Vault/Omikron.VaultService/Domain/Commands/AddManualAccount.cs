using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using Omikron.SharedKernel.Utils;
using System;

namespace Omikron.VaultService.Domain.Commands
{
	public class AddManualAccount
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Guid Id { get; } = Guid.NewGuid();
            public Guid OwnerId { get; set; }
            public string Name { get; set; }
            public decimal Balance { get; set; }
            public string Provider { get; set; }
            public string ReferenceNumber { get; set; }
            public string Notes { get; set; }
            public int Type { get; set; }
			public int? CreditDebitIndicator { get; set; }
			public decimal? OpenBalance { get; set; }
			public DateTime? OpenDate { get; set; }
		}

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.OwnerId).NotEmpty().WithMessage("The owner ID is missing.");
                RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter the account name.");
                RuleFor(x => x.Balance).NotEmpty().GreaterThan(0).WithMessage("Please enter the account balance.");
                RuleFor(x => x.Type).NotEmpty().WithMessage("Please selecte the account type.");
                RuleFor(x => x.CreditDebitIndicator).NotEmpty().When(x => x.Type == 1).WithMessage("Please select the credit debit indicator.");
				RuleFor(x => x.OpenDate).NotEmpty().When(x => x.OpenBalance.HasValue).WithMessage("Please enter open date alongside open balance.");
				RuleFor(x => x.OpenDate).LessThanOrEqualTo(Clock.GetTime()).WithMessage("Please enter open date less than or equal to the current date.");
                RuleFor(x => x.OpenBalance).NotEmpty().When(x => x.OpenDate.HasValue).WithMessage("Please enter open balance alongside open date.");
            }
        }
    }
}