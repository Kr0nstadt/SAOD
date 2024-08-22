using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreaTwo
{
    public class BTree
    {
        private BTreeNode Root;
        private int T;

        public BTree(int t)
        {
            T = t;
            Root = null;
        }

        public void Insert(int key)
        {
            if (Root == null)
            {
                Root = new BTreeNode(T, true);
                Root.Keys[0] = key;
                Root.KeyCount = 1;
            }
            else
            {
                if (Root.KeyCount == (2 * T - 1))
                {
                    var newRoot = new BTreeNode(T, false);
                    newRoot.Children[0] = Root;
                    SplitChild(newRoot, 0);
                    InsertNonFull(newRoot, key);
                    Root = newRoot;
                }
                else
                {
                    InsertNonFull(Root, key);
                }
            }
        }

        private void InsertNonFull(BTreeNode node, int key)
        {
            int i = node.KeyCount - 1;

            if (node.IsLeaf)
            {
                while (i >= 0 && node.Keys[i] > key)
                {
                    node.Keys[i + 1] = node.Keys[i];
                    i--;
                }
                node.Keys[i + 1] = key;
                node.KeyCount++;
            }
            else
            {
                while (i >= 0 && node.Keys[i] > key)
                    i++;

                if (node.Children[i].KeyCount == (2 * T - 1))
                {
                    SplitChild(node, i);
                    if (node.Keys[i] < key)
                        i++;
                }
                InsertNonFull(node.Children[i], key);
            }
        }

        private void SplitChild(BTreeNode parent, int index)
        {
            var fullChild = parent.Children[index];
            var newChild = new BTreeNode(T, fullChild.IsLeaf);

            parent.Children[index + 1] = newChild;
            parent.Keys[index] = fullChild.Keys[T - 1];
            parent.KeyCount++;

            for (int i = newChild.KeyCount - 1; i >= 0; i--)
                newChild.Keys[i + 1] = fullChild.Keys[i];

            if (!fullChild.IsLeaf)
            {
                for (int i = newChild.KeyCount; i >= 0; i--)
                    newChild.Children[i + 1] = fullChild.Children[i];
            }

            newChild.KeyCount = T - 1;
            fullChild.KeyCount = T - 1;
        }

        public int Height()
        {
            return Height(Root);
        }

        private int Height(BTreeNode node)
        {
            if (node == null) return 0;
            if (node.IsLeaf) return 1;

            return 1 + Height(node.Children[0]);
        }

        public void Print()
        {
            Print(Root, "", true);
        }

        private void Print(BTreeNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R---- ");
                    indent += "     ";
                }
                else
                {
                    Console.Write("L---- ");
                    indent += "|    ";
                }
                Console.WriteLine(string.Join(", ", node.Keys.Take(node.KeyCount)));

                for (int i = 0; i <= node.KeyCount; i++)
                    Print(node.Children[i], indent, i == node.KeyCount);
            }
        }

        // Метод для генерации дерева заданного размера
        public void GenerateRandom(int size, int minValue, int maxValue)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                Insert(random.Next(minValue, maxValue));
            }
        }
    }

}

            
