using System;

namespace Omikron.SharedKernel.Utils
{
    public static class Clock
    {
        public static Func<DateTime> GetTime { get; } = () => DateTime.UtcNow;
    }
}