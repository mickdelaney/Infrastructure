using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using SolrNet;

namespace Md.Web.Solr.Extensions
{
    public static class HtmlHelperMapperExtensions
    {
        private static IReadOnlyMappingManager Mapper
        {
            get { return ServiceLocator.Current.GetInstance<IReadOnlyMappingManager>(); }
        }

        public static string SolrFieldPropName<T>(this HtmlHelper helper, string fieldName)
        {
            return Mapper.GetFields(typeof(T)).First(p => p.FieldName == fieldName).Property.Name;
        }
    }
}
