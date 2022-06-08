using System.Collections;
using System.Reflection;

namespace Omikron.SharedKernel.Extensions
{
    public static class IEnumerableExtensions
    {
        public static PropertyInfo[] GetEnumerableProperties(this IEnumerable data)
        {
            if (data != null)
            {
                var enumerator = data.GetEnumerator();
                enumerator.MoveNext();

                if (enumerator.Current != null)
                {
                    var type = enumerator.Current.GetType();
                    return type.GetProperties();
                }
            }

            return default;
        }
    }
}