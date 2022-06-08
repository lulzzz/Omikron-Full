using System;
using AutoMapper;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Messaging;

namespace Omikron.VaultService.Domain.Commands
{
    public class UpdatePersonalItem
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string ItemName { get; set; }
            public decimal Value { get; set; }
            public string FinanceAgreementName { get; set; }
            public decimal NewFinanceBalance { get; set; }
            public string Reference { get; set; }
            public string Notes { get; set; }
            public Guid PersonalItemId { get; set; }
            public bool PersonalItemValueChange { get; set; }
            public bool FinanceBalanceChange { get; set; }
            public string ItemPhoto { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.ItemName).NotEmpty();
                RuleFor(x => x.Value).NotEmpty();
            }
        }
    }

    public class UpdatePersonalItemCommandProfile : Profile
    {
        public UpdatePersonalItemCommandProfile()
        {
            CreateMap<UpdatePersonalItem.Command, PersonalItem>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.ItemName))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(s => s.ItemPhoto));

            CreateMap<UpdatePersonalItem.Command, Account>()
                .ForMember(x => x.ReferenceNumber, x => x.MapFrom(s => s.Reference))
                .ForMember(x => x.Notes, x => x.MapFrom(s => s.Notes))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.FinanceAgreementName));
        }
    }
}