using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Md.Infrastructure.Domain;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Expression=System.Linq.Expressions.Expression;

namespace Md.Infrastructure.Data.NHibernate
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase<T>
    {
        private readonly ISession _session;
        protected ISession Session { get { return _session; } }
        protected INHibernateQueryable<T> Query { get { return Session.Linq<T>(); } }

        protected RepositoryBase(ISession session)
        {
            _session = session;
        }

        public Type ElementType
        {
            get { return Query.ElementType; }
        }
        public Expression Expression
        {
            get { return Query.Expression; }
        }
        public IQueryProvider Provider
        {
            get { return Query.Provider; }
        }

        public IQueryable<T> Expand(string path)
        {
            return Query.Expand(path);
        }

        public QueryOptions QueryOptions
        {
            get { return Query.QueryOptions; }
        }

        public T Save(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Query.GetEnumerator();
        }

        public List<T> SelectList(Func<T, bool> query)
        {
            return Query.Where(query).ToList();
        }

        public T SelectFirst(Func<T, bool> query)
        {
            return Query.Where(query).FirstOrDefault();
        }

        public virtual T GetById(Guid id)
        {
            return Session.Get<T>(id);
        }

        public virtual ICollection<T> FindAll(DetachedCriteria criteria, int firstResult, int maxResults, params Order[] orders)
        {
            ICriteria crit = RepositoryHelper<T>.GetExecutableCriteria(Session, criteria, orders);
            crit.SetFirstResult(firstResult)
                .SetMaxResults(maxResults);
            return crit.List<T>();
        }
        public virtual ICollection<T> FindAll(DetachedCriteria criteria, params Order[] orders)
        {
            ICriteria crit = RepositoryHelper<T>.GetExecutableCriteria(Session, criteria, orders);
            return crit.List<T>();
        }
        public virtual ICollection<T> FindAll()
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        public virtual void Add(T product)
        {
            Session.Save(product);
        }
        public virtual void Remove(T product)
        {
            Session.Delete(product);
        }

        public virtual T Merge(T entity)
        {
            Session.Merge(entity);
            return entity;
        }
        public virtual T SaveOrUpdateCopy(T entity)
        {
            Session.SaveOrUpdateCopy(entity);
            return entity;
        }
        public virtual T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public virtual void DeleteAll(DetachedCriteria where)
        {
            foreach (object entity in where.GetExecutableCriteria(Session).List())
            {
                Session.Delete(entity);
            }
        }
    }
}
