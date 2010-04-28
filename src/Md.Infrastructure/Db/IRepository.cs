using System.Linq;
using Md.Infrastructure.Domain;

namespace Md.Infrastructure.Db
{
    public interface IRepository<T> : IQueryable<T> where T : IIdentifiable
    {
        T Save(T entity);
    }
}
