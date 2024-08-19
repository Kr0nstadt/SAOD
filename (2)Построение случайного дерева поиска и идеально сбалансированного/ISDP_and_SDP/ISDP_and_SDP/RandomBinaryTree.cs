using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDP_and_SDP
{
    class RandomBinaryTree
    {
        public RandomNode Root;
        private int _searchCount;
        public int SearchCount
        {
            get { return _searchCount; }
            set { _searchCount += value; }
        }

        public RandomBinaryTree()
        {
            Root = null;
        }

        // Метод для добавления узлов в случайное дерево
        public void Add(int value)
        {
            Root = AddToTree(Root, value);
        }
        public void Add(int[] value)
        {
            for(int i = 0; i < value.Length; i++)
            {
                Root = AddToTree(Root, value[i]);   
            }
        }

        private RandomNode AddToTree(RandomNode node, int value)
        {
            if (node == null)
                return new RandomNode(value);

            // Добавляем случайным образом влево или вправо
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                if (value < node.Value)
                    node.Left = AddToTree(node.Left, value);
                else
                    node.Right = AddToTree(node.Right, value);
            }
            else
            {
                if (value >= node.Value)
                    node.Right = AddToTree(node.Right, value);
                else
                    node.Left = AddToTree(node.Left, value);
            }

            return node;
        }

        public double AverageHeight()
        {
            return (double)Height(Root) / CountNodes(Root);
        }

        public int Height(RandomNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private int CountNodes(RandomNode node)
        {
            if (node == null) return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        public bool IsBinarySearchTree()
        {
            return IsBSTUtil(Root, int.MinValue, int.MaxValue);
        }

        private bool IsBSTUtil(RandomNode node, int min, int max)
        {
            if (node == null) return true;

            if (node.Value < min || node.Value > max)
                return true;

            return IsBSTUtil(node.Left, min, node.Value - 1) && IsBSTUtil(node.Right, node.Value + 1, max);
        }

        public bool Search(int key)
        {
            return SearchNode(Root, key);
        }

        private bool SearchNode(RandomNode node, int key)
        {
            if (node == null) return false;
            if (node.Value == key) return true;
            SearchCount = 1;
            return key < node.Value ? SearchNode(node.Left, key) : SearchNode(node.Right, key);
        }

        // Метод для красивого вывода дерева
        public void PrintTree()
        {
            PrintTree(Root, "", true);
        }

        private void PrintTree(RandomNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R---- ");
                    indent += "   ";
                }
                else
                {
                    Console.Write("L---- ");
                    indent += "|  ";
                }
                Console.WriteLine(node.Value);
                PrintTree(node.Left, indent, false);
                PrintTree(node.Right, indent, true);
            }
        }
    }
}
