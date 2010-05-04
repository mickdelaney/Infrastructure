using System.Web.Mvc;
using Md.Infrastructure.Mvc;

namespace Md.Infrastructure.Mvc.Extensions
{
    public static class TempDataExtensions
    {
        public static void AddNotification(this TempDataDictionary dictionary, string message)
        {
            dictionary[Messages.Notification] = new TempDataMessage { Message = message };
        }
        public static void AddError(this TempDataDictionary dictionary, string formatMessage, params object[] tokens)
        {
            AddError(dictionary, string.Format(formatMessage, tokens));
        }
        public static void AddError(this TempDataDictionary dictionary, string message)
        {
            dictionary[Messages.Error] = new TempDataMessage { Message = message };
        }
        public static void AddSuccess(this TempDataDictionary dictionary, string message)
        {
            dictionary[Messages.Success] = new TempDataMessage { Message = message };
        }
        public static void AddAttention(this TempDataDictionary dictionary, string message)
        {
            dictionary[Messages.Attention] = new TempDataMessage { Message = message };
        }

    }
}
