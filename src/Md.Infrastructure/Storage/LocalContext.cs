using System;
using System.Collections;
using System.Web;
using Md.Infrastructure.Context;

namespace Md.Infrastructure
{
    /// <summary>
    /// Supports the storage of context information through the life of a thread
    /// </summary>
    public static class LocalContext
    {
        private static readonly ILocalContext _current = new LocalContextStorage();
        private static readonly object _localContextHashtableKey = new object();

        /// <summary>
        /// Gets the current data
        /// </summary>
        /// <value>The data.</value>
        public static ILocalContext Current
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets a value indicating whether running in the web context
        /// </summary>
        /// <value><c>true</c> if [running in web]; otherwise, <c>false</c>.</value>
        public static bool RunningInWeb
        {
            get { return HttpContext.Current != null; }
        }

        private class LocalContextStorage : ILocalContext
        {
            [ThreadStatic]
            private static Hashtable _threadLocalStorage;

            private static Hashtable ThreadLocalHashtable
            {
                get
                {
                    if (!RunningInWeb)
                    {
                        return _threadLocalStorage ?? (_threadLocalStorage = new Hashtable());
                    }

                    var webHashtable = HttpContext.Current.Items[_localContextHashtableKey] as Hashtable;
                    if (webHashtable == null)
                    {
                        HttpContext.Current.Items[_localContextHashtableKey] = webHashtable = new Hashtable();
                    }
                    return webHashtable;
                }
            }

            public object this[object key]
            {
                get { return ThreadLocalHashtable[key]; }
                set { ThreadLocalHashtable[key] = value; }
            }

            public TValue Retrieve<TValue>()
            {
                return ThreadLocalHashtable.Retrieve<TValue>();
            }

            public TValue Retrieve<TValue>(object key)
            {
                object existing = ThreadLocalHashtable[key];

                TValue value = (TValue)existing;

                return value;
            }

            public TValue Retrieve<TValue>(object key, Func<TValue> valueProvider)
            {
                object existing = ThreadLocalHashtable[key];

                TValue value;
                if (existing == null)
                {
                    value = valueProvider();
                    ThreadLocalHashtable[key] = value;
                }
                else
                {
                    value = (TValue)existing;
                }

                return value;
            }

            public void Remove(object key)
            {
                if (ThreadLocalHashtable.ContainsKey(key))
                    ThreadLocalHashtable.Remove(key);
            }

            public bool Contains(object key)
            {
                return ThreadLocalHashtable.ContainsKey(key);
            }

            public void Store<T>(object key, T value)
            {
                ThreadLocalHashtable[key] = value;
            }

            public void Store<T>(T value)
            {
                ThreadLocalHashtable.Store(value);
            }

            public void Clear()
            {
                ThreadLocalHashtable.Clear();
            }
        }
    }
}
