using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrea
{
    class Node
    {
        public int Value;
        public Node Left;
        public Node Right;
        public int Index;
        private static int Counter = 1;
        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Counter++;
            Index += Counter;
        }
    }
}
