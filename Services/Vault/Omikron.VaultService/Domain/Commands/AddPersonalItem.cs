using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using Omikron.SharedKernel.Utils;
using System;

namespace Omikron.VaultService.Domain.Commands
{
	public class AddPersonalItem
	{
		public class Command : TenantCommand<ApiResult>
		{
			public Guid UserId { get; set; }
			public string ItemName { get; set; }
			public decimal Value { get; set; }
			public FinanceAgreement FinanceAgreement { get; set; }
			public Guid? ExistingFinanceAgreementId { get; set; }
			public string ItemPhoto { get; set; }
			public decimal? PurchaseValue { get; set; }
			public DateTime? PurchaseDate { get; set; }
		}

		public class FinanceAgreement
		{
			public string Name { get; set; }
			public decimal Balance { get; set; }
			public string Notes { get; set; }
			public string Reference { get; set; }
			public decimal? OpenBalance { get; set; }
			public DateTime? OpenDate { get; set; }
		}

		public class Validation : AbstractValidator<Command>
		{
			public Validation()
			{
				RuleFor(x => x.ItemName).NotEmpty().WithMessage("Please specify item name");
				RuleFor(x => x.Value).NotEmpty().GreaterThan(0).WithMessage("Please enter the value.");
				RuleFor(x => x.ExistingFinanceAgreementId).Null().When(x => x.FinanceAgreement.IsNotNull());
                RuleFor(x => x.FinanceAgreement).Null().When(x => x.ExistingFinanceAgreementId.HasValue);
                RuleFor(x => x.FinanceAgreement.Name).NotEmpty().When(x => x.FinanceAgreement != null);
				RuleFor(x => x.PurchaseDate).LessThanOrEqualTo(Clock.GetTime()).WithMessage("Please enter purchase date less than or equal to the current date.");
				RuleFor(x => x.PurchaseDate).NotEmpty().When(x => x.PurchaseValue.HasValue).WithMessage("Please enter purchase date alongside purchase value.");
				RuleFor(x => x.PurchaseValue).NotEmpty().When(x => x.PurchaseDate.HasValue).WithMessage("Please enter purchase value alongside purchase date.");
				RuleFor(x => x.PurchaseValue).GreaterThan(0).WithMessage("Please enter purchase value greater than zero.");
				RuleFor(x => x.FinanceAgreement.OpenDate).LessThanOrEqualTo(Clock.GetTime()).When(x => x.FinanceAgreement != null).WithMessage("Please enter finance agreement open date less than or equal to the current date.");
				RuleFor(x => x.FinanceAgreement.OpenDate).NotEmpty().When(x => x.FinanceAgreement != null && x.FinanceAgreement.OpenBalance.HasValue).WithMessage("Please enter finance agreement open date alongside open balance.");
				RuleFor(x => x.FinanceAgreement.OpenBalance).NotEmpty().When(x => x.FinanceAgreement != null && x.FinanceAgreement.OpenDate.HasValue).WithMessage("Please enter finance agreement open balance alongside open date.");
			}
		}
	}
}
