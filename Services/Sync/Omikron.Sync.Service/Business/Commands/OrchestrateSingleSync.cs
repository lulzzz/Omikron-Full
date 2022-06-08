using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.Sync.Service.Business.Commands
{
	public class OrchestrateSingleSync
    {
        public class Command : BaseCommand.Action
		{
			public string Username { get; set; }

			public Command(string username)
			{
				Username = username;
			}
		}
	}
}
