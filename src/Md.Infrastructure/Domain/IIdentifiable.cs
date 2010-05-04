using System;

namespace Md.Infrastructure.Domain
{
    public interface IIdentifiable
    {
        Guid Identifier();
    }
}
