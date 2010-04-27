using System;

namespace Md.Infrastructure
{
    public interface ILocalContext
    {
        object this[object key] { get; set; }

        void Clear();

        TValue Retrieve<TValue>();
        TValue Retrieve<TValue>(object key);
        TValue Retrieve<TValue>(object key, Func<TValue> valueProvider);

        void Remove(object key);

        bool Contains(object key);
        void Store<T>(object key, T value);


        /// <summary>
        /// Stores the value using a <see cref="TypedKey{T}"/> for the key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        void Store<T>(T value);

    }
}
