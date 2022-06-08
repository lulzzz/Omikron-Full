using AutoMapper;
using FluentValidation;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.IdentityService.Domain.Commands
{
    public class PerformKyc
    {
        public class Command : TenantCommand<ApiResult>
        {
            public UserTitle? Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Postcode { get; set; }
            public string Address { get; set; }
            public string UserName { get; set; }
			public bool IsManualAddress { get; set; }
		}

        public class PerformKycUserProfile : Profile
        {
            public PerformKycUserProfile()
            {
                CreateMap<Command, User>()
                    .ForMember(x => x.UserName, opt => opt.Ignore());
            }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
                RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThan(new DateTime(1900, 01, 01)).WithMessage("Year must be greater than 1900");
                RuleFor(x => x.Postcode).NotEmpty().WithMessage("Postcode is required.");
                RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            }
        }
    }
}