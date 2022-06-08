using System;

namespace Omikron.VaultService.Domain.Queries.ManualAccountDetails
{
    public class GetPersonalItemFinanceDetails
    {
        public class Query : ManualAccountDetailsBaseQuery
        {
            public Guid FinanceId { get; set; }
        }
    }
}