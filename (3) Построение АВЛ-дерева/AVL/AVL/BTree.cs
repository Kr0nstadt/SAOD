using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class BTree
    {
        private BTreeNode _root;
        private int _degree;

        public BTree(int degree)
        {
            _root = null;
            _degree = degree;
        }

        public void Insert(int key)
        {
            if (_root == null)
            {
                _root = new BTreeNode(_degree, true);
                _root.Keys[0] = key;
                _root.KeyCount = 1;
            }
            else
            {
                if (_root.KeyCount == 2 * _degree - 1)
                {
                    var newRoot = new BTreeNode(_degree, false);
                    newRoot.Children[0] = _root;
                    newRoot.SplitChild(0);
                    int i = 0;
                    if (newRoot.Keys[0] < key)
                        i++;
                    newRoot.Children[i].InsertNonFull(key);
                    _root = newRoot;
                }
                else
                {
                    _root.InsertNonFull(key);
                }
            }
        }

        public int Size()
        {
            return Size(_root);
        }

        private int Size(BTreeNode node)
        {
            if (node == null) return 0;

            int size = node.KeyCount;

            for (int i = 0; i <= node.KeyCount; i++)
                size += Size(node.Children[i]);

            return size;
        }

        public int Sum()
        {
            return Sum(_root);
        }

        private int Sum(BTreeNode node)
        {
            if (node == null) return 0;

            int sum = 0;

            for (int i = 0; i < node.KeyCount; i++)
                sum += node.Keys[i];

            for (int i = 0; i <= node.KeyCount; i++)
                sum += Sum(node.Children[i]);

            return sum;
        }

        public int Height()
        {
            return Height(_root);
        }

        private int Height(BTreeNode node)
        {
            if (node == null) return 0;

            if (node.IsLeaf) return 1;

            return 1 + Height(node.Children[0]);
        }

        public double AverageHeight()
        {
            return AverageHeight(_root, 1) / Size();
        }

        private double AverageHeight(BTreeNode node, int currentHeight)
        {
            if (node == null) return 0;

            double totalHeight = currentHeight;

            for (int i = 0; i < node.KeyCount; i++)
                totalHeight += AverageHeight(node.Children[i], currentHeight + 1);
            for (int i = node.KeyCount; i <= node.KeyCount; i++)
                totalHeight += AverageHeight(node.Children[i], currentHeight + 1);

            return totalHeight;
        }

        public int Levels()
        {
            return Levels(_root);
        }

        private int Levels(BTreeNode node)
        {
            if (node == null) return 0;

            int maxLevel = 0;

            for (int i = 0; i <= node.KeyCount; i++)
                maxLevel = Math.Max(maxLevel, Levels(node.Children[i]));

            return maxLevel + 1;
        }

        public void InOrderTraversal()
        {
            InOrderTraversal(_root);
        }

        private void InOrderTraversal(BTreeNode node)
        {
            if (node != null)
            {
                for (int i = 0; i < node.KeyCount; i++)
                {
                    InOrderTraversal(node.Children[i]);
                    Console.Write(node.Keys[i] + " ");
                }
                InOrderTraversal(node.Children[node.KeyCount]);
            }
        }
        public void Print()
        {
            Print(_root, "", true);
        }

        private void Print(BTreeNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("└── ");
                    indent += "    ";
                }
                else
                {
                    Console.Write("├── ");
                    indent += "│   ";
                }
                var txt = "";
                foreach(var val in node.Keys)
                {
                    txt += val + " ";
                }
                Console.WriteLine(string.Join(" ", txt, "|", node.KeyCount));

                for (int i = 0; i <= node.KeyCount; i++)
                    Print(node.Children[i], indent, i == node.KeyCount);
            }
        }
    }
}
