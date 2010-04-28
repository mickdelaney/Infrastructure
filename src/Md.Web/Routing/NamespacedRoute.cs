using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Md.Web.Routing
{
    public class NamespacedRoute : Route
    {
        private readonly string _areaName;

        public NamespacedRoute(string areaName, string url)
            : base(url, new MvcRouteHandler())
        {
            _areaName = areaName;
            Constraints = new RouteValueDictionary();
            Defaults = new RouteValueDictionary();
        }

        public NamespacedRoute(string url)
            : base(url, new MvcRouteHandler())
        {
            Constraints = new RouteValueDictionary();
            Defaults = new RouteValueDictionary();
        }

        public static NamespacedRoute Mapping(string profileName, string url)
        {
            return new NamespacedRoute(profileName, url);
        }
        public static NamespacedRoute Mapping(string url)
        {
            return new NamespacedRoute(url);
        }

        public NamespacedRoute ToActionOn<T>(Expression<Func<T, ActionResult>> action) where T : IController
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
                throw new ArgumentException("Expression must be a method call");

            if (body.Object != action.Parameters[0])
                throw new ArgumentException("Method call must target lambda argument");

            var actionName = body.Method.Name;
            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            var controllerType = typeof(T);

            Defaults = CreateRouteDefaults(controllerType, actionName);
            DataTokens = CreateNamespaceTokens(controllerType);

            return this;
        }

        private RouteValueDictionary CreateNamespaceTokens(Type controllerType)
        {
            if (string.IsNullOrEmpty(_areaName))
            {
                return new RouteValueDictionary
                {
                    { "namespaces", new[] { controllerType.Namespace } }
                };
            }

            return new RouteValueDictionary
            {
                { "namespaces", new[] { controllerType.Namespace } },
                { "area", _areaName},
                { "UseNamespaceFallback", false }
            };
        }

        private RouteValueDictionary CreateRouteDefaults(Type controllerType, string actionName)
        {
            string controllerName = controllerType.Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);

            var defaults = new RouteValueDictionary();

            foreach (var pair in Defaults.Where(x => x.Value == null).ToList())
                Defaults.Remove(pair.Key);

            defaults.Add("controller", controllerName);

            if (!string.IsNullOrEmpty(actionName))
                defaults.Add("action", actionName);

            return defaults;
        }

        public NamespacedRoute To<T>()
        {
            var controllerType = typeof(T);

            Defaults = CreateRouteDefaults(controllerType, null);
            DataTokens = CreateNamespaceTokens(controllerType);

            return this;
        }

        public NamespacedRoute WithNamespaceOf<T>()
        {
            var controllerType = typeof(T);

            DataTokens = CreateNamespaceTokens(controllerType);

            return this;
        }
    }
}
