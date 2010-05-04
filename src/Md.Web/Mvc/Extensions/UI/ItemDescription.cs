using System;

namespace Md.Web.Mvc.Extensions.UI
{
    [Flags]
    public enum ItemDescription
    {
        First = 1, 
        Last = 2, 
        Interior = 4, 
        Even = 8,
        Odd = 16,
    }
}