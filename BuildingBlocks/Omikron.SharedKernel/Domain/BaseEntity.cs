using System;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Domain
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = Clock.GetTime();
        public DateTime? ModifiedAt { get; set; } = Clock.GetTime();
    }
}