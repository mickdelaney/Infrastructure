﻿using System.Collections.Generic;

namespace Md.Web.Search
{
    public class SearchParameters
    {
        public const int DefaultPageSize = 5;

        public SearchParameters()
        {
            Facets = new Dictionary<string, string>();
            PageSize = DefaultPageSize;
            PageIndex = 1;
        }

        public string FreeSearch { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IDictionary<string, string> Facets { get; set; }
        public string Sort { get; set; }

        public int FirstItemIndex
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }

        public int LastItemIndex
        {
            get
            {
                return FirstItemIndex + PageSize;
            }
        }
    }
}
