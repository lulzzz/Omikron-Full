using System;
using FluentValidation;
using MediatR;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetPersonalItemDetails
    {
        public class Query : BaseCommand.Action<ApiResult<PersonalItemViewModel>>
        {
            public Guid AccountId { get; set; }
        }

        public class Valiadtion : AbstractValidator<Query>
        {
            public Valiadtion()
            {
                RuleFor(x => x.AccountId).NotEmpty();
            }
        }
    }
}