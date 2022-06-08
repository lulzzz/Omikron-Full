using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class RefreshHistory : BaseEntity<Guid>
    {
        public RefreshHistory(Guid userId)
        {
            UserId = userId;
        }

        //TODO: This should be of type CustomerId
        public Guid UserId { get; set; }
    }
}
