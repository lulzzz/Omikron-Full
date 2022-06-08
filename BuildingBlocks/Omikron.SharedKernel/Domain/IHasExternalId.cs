using System;

namespace Omikron.SharedKernel.Domain
{
    public interface IHasExternalId
    {
        Guid ExternalId { get; set; }
    }
}