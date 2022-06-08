using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using Omikron.SharedKernel.Utils;
using System;
using static Omikron.VaultService.Domain.Commands.AddPersonalItem;

namespace Omikron.VaultService.Domain.Commands
{
    public class AddVehicle
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string VehicleName { get; set; }
            public string Registration { get; set; }
            public int Mileage { get; set; }
            public decimal VehicleValue { get; set; }
            public bool AutomaticallyReValueVehicle { get; set; }
            public Guid UserId { get; set; }
            public FinanceAgreement FinanceAgreement { get; set; }
            public Guid? ExistingFinanceAgreementId { get; set; }
            public string VehiclePhoto { get; set; }
            public decimal? PurchaseValue { get; set; }
            public DateTime? PurchaseDate { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Registration).NotEmpty().WithMessage("Registration must not be empty");
                RuleFor(x => x.VehicleName).NotEmpty().WithMessage("Vehicle name must not be empty!");
                RuleFor(x => x.Mileage).NotEmpty().WithMessage("Mileage must not be empty!");
                RuleFor(x => x.VehicleValue).NotEmpty().GreaterThan(0).WithMessage("Vehicle value must not be empty!");
                RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required!");
                RuleFor(x => x.ExistingFinanceAgreementId).Null().When(x => x.FinanceAgreement.IsNotNull());
                RuleFor(x => x.FinanceAgreement).Null().When(x => x.ExistingFinanceAgreementId.HasValue);
                RuleFor(x => x.FinanceAgreement.Name).NotEmpty().When(x => x.FinanceAgreement != null);
                RuleFor(x => x.PurchaseDate).NotEmpty().When(x => x.PurchaseValue.HasValue).WithMessage("Please enter purchase date alongside purchase value.");
                RuleFor(x => x.PurchaseDate).LessThanOrEqualTo(Clock.GetTime()).WithMessage("Please enter purchase date less than or equal to the current date.");
                RuleFor(x => x.PurchaseValue).NotEmpty().When(x => x.PurchaseDate.HasValue).WithMessage("Please enter purchase value alongside purchase date.");
                RuleFor(x => x.PurchaseValue).GreaterThan(0).WithMessage("Please enter purchase value greater than zero.");
                RuleFor(x => x.FinanceAgreement.OpenDate).LessThanOrEqualTo(Clock.GetTime()).When(x => x.FinanceAgreement != null).WithMessage("Please enter finance agreement open date less than or equal to the current date.");
                RuleFor(x => x.FinanceAgreement.OpenDate).NotEmpty().When(x => x.FinanceAgreement != null && x.FinanceAgreement.OpenBalance.HasValue).WithMessage("Please enter finance agreement open date alongside open balance.");
                RuleFor(x => x.FinanceAgreement.OpenBalance).NotEmpty().When(x => x.FinanceAgreement != null && x.FinanceAgreement.OpenDate.HasValue).WithMessage("Please enter finance agreement open balance alongside open date.");
            }
        }
    }
}