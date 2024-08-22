using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    public class FreeList<T> where T : class
    {
        private const int DefaultFreeListSize = 32;

        private readonly object _mu;
        private readonly List<Node<T>> _freelist;
        private readonly Comparer<T> _comparer;

        public FreeList(Comparer<T> comparer)
            : this(DefaultFreeListSize, comparer)
        { }

        public FreeList(int size, Comparer<T> comparer)
        {
            _mu = new object();
            _freelist = new List<Node<T>>(size);
            _comparer = comparer;
        }

        internal Node<T> NewNode()
        {
            lock (_mu)
            {
                int index = _freelist.Count - 1;

                if (index < 0)
                {
                    return new Node<T>(_comparer);
                }

                Node<T> n = _freelist[index];

                _freelist[index] = null;
                _freelist.RemoveAt(index);

                return n;
            }
        }
         
        internal bool FreeNode(Node<T> n)
        {
            bool success = false;

            lock (_mu)
            {
                if (_freelist.Count < _freelist.Capacity)
                {
                    _freelist.Add(n);
                    success = true;
                }
            }

            return success;
        }
    }
}
