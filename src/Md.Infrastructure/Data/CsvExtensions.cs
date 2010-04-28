using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Md.Infrastructure.Clr.Extensions;
using Md.Infrastructure.Data.Extensions;

namespace Md.Infrastructure.Data
{
    public static class CsvExtensions
    {
        public static string AsCsv<T>(this IEnumerable<T> items) where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            foreach (T item in items)
            {
                var line = properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray().Join(",");
                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item is string)
            {
                return "\"{0}\"".With(item.ToString().Replace("\"", "\\\""));
            }
            if (item is DateTime)
            {
                return "{0:u}".With(item);
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return "{0}".With(item);
            }
            return "\"{0}\"".With(item);
        }


    }
}
