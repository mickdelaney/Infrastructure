using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.Domain
{
    public interface IIdentifiable
    {
        Guid Identifier();
    }
}
