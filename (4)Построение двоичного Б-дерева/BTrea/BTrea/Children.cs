using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    internal class Children<T> : IEnumerable<Node<T>> where T : class
    {
        private readonly List<Node<T>> _children = new List<Node<T>>();

        public int Length => _children.Count;
        public int Capacity => _children.Capacity;
       
        public void InsertAt(int index, Node<T> item)
        {
            _children.Insert(index, item);
        }
      
        public Node<T> RemoveAt(int index)
        {
            Node<T> n = _children[index];
            _children.RemoveAt(index);
            return n;
        }
      
        public Node<T> Pop()
        {
            int index = _children.Count - 1;
            Node<T> child = _children[index];
            _children[index] = null;
            _children.RemoveAt(index);
            return child;
        }
     
        public void Truncate(int index)
        {
            int count = _children.Count - index;
            if (count > 0)
            {
                _children.RemoveRange(index, count);
            }
        }

        public Node<T> this[int i]
        {
            get => _children[i];
            set => _children[i] = value;
        }
        public void Append(Node<T> node)
        {
            _children.Add(node);
        }

        public void Append(IEnumerable<Node<T>> range)
        {
            _children.AddRange(range);
        }

        public List<Node<T>> GetRange(int index, int count)
        {
            return _children.GetRange(index, count);
        }

        IEnumerator<Node<T>> IEnumerable<Node<T>>.GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }
    }
    internal enum ToRemove
    {
        // removes the given item        
        RemoveItem,

        // removes smallest item in the subtree        
        RemoveMin,

        // removes largest item in the subtree        
        RemoveMax
    }

    internal enum Direction
    {
        Descend = -1,
        Ascend = 1
    }
}
