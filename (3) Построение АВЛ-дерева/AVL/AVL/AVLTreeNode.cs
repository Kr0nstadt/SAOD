using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class AVLTreeNode
    {
        public int Key;
        public AVLTreeNode Left;
        public AVLTreeNode Right;
        public int Height;
        public int Index;

        public AVLTreeNode(int key, int index)
        {
            Key = key;
            Height = 0; // Начальная высота узла
            Index = index;
        }
    }
}
