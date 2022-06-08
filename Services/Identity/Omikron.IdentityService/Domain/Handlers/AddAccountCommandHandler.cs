using System;
using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class AddAccountCommandHandler : BaseHandler<AddUserAccount.Command, ApiResult>
    {
        public AddAccountCommandHandler(IDispatcher dispatcher): base(dispatcher)
        {
        }

        public override async Task<ApiResult> Handle(AddUserAccount.Command command, CancellationToken cancellationToken)
        {
            var response = await Dispatcher.DispatchAsync(command: new SetupUserAccountCommand<ApiResult<Guid>>(command.FirstName, command.LastName,
                command.Email, command.Roles), cancellationToken);

            return response;
        }
    }
}