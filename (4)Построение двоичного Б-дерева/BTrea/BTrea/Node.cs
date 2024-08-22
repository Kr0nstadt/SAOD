using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    internal class Node<T> where T : class
    {
        internal Items<T> Items { get; set; }
        internal Children<T> Children { get; set; }
        internal CopyOnWriteContext<T> Cow { get; set; }
        internal Comparer<T> Comparer { get; set; }

        public Node(Comparer<T> comparer)
        {
            Comparer = comparer;
            Items = new Items<T>(comparer);
            Children = new Children<T>();
        }

        public Node<T> MutableFor(CopyOnWriteContext<T> cow)
        {
            if (ReferenceEquals(Cow, cow))
            {
                return this;
            }

            Node<T> node = Cow.NewNode();

            node.Items.Append(Items);
            node.Children.Append(Children);

            return node;
        }

        public Node<T> MutableChild(int i)
        {
            Node<T> c = Children[i].MutableFor(Cow);
            Children[i] = c;
            return c;
        }

        // Splits the given node at the given index.  The current node shrinks,
        // and this function returns the item that existed at that index and a new node
        // containing all items/children after it.        
        public (T item, Node<T> node) Split(int i)
        {
            T item = Items[i];
            Node<T> next = Cow.NewNode();
            next.Items.Append(Items.GetRange(i + 1, Items.Length - (i + 1)));
            Items.Truncate(i);
            if (Children.Length > 0)
            {
                next.Children.Append(Children.GetRange(i + 1, Children.Length - (i + 1)));
                Children.Truncate(i + 1);
            }
            return (item, next);
        }

        // Checks if a child should be split, and if so splits it.
        // Returns whether or not a split occurred.        
        public bool MaybeSplitChild(int i, int maxItems)
        {
            if (Children[i].Items.Length < maxItems)
            {
                return false;
            }
            Node<T> first = MutableChild(i);
            (T item, Node<T> second) = first.Split(maxItems / 2);
            Items.InsertAt(i, item);
            Children.InsertAt(i + 1, second);
            return true;
        }

        // Inserts an item into the subtree rooted at this node, making sure
        // no nodes in the subtree exceed maxItems items.  Should an equivalent item be
        // be found/replaced by insert, it will be returned.        
        public T Insert(T item, int maxItems)
        {
            (int i, bool found) = Items.Find(item);
            if (found)
            {
                T n = Items[i];
                Items[i] = item;
                return n;
            }
            if (Children.Length == 0)
            {
                Items.InsertAt(i, item);
                return null;
            }
            if (MaybeSplitChild(i, maxItems))
            {
                T inTree = Items[i];
                if (Less(item, inTree))
                {
                    // no change, we want first split node
                }
                else if (Less(inTree, item))
                {
                    i++; // we want second split node
                }
                else
                {
                    T n = Items[i];
                    Items[i] = item;
                    return n;
                }
            }
            return MutableChild(i).Insert(item, maxItems);
        }

        // Finds the given key in the subtree and returns it.        
        public T Get(T key)
        {
            (int i, bool found) = Items.Find(key);
            if (found)
            {
                return Items[i];
            }
            else if (Children.Length > 0)
            {
                return Children[i].Get(key);
            }
            return null;
        }

        // Returns the first item in the subtree.        
        public static T Min(Node<T> n)
        {
            if (n == null)
            {
                return null;
            }
            while (n.Children.Length > 0)
            {
                n = n.Children[0];
            }
            return n.Items.Length == 0 ? null : n.Items[0];
        }

        // Returns the last item in the subtree.        
        public static T Max(Node<T> n)
        {
            if (n == null)
            {
                return null;
            }
            while (n.Children.Length > 0)
            {
                n = n.Children[n.Children.Length - 1];
            }
            return n.Items.Length == 0 ? null : n.Items[n.Items.Length - 1];
        }

        // Removes an item from the subtree rooted at this node.        
        public T Remove(T item, int minItems, ToRemove typ)
        {
            int i = 0;
            bool found = false;
            switch (typ)
            {
                case ToRemove.RemoveMax:
                    {
                        if (Children.Length == 0)
                        {
                            return Items.Pop();
                        }
                        i = Items.Length;
                    }
                    break;
                case ToRemove.RemoveMin:
                    {
                        if (Children.Length == 0)
                        {
                            return Items.RemoveAt(0);
                        }
                        i = 0;
                    }
                    break;
                case ToRemove.RemoveItem:
                    {
                        (i, found) = Items.Find(item);
                        if (Children.Length == 0)
                        {
                            return found ? Items.RemoveAt(i) : null;
                        }
                    }
                    break;
                default:
                    Environment.FailFast("invalid type");
                    break;
            }
            // If we get to here, we have children.
            if (Children[i].Items.Length <= minItems)
            {
                return GrowChildAndRemove(i, item, minItems, typ);
            }
            Node<T> child = MutableChild(i);
            // Either we had enough items to begin with, or we've done some
            // merging/stealing, because we've got enough now and we're ready to return
            if (found)
            {
                // The item exists at index 'i', and the child we've selected can give us a
                // predecessor, since if we've gotten here it's got > minItems items in it.
                T n = Items[i];
                // We use our special-case 'remove' call with typ=maxItem to pull the
                // predecessor of item i (the rightmost leaf of our immediate left child)
                // and set it into where we pulled the item from.
                Items[i] = child.Remove(null, minItems, ToRemove.RemoveMax);
                return n;
            }
            // Final recursive call.  Once we're here, we know that the item isn't in this
            // node and that the child is big enough to remove from.
            return child.Remove(item, minItems, typ);
        }
            public T GrowChildAndRemove(int i, T item, int minItems, ToRemove typ)
            {
                if (i > 0 && Children[i - 1].Items.Length > minItems)
                {
                    // Steal from left child
                    Node<T> child = MutableChild(i);
                    Node<T> stealFrom = MutableChild(i - 1);
                    T stolenItem = stealFrom.Items.Pop();
                    child.Items.InsertAt(0, Items[i - 1]);
                    Items[i - 1] = stolenItem;
                    if (stealFrom.Children.Length > 0)
                    {
                        child.Children.InsertAt(0, stealFrom.Children.Pop());
                    }
                }
                else if (i < Items.Length && Children[i + 1].Items.Length > minItems)
                {
                    // steal from right child
                    Node<T> child = MutableChild(i);
                    Node<T> stealFrom = MutableChild(i + 1);
                    T stolenItem = stealFrom.Items.RemoveAt(0);
                    child.Items.Append(Items[i]);
                    Items[i] = stolenItem;
                    if (stealFrom.Children.Length > 0)
                    {
                        child.Children.Append(stealFrom.Children.RemoveAt(0));
                    }
                }
                else
                {
                    if (i >= Items.Length)
                    {
                        i--;
                    }
                    Node<T> child = MutableChild(i);
                    // merge with right child
                    T mergeItem = Items.RemoveAt(i);
                    Node<T> mergeChild = Children.RemoveAt(i + 1);
                    child.Items.Append(mergeItem);
                    child.Items.Append(mergeChild.Items);
                    child.Children.Append(mergeChild.Children);
                    _ = Cow.FreeNode(mergeChild);
                }
                return Remove(item, minItems, typ);
            }
        
            public (bool, bool) Iterate(Direction dir, T start, T stop, bool includeStart, bool hit, ItemIterator<T> iter)
            {
                bool ok, found;
                int index = 0;
                switch (dir)
                {
                    case Direction.Ascend:
                        {
                            if (start != null)
                            {
                                (index, _) = Items.Find(start);
                            }
                            for (int i = index; i < Items.Length; i++)
                            {
                                if (Children.Length > 0)
                                {
                                    (hit, ok) = Children[i].Iterate(dir, start, stop, includeStart, hit, iter);
                                    if (!ok)
                                    {
                                        return (hit, false);
                                    }
                                }
                                if (!includeStart && !hit && start != null && !Less(start, Items[i]))
                                {
                                    hit = true;
                                    continue;
                                }
                                hit = true;
                                if (stop != null && !Less(Items[i], stop))
                                {
                                    return (hit, false);
                                }
                                if (!iter(Items[i]))
                                {
                                    return (hit, false);
                                }
                            }
                            if (Children.Length > 0)
                            {
                                (hit, ok) = Children[Children.Length - 1].Iterate(dir, start, stop, includeStart, hit, iter);
                                if (!ok)
                                {
                                    return (hit, false);
                                }
                            }
                        }
                        break;
                    case Direction.Descend:
                        {
                            if (start != null)
                            {
                                (index, found) = Items.Find(start);
                                if (!found)
                                {
                                    index -= 1;
                                }
                            }
                            else
                            {
                                index = Items.Length - 1;
                            }
                            for (int i = index; i >= 0; i--)
                            {
                                if (start != null && !Less(Items[i], start))
                                {
                                    if (!includeStart || hit || Less(start, Items[i]))
                                    {
                                        continue;
                                    }
                                }
                                if (Children.Length > 0)
                                {
                                    (hit, ok) = Children[i + 1].Iterate(dir, start, stop, includeStart, hit, iter);
                                    if (!ok)
                                    {
                                        return (hit, false);
                                    }
                                }
                                if (stop != null && !Less(stop, Items[i]))
                                {
                                    return (hit, false);
                                }
                                hit = true;
                                if (!iter(Items[i]))
                                {
                                    return (hit, false);
                                }
                            }
                            if (Children.Length > 0)
                            {
                                (hit, ok) = Children[0].Iterate(dir, start, stop, includeStart, hit, iter);
                                if (!ok)
                                {
                                    return (hit, false);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
                return (hit, true);
            }
     
            public bool Reset(CopyOnWriteContext<T> c)
            {
                foreach (Node<T> child in Children)
                {
                    if (!child.Reset(c))
                    {
                        return false;
                    }
                }
                return c.FreeNode(this) != FreeType.ftFreeListFull;
            }

            // Used for testing/debugging purposes.        
            public void Print(System.IO.TextWriter w, int level)
            {
                string repeat = new string(' ', level);
                w.Write($"{repeat}NODE:{Items}\n");
                foreach (Node<T> c in Children)
                {
                    c.Print(w, level + 1);
                }
            }

            private bool Less(T x, T y)
            {
                return Comparer.Compare(x, y) == -1;
            }
        
    }
    }
