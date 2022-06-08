using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud.Events
{
    public class BudTransactionsRetrievedEvent
    {
        public BudTransactionsRetrievedEvent(Guid userId, IEnumerable<BudListTransactionsResponse> budTransactions)
        {
            UserId = userId;
            BudTransactions = budTransactions;
        }

        public Guid UserId { get; set; }
        public IEnumerable<BudListTransactionsResponse> BudTransactions { get; set; }
    }
}