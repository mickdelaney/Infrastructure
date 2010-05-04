using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityBase<TEntity> : IIdentifiable where TEntity : EntityBase<TEntity>
    {
        public virtual Guid Id { get; set; }
        private int? _oldHashCode;

        public virtual Guid Identifier()
        {
            return Id;
        }
        
        /// <summary>
        /// /
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(EntityBase<TEntity> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(other.Id, Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it  
            if (_oldHashCode.HasValue)
            {
                return _oldHashCode.Value;
            }

            var thisIsTransient = Equals(Id, Guid.Empty);
            // When this instance is transient, we use the base GetHashCode()  
            // and remember it, so an instance can NEVER change its hash code.  
            if (thisIsTransient)
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }

            return Id.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as TEntity;
            if (other == null)
            {
                return false;
            }

            // handle the case of comparing two NEW objects  
            var otherIsTransient = Equals(other.Id, Guid.Empty);
            var thisIsTransient = Equals(Id, Guid.Empty);
            if (otherIsTransient && thisIsTransient)
            {
                return ReferenceEquals(other, this);
            }

            return other.Id.Equals(Id);
        }
    }

    public static class EntityBaseQueries
    {
        public static T GetByIdentifier<T>(this IQueryable<T> query, Guid id) where T : EntityBase<T>
        {
            return query.Where(a => a.Id == id)
                        .FirstOrDefault();
        }
    }

}
