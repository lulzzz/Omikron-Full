using AutoMapper;
using FluentValidation;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.IdentityService.Domain.Commands
{
    public class EditProfileDetails
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Guid UserId { get; set; }
            public string Nickname { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int VerificationToken { get; set; }
        }

        public class EditProfileDetailsUserProfile : Profile
        {
            public EditProfileDetailsUserProfile()
            {
                CreateMap<Command, User>()
                    .ForMember(x => x.PhoneNumber, opt => opt.Ignore())
                    .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));
            }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Nickname).NotEmpty().WithMessage("Nickname should not be empty.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.UserId).NotEmpty();
                RuleFor(x => x.PhoneNumber).Matches(@"^\+(?:[0-9]●?){6,14}[0-9]$").WithMessage("Invalid phone number. Valid example: +447911123456");
                RuleFor(x => x.VerificationToken).GreaterThan(Constants.VerificationTokenLowerBound).LessThan(Constants.VerificationTokenUpperBound).WithMessage("Invalid verification token");
            }
        }
    }
}
