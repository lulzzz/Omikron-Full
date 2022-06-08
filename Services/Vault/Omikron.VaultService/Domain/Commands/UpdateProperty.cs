using System;
using AutoMapper;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Messaging;

namespace Omikron.VaultService.Domain.Commands
{
    public class UpdateProperty
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string PropertyName { get; set; }
            public int NumberOfBedrooms { get; set; }
            public string Postcode { get; set; }
            public decimal PropertyValue { get; set; }
            public bool PropertyValueChange { get; set; }
            public bool AutomaticallyReValueProperty { get; set; }
            public string Address { get; set; }
            public string FinanceAgreementName { get; set; }
            public string Reference { get; set; }
            public decimal NewFinanceBalance { get; set; }
            public bool MortgageBalanceChange { get; set; }
            public string Notes { get; set; }
            public Guid PropertyId { get; set; }
            public string PropertyPhoto { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.PropertyId).NotEmpty();
                RuleFor(x => x.PropertyName).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.Postcode).NotEmpty();
                RuleFor(x => x.PropertyValue).NotEmpty();
            }
        }
    }

    public class UpdatePropertyProfile : Profile
    {
        public UpdatePropertyProfile()
        {
            CreateMap<UpdateProperty.Command, Property>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.PropertyName))
                .ForMember(x => x.NumberOfBedrooms, x => x.MapFrom(s => s.NumberOfBedrooms))
                .ForMember(x => x.Postcode, x => x.MapFrom(s => s.Postcode))
                .ForMember(x => x.AutomaticallyReValueProperty, x => x.MapFrom(s => s.AutomaticallyReValueProperty))
                .ForMember(x => x.Address, x => x.MapFrom(s => s.Address))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(s => s.PropertyPhoto));

            CreateMap<UpdateProperty.Command, Account>()
                .ForMember(x => x.ReferenceNumber, x => x.MapFrom(s => s.Reference))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.FinanceAgreementName));
        }
    }
}
