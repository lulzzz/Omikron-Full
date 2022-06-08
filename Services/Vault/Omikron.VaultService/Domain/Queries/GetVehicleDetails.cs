using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetVehicleDetails
    {
        public class Query : BaseCommand.Action<ApiResult<VehicleViewModel>>
        {
            public Guid AccountId { get; set; }
        }

        public class Validation : AbstractValidator<Query>
        {
            public Validation()
            {
                RuleFor(x => x.AccountId).NotNull().NotEmpty();
            }
        }
    }
}