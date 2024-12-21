using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    class Node
    {
        public char Symbol { get; set; }
        public double Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(char symbol, double frequency)
        {
            Symbol = symbol;
            Frequency = frequency;
        }

        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
            Frequency = left.Frequency + right.Frequency;
        }
    }
}
