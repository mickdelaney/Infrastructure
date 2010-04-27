using System.Collections;

namespace Md.Infrastructure.Context
{
    public class TypedKey<T>
    {
        public bool Equals(TypedKey<T> obj)
        {
            return !ReferenceEquals(null, obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TypedKey<T>)) return false;
            return Equals((TypedKey<T>)obj);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        public static string UniqueKey = typeof(TypedKey<T>).FullName;
    }

    public static class ExtensionsForTypedKey
    {
        public static void Store<T>(this IDictionary items, T value)
        {
            items[TypedKey<T>.UniqueKey] = value;
        }

        public static void Remove<T>(this IDictionary items)
        {
            if (items.Exists<T>())
                items.Remove(TypedKey<T>.UniqueKey);
        }

        public static bool Exists<T>(this IDictionary items)
        {
            return items.Contains(TypedKey<T>.UniqueKey);
        }

        public static T Retrieve<T>(this IDictionary items)
        {
            return (T)items[TypedKey<T>.UniqueKey];
        }
    }
}
