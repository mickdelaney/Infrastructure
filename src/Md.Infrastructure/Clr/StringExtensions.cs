using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.Clr
{
    public static class StringUtil
    {
        public static string AddTrailingCommaOrNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return string.Format("{0}, ", s);
        }

        public static string EmptyStringIfNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return s;
        }
    }
}
