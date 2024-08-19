using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDP_and_SDP
{
    class BalancedNode
    {
        public int Value;
        public BalancedNode Left;
        public BalancedNode Right;

        public BalancedNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
}
