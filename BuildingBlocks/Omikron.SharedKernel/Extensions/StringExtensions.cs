using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Omikron.SharedKernel.Extensions
{
    public static class StringExtensions
    {
        public static string ToDashCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString())).Replace(".", string.Empty).ToLower();
        }

        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string HidePartOfThePhoneNumber(this string number)
        {
            if (!Regex.IsMatch(number, @"^\+(?:[0-9]●?){6,14}[0-9]$"))
            {
                throw new ArgumentException("Number is not in a valid format");
            }

            var visibleDigits = 3;
            var replacementString = string.Concat(Enumerable.Repeat("*", number.Length - (visibleDigits + 1))); // '+1' is because of the '+' sign
            var lastThreeDigits = number.Substring(number.Length - visibleDigits);

            return $"+ {replacementString} {lastThreeDigits}";
        }
    }
}