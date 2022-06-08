using System;

namespace Omikron.SharedKernel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoggerField : Attribute
    {
        public string Name { get; set; }
    }
}
