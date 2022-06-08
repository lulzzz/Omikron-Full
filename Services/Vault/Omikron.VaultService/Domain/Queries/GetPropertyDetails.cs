using System;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetPropertyDetails 
    {
        public class Query : BaseCommand.Action<ApiResult<PropertyViewModel>>
        {
            public Guid AccountId { get; set; }
        }

        public class Validation : AbstractValidator<Query>
        {
            public Validation()
            {
                RuleFor(x => x.AccountId).NotEmpty().NotNull();
            }
        }
    }
}