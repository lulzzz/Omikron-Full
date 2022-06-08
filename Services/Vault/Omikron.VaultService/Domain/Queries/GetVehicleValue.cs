using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetVehicleValue
    {
        public class Query : BaseCommand.Action<ApiResult<AssetValue>>
        {
            public string Registration { get; set; }
            public int Mileage { get; set; }
        }

        public class Validation : AbstractValidator<Query>
        {
            public Validation()
            {
                RuleFor(x => x.Registration).NotEmpty();
                RuleFor(x => x.Mileage).NotEmpty();
            }
        }
    }
}