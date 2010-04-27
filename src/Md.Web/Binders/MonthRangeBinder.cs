using System.Web.Mvc;
using Md.Infrastructure.Clr;

namespace Md.Web.Binders
{
    public class MonthRangeBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var startMonth = GetValue<Month>(bindingContext, "StartMonth").Value;
            var startYear = GetValue<int>(bindingContext, "StartYear").Value;
            var endMonth = GetValue<Month>(bindingContext, "EndMonth").Value;
            var endYear = GetValue<int>(bindingContext, "EndYear").Value;

            return new MonthRange(startMonth, startYear, endMonth, endYear);
        }

        private static T? GetValue<T>(ModelBindingContext bindingContext, string key) where T : struct
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);

            if (result == null && bindingContext.FallbackToEmptyPrefix)
            {
                result = bindingContext.ValueProvider.GetValue(key);
            }

            if (result == null)
            {
                return null;
            }

            return (T?)result.ConvertTo(typeof(T));
        }
    }
}