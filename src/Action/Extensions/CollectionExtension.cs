using System.Collections;
using System.Linq;

namespace System{
    public static class CollectionExtension
    {
        public static string StringJoin(this IEnumerable collection, string separator)
        {
            var result = collection.Cast<object>().Aggregate("", (current, x) => current + x.ToString() + separator);

            return result.Substring(0, result.Length - separator.Length);
        }
    }
}