using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.VaultService.Domain.Commands
{
    public class DeleteUserRelatedData
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Guid UserId { get; set; }

            public Command(Guid userId)
            {
                UserId = userId;
            }
        }
    }
}
