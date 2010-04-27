using System;
using Md.Infrastructure.Domain;
using Md.Infrastructure.Security;

namespace Md.Infrastructure.Audit
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class AuditedEntity<T> : EntityBase<T>, IHaveAuditInformation where T : EntityBase<T>
    {
        public virtual DateTime UpdatedAt { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
    }
}
