using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    internal class CopyOnWriteContext<T> where T : class
    {
        public FreeList<T> FreeList { get; internal set; }

        public Node<T> NewNode()
        {
            Node<T> n = FreeList.NewNode();
            n.Cow = this;
            return n;
        }
    
        public FreeType FreeNode(Node<T> n)
        {
            if (ReferenceEquals(n.Cow, this))
            {
                // clear to allow GC
                n.Items.Truncate(0);
                n.Children.Truncate(0);
                n.Cow = null;
                return FreeList.FreeNode(n) ? FreeType.ftStored : FreeType.ftFreeListFull;
            }
            else
            {
                return FreeType.ftNotOwned;
            }
        }
    }
}
