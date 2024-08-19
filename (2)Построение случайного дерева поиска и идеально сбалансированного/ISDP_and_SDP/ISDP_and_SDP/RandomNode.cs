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

        public RandomNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

}
