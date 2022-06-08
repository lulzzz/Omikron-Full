using System;
using AutoMapper;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Messaging;

namespace Omikron.VaultService.Domain.Commands
{
    public class UpdateVehicle
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Guid VehicleId { get; set; }
            public string VehicleName { get; set; }
            public string Registration { get; set; }
            public int Mileage { get; set; }
            public decimal VehicleValue { get; set; }
            public bool AutomaticallyReValueVehicle { get; set; }
            public string FinanceAgreementName { get; set; }
            public decimal NewFinanceBalance { get; set; }
            public string Reference { get; set; }
            public string Notes { get; set; }
            public bool VehicleValueChanged { get; set; }
            public bool FinanceBalanceChanged { get; set; }
            public string VehiclePhoto { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Registration).NotEmpty();
                RuleFor(x => x.VehicleName).NotEmpty();
                RuleFor(x => x.Mileage).NotEmpty();
                RuleFor(x => x.VehicleValue).NotEmpty();
                RuleFor(x => x.VehicleId).NotEmpty().NotNull();
            }
        }
    }

    public class UpdateVehicleCommandProfile : Profile
    {
        public UpdateVehicleCommandProfile()
        {
            CreateMap<UpdateVehicle.Command, Vehicle>()
                .ForMember(x => x.AutomaticallyReValueVehicle, x => x.MapFrom(s => s.AutomaticallyReValueVehicle))
                .ForMember(x => x.Registration, x => x.MapFrom(s => s.Registration))
                .ForMember(x => x.Mileage, x => x.MapFrom(s => s.Mileage))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.VehicleName))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(s => s.VehiclePhoto));

            CreateMap<UpdateVehicle.Command, Account>()
                .ForMember(x => x.Name, x => x.MapFrom(s => s.FinanceAgreementName))
                .ForMember(x => x.ReferenceNumber, x => x.MapFrom(s => s.Reference))
                .ForMember(x => x.Notes, x => x.MapFrom(s => s.Notes));
        }
    }
}