using System;
using System.Web.Mvc;

namespace Md.Web.Mvc.Extensions
{
    public static class SearchExtensions
    {
        private static Random _rnd = new Random();

        public static int RandomNumber(this HtmlHelper helper)
        {
            return _rnd.Next();
        }
    }
}
