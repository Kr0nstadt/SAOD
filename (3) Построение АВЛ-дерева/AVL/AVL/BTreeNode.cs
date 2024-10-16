using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class BTreeNode
    {
        public int[] Keys;
        public BTreeNode[] Children;
        public int Degree;
        public int KeyCount;
        public bool IsLeaf;

        public BTreeNode(int degree, bool isLeaf)
        {
            Degree = degree;
            IsLeaf = isLeaf;
            Keys = new int[2 * degree - 1];
            Children = new BTreeNode[2 * degree];
            KeyCount = 0;
        }

        public void InsertNonFull(int key)
        {
            int i = KeyCount - 1;

            if (IsLeaf)
            {
                while (i >= 0 && Keys[i] > key)
                {
                    Keys[i + 1] = Keys[i];
                    i--;
                }
                Keys[i + 1] = key;
                KeyCount++;
            }
            else
            {
                while (i >= 0 && Keys[i] > key)
                    i--;

                if (Children[i + 1].KeyCount == 2 * Degree - 1)
                {
                    SplitChild(i + 1);
                    if (Keys[i + 1] < key)
                        i++;
                }
                Children[i + 1].InsertNonFull(key);
            }
        }

        public void SplitChild(int i)
        {
            var y = Children[i];
            var z = new BTreeNode(y.Degree, y.IsLeaf);
            z.KeyCount = Degree - 1;

            for (int j = 0; j < Degree - 1; j++)
                z.Keys[j] = y.Keys[j + Degree];

            if (!y.IsLeaf)
            {
                for (int j = 0; j < Degree; j++)
                    z.Children[j] = y.Children[j + Degree];
            }

            y.KeyCount = Degree - 1;

            for (int j = KeyCount; j >= i + 1; j--)
                Children[j + 1] = Children[j];

            Children[i + 1] = z;

            for (int j = KeyCount - 1; j >= i; j--)
                Keys[j + 1] = Keys[j];

            Keys[i] = y.Keys[Degree - 1];
            KeyCount++;
        }
        public override string ToString()
        {
            var txt = "";
            foreach(int i in Keys)
            {
                txt += i.ToString() + " ";
            }
            return txt;
        }
    }

}
