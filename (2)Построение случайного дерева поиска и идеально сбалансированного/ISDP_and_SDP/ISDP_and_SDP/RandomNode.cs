using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDP_and_SDP
{
    class RandomNode
    {
        public int Value;
        public RandomNode Left;
        public RandomNode Right;
        public int Index;
        public static int Counter = 0;
        public RandomNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Counter++;
            Index += Counter;
        }
        public override string ToString()
        {
            return $"{Value}({Index})";
        }
    }

}
