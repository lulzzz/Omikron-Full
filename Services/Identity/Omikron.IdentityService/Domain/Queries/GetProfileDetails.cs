using Omikron.IdentityService.ViewModel;
using System;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetProfileDetails
    {
        public class Query : GetUserAccountBy.Query<Guid, ProfileDetailsViewModel>
        {
            public Query(Guid userId) : base(userId)
            {
            }
        }
    }
}
