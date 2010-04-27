using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.CQRS
{
    public interface IHandler<TEvent>
    {
        void Handle(TEvent args);
    }
}
