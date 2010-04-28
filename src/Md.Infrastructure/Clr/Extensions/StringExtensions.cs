using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.Clr.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullAnd(this string s, Func<string, bool> f)
        {
            return s != null && f(s);
        }

        public static string With(this string s, params object[] args)
        {
            return string.Format(s, args);

        }

        public static int TryParse(this string u, int defaultValue)
        {
            try
            {
                return int.Parse(u);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int TryParse(this string u)
        {
            return TryParse(u, 0);
        }

        /// <summary>
        /// Like null coalescing operator (??) but including empty strings
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string a, string b)
        {
            return string.IsNullOrEmpty(a) ? b : a;
        }

        /// <summary>
        /// If <paramref name="a"/> is empty, returns null
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string EmptyToNull(this string a)
        {
            return string.IsNullOrEmpty(a) ? null : a;
        }
    }
}
