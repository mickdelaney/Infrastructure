using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.Data.Extensions
{
    public static class EnumerableExtensions
    {
        public static IDictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> list)
        {
            return list.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static string Join<T>(this IEnumerable<T> collection, string token)
        {
            if (collection == null || collection.Count() == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (var element in collection)
            {
                builder.Append(string.Format("{0}{1}", element, token));
            }
            return builder.ToString();
        }

        public static T RandomFrom<T>(this IList<T> collection)
        {
            return collection[new Generate().RandomNumberBetween(0, collection.Count)];
        }
    }
}
