using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    public delegate bool ItemIterator<T>(T i) where T : class;
 
    internal class Items<T> : IEnumerable<T> where T : class
    {
        private readonly List<T> _items = new List<T>();
        private readonly Comparer<T> _comparer;

        public int Length => _items.Count;
        public int Capacity => _items.Capacity;

        public Items(Comparer<T> comparer)
        {
            _comparer = comparer;
        }
       
        public void InsertAt(int index, T item)
        {
            _items.Insert(index, item);
        }
      
        public T RemoveAt(int index)
        {
            T item = _items[index];
            _items.RemoveAt(index);
            return item;
        }
              
        public T Pop()
        {
            int index = _items.Count - 1;
            T item = _items[index];
            _items[index] = null;
            _items.RemoveAt(index);
            return item;
        }
     
        public void Truncate(int index)
        {
            int count = _items.Count - index;
            if (count > 0)
            {
                _items.RemoveRange(index, count);
            }
        }
       
        public (int, bool) Find(T item)
        {
            int index = _items.BinarySearch(0, _items.Count, item, _comparer);

            bool found = index >= 0;

            if (!found)
            {
                index = ~index;
            }

            return index > 0 && !Less(_items[index - 1], item) ? (index - 1, true) : (index, found);
        }

        public T this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }

        public void Append(T item)
        {
            _items.Add(item);
        }

        public void Append(IEnumerable<T> items)
        {
            _items.AddRange(items);
        }

        public List<T> GetRange(int index, int count)
        {
            return _items.GetRange(index, count);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(" ", _items);
        }

        private bool Less(T x, T y)
        {
            return _comparer.Compare(x, y) == -1;
        }
    }
}
