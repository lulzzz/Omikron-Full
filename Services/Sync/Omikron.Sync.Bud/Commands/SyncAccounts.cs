using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Messaging;

namespace Omikron.Sync.Bud.Commands
{
    public class SyncAccounts
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Command(Guid userId, IEnumerable<BudListAccountsResponse> budAccounts, IEnumerable<BudListCustomerConsentsResponse> customerConsents)
            {
                UserId = userId;
                BudAccounts = budAccounts;
                CustomerConsents = customerConsents;
            }

            public Guid UserId { get; set; }
            public IEnumerable<BudListAccountsResponse> BudAccounts { get; set; }
            public IEnumerable<BudListCustomerConsentsResponse> CustomerConsents { get; set; }
        }
    }
}