using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard;
using System;
using System.Collections.Generic;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetDashboardChartData
	{
        public class Query : BaseCommand.Action<ApiResult<IEnumerable<DashboardChartViewModel>>>
        {
            public Guid UserId { get; set; }
			public bool AllTimeFilter { get; set; }
			public int? NumberOfMonths { get; set; }

            public Query(Guid userId, bool allTimeFilter, int? numberOfMonths)
            {
				UserId = userId;
				AllTimeFilter = allTimeFilter;
				NumberOfMonths = numberOfMonths;
            }

            public Query() { }
        }

        public class Validation : AbstractValidator<Query>
		{
			public Validation()
			{
				RuleFor(x => x.NumberOfMonths).NotNull().When(x => x.AllTimeFilter).WithMessage("Number of months filter must not be empty!");
			}
		}
	}
}