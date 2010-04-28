﻿using System.Collections.Generic;
using System.Linq;
using SolrNet;
using SolrNet.DSL;

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

        /// <summary>
        /// Builds the Solr query from the search parameters
        /// </summary>
        /// <returns></returns>
        public ISolrQuery SolrQuery()
        {
            if (!string.IsNullOrEmpty(FreeSearch))
                return new SolrQuery(FreeSearch);
            return SolrNet.SolrQuery.All;
        }

        public ICollection<ISolrQuery> FilterQueries()
        {
            var queriesFromFacets = from p in Facets
                                    select (ISolrQuery)Query.Field(p.Key).Is(p.Value);
            return queriesFromFacets.ToList();
        }

        /// <summary>
        /// Gets the selected facet fields
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> SelectedFacetFields()
        {
            return Facets.Select(f => f.Key);
        }

        public SortOrder[] GetSelectedSort()
        {
            return new[] { SortOrder.Parse(Sort) }.Where(o => o != null).ToArray();
        }
    }
}
