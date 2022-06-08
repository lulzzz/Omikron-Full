using AutoMapper;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Messaging;
using Omikron.SharedKernel.Utils;
using System;
using static Omikron.VaultService.Domain.Commands.AddPersonalItem;

namespace Omikron.VaultService.Domain.Commands
{
    public class AddProperty
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string PropertyName { get; set; }
            public int NumberOfBedrooms { get; set; }
            public string Postcode { get; set; }
            public decimal PropertyValue { get; set; }
            public bool AutomaticallyReValueProperty { get; set; }
            public string UserId { get; set; }
            public string Address { get; set; }
            public FinanceAgreement Mortgage { get; set; }
            public Guid? ExistingMortgageId { get; set; }
            public string PropertyPhoto { get; set; }
            public decimal? PurchaseValue { get; set; }
            public DateTime? PurchaseDate { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.PropertyName).NotEmpty();
                RuleFor(x => x.NumberOfBedrooms).NotEmpty();
                RuleFor(x => x.Postcode).NotEmpty();
                RuleFor(x => x.PropertyValue).NotEmpty().GreaterThan(0);
                RuleFor(x => x.UserId).NotEmpty();
                RuleFor(x => x.ExistingMortgageId).Null().When(x => x.Mortgage.IsNotNull());
                RuleFor(x => x.Mortgage).Null().When(x => x.ExistingMortgageId.HasValue);
                RuleFor(x => x.Mortgage.Name).NotEmpty().When(x => x.Mortgage != null); 
                RuleFor(x => x.PurchaseDate).NotEmpty().When(x => x.PurchaseValue.HasValue).WithMessage("Please enter purchase date alongside purchase value.");
                RuleFor(x => x.PurchaseDate).LessThanOrEqualTo(Clock.GetTime()).When(x => x.PurchaseDate.HasValue).WithMessage("Please enter purchase date less than or equal to the current date.");
                RuleFor(x => x.PurchaseValue).NotEmpty().When(x => x.PurchaseDate.HasValue).WithMessage("Please enter purchase value alongside purchase date.");
                RuleFor(x => x.PurchaseValue).GreaterThan(0).WithMessage("Please enter purchase value greater than zero.");
                RuleFor(x => x.Mortgage.OpenDate).NotEmpty().When(x => x.Mortgage != null && x.Mortgage.OpenBalance.HasValue).WithMessage("Please enter mortgage open date alongside open balance.");
                RuleFor(x => x.Mortgage.OpenDate).LessThanOrEqualTo(Clock.GetTime()).When(x => x.Mortgage != null && x.Mortgage.OpenDate.HasValue).WithMessage("Please enter mortgage open date less than or equal to current date.");
                RuleFor(x => x.Mortgage.OpenBalance).NotEmpty().When(x => x.Mortgage != null && x.Mortgage.OpenDate.HasValue).WithMessage("Please enter mortgage open balance alongside open date.");
            }
        }
    }

    public class AddPropertyProfile : Profile
    {
        public AddPropertyProfile()
        {
            CreateMap<AddProperty.Command, Property>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.PropertyName))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(s => s.PropertyPhoto))
                .ForMember(x => x.Mortgage, x => x.Ignore());
        }
    }
}