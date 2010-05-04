using System;
using System.Linq;
using Castle.MicroKernel;

namespace Md.Infrastructure.IoC.Windsor
{
    public class CloseGenericTypesSelector : IHandlerSelector
    {
        public bool HasOpinionAbout(string key, Type service)
        {
            // that's about as much as we can say at this point...
            return service.IsGenericType && service.GetGenericArguments().Length == 1;
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            return handlers.FirstOrDefault(h => MatchHandler(service, h));
        }

        private static bool MatchHandler(Type service, IHandler handler)
        {
            //Get the closing type required.
            var closingTypeRequired = handler.ComponentModel
                                             .Implementation.GetGenericArguments()
                                             .Single()
                                             .GetGenericParameterConstraints()
                                             .SingleOrDefault();

            if (closingTypeRequired == null)
                return false;

            var closingTypeActual = service.GetGenericArguments().Single();

            return closingTypeRequired.IsAssignableFrom(closingTypeActual);
        }
    }
}
