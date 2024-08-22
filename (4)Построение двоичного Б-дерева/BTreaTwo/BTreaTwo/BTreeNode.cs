using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreaTwo
{
    public class BTreeNode
    {
        public int[] Keys { get; private set; }
        public BTreeNode[] Children { get; private set; }
        public int KeyCount { get; set; }
        public bool IsLeaf { get; private set; }
        public int T { get; private set; }

        public BTreeNode(int t, bool isLeaf)
        {
            T = t;
            IsLeaf = isLeaf;
            Keys = new int[2 * T - 1];
            Children = new BTreeNode[2 * T];
            KeyCount = 0;
        }
    }

}
