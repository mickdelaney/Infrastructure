using System;

namespace Md.Infrastructure.Security
{
    public interface IHaveAuditInformation
    {
        DateTime UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}