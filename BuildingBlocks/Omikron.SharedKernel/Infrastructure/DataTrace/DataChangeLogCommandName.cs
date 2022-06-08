using System;
using System.Collections.Generic;
using System.Linq;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLogCommandName : ValueObject<DataChangeLogCommandName>
    {
        private readonly string _commandName;

        private DataChangeLogCommandName(string commandName)
        {
            _commandName = commandName;
        }

        public static DataChangeLogCommandName Parse(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName))
            {
                throw new ArgumentException("The command name cannot be null or empty.");
            }

            commandName = ExtractCommandName(commandName);
            return new DataChangeLogCommandName(commandName);
        }

        private static string ExtractCommandName(string commandName)
        {
            const string delimiter = "+Command";
            var segments = commandName.Split(delimiter);
            return segments?.FirstOrDefault()?.Split(".")?.LastOrDefault() ?? commandName;
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object>() { _commandName };

        public override string ToString()
        {
            return _commandName;
        }
    }
}