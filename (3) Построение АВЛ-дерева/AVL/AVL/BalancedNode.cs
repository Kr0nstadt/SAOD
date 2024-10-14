using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    class BalancedNode
    {
        public int Value;
        public BalancedNode Left;
        public BalancedNode Right;
        public int Index;
        public static int Counter = 0;
        public BalancedNode(int value, int index)
        {
            Value = value;
            Left = null;
            Right = null;
            Counter++;
            Index = index;

        }
    }
}
