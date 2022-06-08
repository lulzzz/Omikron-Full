using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using System;
using System.Collections.Generic;

namespace Omikron.VaultService.Domain.Queries
{
	public class GetNetPositionsChartData
    {
        public class Query : BaseCommand.Action<ApiResult<IEnumerable<NetPositionsChartViewModel>>>
        {
            public Guid UserId { get; set; }
			public bool MonthMode { get; set; }
			public int? Year { get; set; }
			public IEnumerable<string> AssetLiabilityTypes { get; set; }
			public IEnumerable<Guid> VaultEntries { get; set; }
			public bool ArchivedAccounts { get; set; }
        }

        public class Validation : AbstractValidator<Query>
		{
			public Validation()
			{
				RuleFor(x => x.Year).NotNull().When(x => x.MonthMode).WithMessage("Year has to be provided in month mode.");
				RuleFor(x => x.Year).Null().When(x => !x.MonthMode).WithMessage("Year cannot be provided in year mode.");
			}
		}
	}
}
