using System.Linq;
using Md.Infrastructure.Domain;

namespace Md.Infrastructure.Data
{
    public interface IRepository<T> : IQueryable<T> where T : EntityBase<T>
    {
        T Save(T entity);
    }
}
