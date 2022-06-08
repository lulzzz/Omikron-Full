using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;
using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.VaultService.Domain.Commands
{
    public class UpdateManualAccount
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Guid AccountId { get; set; }
            public decimal Balance { get; set; }
            public bool AccountBalanceChanged { get; set; }
            public int Type { get; set; }
            public string Name { get; set; }
            public string Notes { get; set; }
            public string ReferenceNumber { get; set; }
        }
    }

    public class UpdateManualAccountProfile : Profile
    {
        public UpdateManualAccountProfile()
        {
            CreateMap<UpdateManualAccount.Command, Account>()
                .ForMember(x => x.Type, x => x.MapFrom(s => AccountType.Parse(s.Type)))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.Name));
        }
    }
}