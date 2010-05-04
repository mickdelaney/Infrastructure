using System.Collections.Generic;
using System.Linq;

namespace Md.Infrastructure.Domain
{
    public interface INamedEntity
    {
        string Name { get; set; }
    }

    public static class NamedEntityQueries
    {
        public static IEnumerable<T> FindByName<T>(this IQueryable<T> query, string name) where T : INamedEntity
        {
            return query.Where(a => a.Name == name)
                        .ToList();
        }
    }
}
