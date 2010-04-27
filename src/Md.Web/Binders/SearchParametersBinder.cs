﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Md.Infrastructure.Clr;
using Md.Infrastructure.Data;

namespace Md.Web.Search
{
    public class SearchParametersBinder : IModelBinder
    {
        public const int DefaultPageSize = SearchParameters.DefaultPageSize;

        public IDictionary<string, string> NvToDict(NameValueCollection nv)
        {
            var d = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var k in nv.AllKeys)
                d[k] = nv[k];
            return d;
        }

        private static readonly Regex FacetRegex = new Regex("^f_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var qs = controllerContext.HttpContext.Request.QueryString;
            var qsDict = NvToDict(qs);
            var sp = new SearchParameters
            {
                FreeSearch = StringHelper.EmptyToNull(qs["q"]),
                PageIndex = StringHelper.TryParse(qs["page"], 1),
                PageSize = StringHelper.TryParse(qs["pageSize"], DefaultPageSize),
                Sort = StringHelper.EmptyToNull(qs["sort"]),
                Facets = qsDict.Where(k => FacetRegex.IsMatch(k.Key))
                    .Select(k => k.WithKey(FacetRegex.Replace(k.Key, "")))
                    .ToDictionary()
            };
            return sp;
        }
    }
}
