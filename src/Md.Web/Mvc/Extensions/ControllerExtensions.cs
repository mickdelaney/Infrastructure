﻿using System;
using System.Web.Mvc;

namespace Md.Infrastructure.Mvc.Extensions
{
    ///<summary>
    /// Static class containing extension methods for controllers
    ///</summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Determines whether the specified type is a controller
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if type is a controller, otherwise false</returns>
        public static bool IsController(Type type)
        {
            return type != null
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !type.IsAbstract
                   && typeof (IController).IsAssignableFrom(type);
        }
    }
}