using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class BTreeNode
    {
        public int Key;
        public BTreeNode Left;
        public BTreeNode Right;
        public int Height;
        public int Index;

        public BTreeNode(int key, int index)
        {
            Key = key;
            Height = 0; // Начальная высота узла
            Index = index;
        }
    }

}
