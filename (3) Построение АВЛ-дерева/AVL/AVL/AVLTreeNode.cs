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

        public AVLTreeNode(int key)
        {
            Key = key;
            Height = 1; // Начальная высота узла
        }
    }
}
