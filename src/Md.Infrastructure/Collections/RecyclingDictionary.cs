using System.Collections.Generic;
using System.Linq;

namespace Md.Infrastructure.Collections
{
    public class RecyclingDictionary<T>
    {
        private readonly int _maximumSize = 1000;
        private readonly int _recycleSize = 100;
        private Dictionary<string, T> _items = new Dictionary<string, T>();

        public RecyclingDictionary()
        {
        }

        public RecyclingDictionary(int maximumSize, int recycleSize)
        {
            _maximumSize = maximumSize;
            _recycleSize = recycleSize;
        }

        public T this[string key]
        {
            get { return _items[key]; }
            set
            {
                if (_items.Count >= _maximumSize)
                    Recycle();
                _items[key] = value;
            }
        }

        public void Add(string key, T item)
        {
            if (_items.Count >= _maximumSize)
                Recycle();
            _items.Add(key, item);
        }

        public void Remove(string key)
        {
            _items.Remove(key);
        }

        public int Count()
        {
            return _items.Count;
        }

        private void Recycle()
        {
            IEnumerable<KeyValuePair<string, T>> skipList = _items.Skip(_recycleSize);
            _items = skipList.ToDictionary(x => x.Key, x => x.Value);
        }

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }
    }
}