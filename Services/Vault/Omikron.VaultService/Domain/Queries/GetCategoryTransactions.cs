using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.ViewModels;
using System;
using System.Collections.Generic;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetCategoryTransactions
    {
        public class Query : BaseCommand.Action<ApiResult<CategoryTransactionsViewModel>>
        {
            public Guid UserId { get; set; }
			public int? MonthIndex { get; set; }
			public int Year { get; set; }
			public IEnumerable<string> Categories { get; set; }
			public IEnumerable<string> AssetLiabilityTypes { get; set; }
			public IEnumerable<Guid> VaultEntries { get; set; }
		}

        public class Validation : AbstractValidator<Query>
		{
			public Validation()
			{
				RuleFor(x => x.MonthIndex).GreaterThan(0).LessThanOrEqualTo(12).When(x => x.MonthIndex.HasValue).WithMessage("Invalid month index.");
				RuleFor(x => x.Year).LessThanOrEqualTo(Clock.GetTime().Year).WithMessage($"Year cannot be greater than {Clock.GetTime().Year}.");
			}
		}
	}
}
