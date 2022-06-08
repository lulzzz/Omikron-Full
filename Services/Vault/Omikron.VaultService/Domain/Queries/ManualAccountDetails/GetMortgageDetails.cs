using System;

namespace Omikron.VaultService.Domain.Queries.ManualAccountDetails
{
    public class GetMortgageDetails
    {
        public class Query : ManualAccountDetailsBaseQuery
        {
            public Guid FinanceId { get; set; }
        }
    }
}