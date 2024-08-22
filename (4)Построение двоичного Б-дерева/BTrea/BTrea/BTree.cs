using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    public class BTree<T> where T : class
    {
        private int Degree { get; set; }

        public int Length { get; private set; }

        private Node<T> Root { get; set; }
        private CopyOnWriteContext<T> Cow { get; set; }

        private BTree()
        { }

        public BTree(int degree, Comparer<T> comparer)
            : this(degree, new FreeList<T>(comparer))
        { }

        public BTree(int degree, FreeList<T> f)
        {
            if (degree <= 1)
            {
                Environment.FailFast("bad degree");
            }
            Degree = degree;
            Cow = new CopyOnWriteContext<T> { FreeList = f };
        }
   
        public BTree<T> Clone()
        {
            CopyOnWriteContext<T> cow1 = new CopyOnWriteContext<T> { FreeList = Cow.FreeList };
            CopyOnWriteContext<T> cow2 = new CopyOnWriteContext<T> { FreeList = Cow.FreeList };
            BTree<T> tree = new BTree<T>
            {
                Degree = Degree,
                Length = Length,
                Root = Root,
                Cow = Cow
            };
            Cow = cow1;
            tree.Cow = cow2;
            return tree;
        }
       
        private int MaxItems()
        {
            return (Degree * 2) - 1;
        }
     
        private int MinItems()
        {
            return Degree - 1;
        }

        public T ReplaceOrInsert(T item)
        {
            if (item == null)
            {
                Environment.FailFast("null item being added to BTree");
            }
            if (Root == null)
            {
                Root = Cow.NewNode();
                Root.Items.Append(item);
                Length++;
                return null;
            }
            else
            {
                Root = Root.MutableFor(Cow);
                if (Root.Items.Length >= MaxItems())
                {
                    (T item2, Node<T> second) = Root.Split(MaxItems() / 2);
                    Node<T> oldRoot = Root;
                    Root = Cow.NewNode();
                    Root.Items.Append(item2);
                    Root.Children.Append(oldRoot);
                    Root.Children.Append(second);
                }
            }
            T result = Root.Insert(item, MaxItems());
            if (result == null)
            {
                Length++;
            }
            return result;
        }

        public T Delete(T item)
        {
            return DeleteItem(item, ToRemove.RemoveItem);
        }
      
        public T DeleteMin()
        {
            return DeleteItem(null, ToRemove.RemoveMin);
        }
    
        public T DeleteMax()
        {
            return DeleteItem(null, ToRemove.RemoveMax);
        }

        private T DeleteItem(T item, ToRemove typ)
        {
            if (Root == null || Root.Items.Length == 0)
            {
                return null;
            }
            Root = Root.MutableFor(Cow);
            T result = Root.Remove(item, MinItems(), typ);
            if (Root.Items.Length == 0 && Root.Children.Length > 0)
            {
                Node<T> oldRoot = Root;
                Root = Root.Children[0];
                _ = Cow.FreeNode(oldRoot);
            }
            if (result != null)
            {
                Length--;
            }
            return result;
        }

        public void AscendRange(T greaterOrEqual, T lessThan, ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Ascend, greaterOrEqual, lessThan, true, false, iterator);
        }

        public void AscendLessThan(T pivot, ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Ascend, null, pivot, false, false, iterator);
        }

        public void AscendGreaterOrEqual(T pivot, ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Ascend, pivot, null, true, false, iterator);
        }

        public void Ascend(ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Ascend, null, null, false, false, iterator);
        }
        public void DescendRange(T lessOrEqual, T greaterThan, ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Descend, lessOrEqual, greaterThan, true, false, iterator);
        }

        public void DescendLessOrEqual(T pivot, ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Descend, pivot, null, true, false, iterator);
        }
        public void DescendGreaterThan(T pivot, ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Descend, null, pivot, false, false, iterator);
        }

        public void Descend(ItemIterator<T> iterator)
        {
            if (Root == null)
            {
                return;
            }
            _ = Root.Iterate(Direction.Descend, null, null, false, false, iterator);
        }

        public T Get(T key)
        {
            return Root?.Get(key);
        }
       
        public T Min()
        {
            return Node<T>.Min(Root);
        }
     
        public T Max()
        {
            return Node<T>.Max(Root);
        }      
        public bool Has(T key)
        {
            return Get(key) != null;
        }
        public void Clear(bool addNodesToFreeList)
        {
            if (Root != null && addNodesToFreeList)
            {
                _ = Root.Reset(Cow);
            }
            Root = null;
            Length = 0;
        }
    }

    internal enum FreeType
    {
        // node was freed (available for GC, not stored in freelist)        
        ftFreeListFull,

        // node was stored in the freelist for later use        
        ftStored,

        // node was ignored by COW, since it's owned by another one        
        ftNotOwned
    }
}
