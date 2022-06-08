using System;
using Omikron.IdentityService.ViewModel;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetUserAccountByUserAccountId
    {
        public class Query : GetUserAccountBy.Query<Guid, UserAccountViewModel>
        {
            public Query(Guid userId) : base(userId)
            {
            }
        }
    }
}