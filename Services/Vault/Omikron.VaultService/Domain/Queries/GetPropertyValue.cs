using FluentValidation;
using MediatR;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetPropertyValue : IRequest<ApiResult<AssetValue>>
    {
        public class Query : BaseCommand.Action<ApiResult<AssetValue>>
        {
            public string PostCode { get; set; }
            public int NumberOfBedrooms { get; set; }
        }

        public class Validation : AbstractValidator<Query>
        {
            public Validation()
            {
                RuleFor(x => x.PostCode).NotEmpty();
                RuleFor(x => x.NumberOfBedrooms).NotEmpty();
            }
        }
    }
}