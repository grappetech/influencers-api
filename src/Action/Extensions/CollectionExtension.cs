using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System{
    public static class CollectionExtension
    {
        public static string StringJoin(this IEnumerable collection, string separator)
        {
            var result = collection.Cast<object>().Aggregate("", (current, x) => current + x.ToString() + separator);

            return result.Substring(0, result.Length - separator.Length);
        }
        
        public static string ToUrlParams(this Dictionary<string, string> source)
        {
            string param = "?";

            foreach (var item in source)
            {
                param += $"{item.Key}={item.Value}&";
            }

            return param.Substring(0, param.Length - 1);
        }
    }
}