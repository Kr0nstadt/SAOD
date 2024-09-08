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
        public int Size()
        {
            return SizeRecursive(Root);
        }

        private int SizeRecursive(RandomNode node)
        {
            if (node == null) return 0;
            return 1 + SizeRecursive(node.Left) + SizeRecursive(node.Right);
        }
        // Контрольная сумма для дерева
        public int Checksum()
        {
            return ChecksumRecursive(Root);
        }

        private int ChecksumRecursive(RandomNode node)
        {
            if (node == null) return 0;
            return node.Value + ChecksumRecursive(node.Left) + ChecksumRecursive(node.Right);
        }

        // Вычисление высоты дерева
        public int Height()
        {
            return HeightRecursive(Root);
        }

        private int HeightRecursive(RandomNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(HeightRecursive(node.Left), HeightRecursive(node.Right));
        }
        // Метод для добавления узлов в случайное дерево
        public void AddTwo(int value)
        {
            Root = AddToTreeTwo(ref Root, value);
        }
        public void AddTwo(int[] value)
        {
            for(int i = 0; i < value.Length; i++)
            {
                Root = AddToTreeTwo( ref Root, value[i]);   
            }
        }

        private RandomNode AddToTreeTwo(ref RandomNode node, int value)
        {
            if (node == null)
                return new RandomNode(value);

            // Добавляем случайным образом влево или вправо
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                if (value < node.Value)
                    node.Left = AddToTreeTwo( ref node.Left, value);
                else
                    node.Right = AddToTreeTwo(ref node.Right, value);
            }
            else
            {
                if (value >= node.Value)
                    node.Right = AddToTreeTwo(ref node.Right, value);
                else
                    node.Left = AddToTreeTwo(ref node.Left, value);
            }

            return node;
        }
        public void Add(int value)
        {
            Root = AddToTree( Root, value);
        }
        public void Add(int[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Root = AddToTree( Root, value[i]);
            }
        }

        private RandomNode AddToTree( RandomNode node, int value)
        {
            if (node == null)
                return new RandomNode(value);

            // Добавляем случайным образом влево или вправо
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                if (value < node.Value)
                    node.Left = AddToTree( node.Left, value);
                else
                    node.Right = AddToTree( node.Right, value);
            }
            else
            {
                if (value >= node.Value)
                    node.Right = AddToTree( node.Right, value);
                else
                    node.Left = AddToTree( node.Left, value);
            }

            return node;
        }

        public double AverageHeight()
        {
            int totalHeight = 0;
            int branchCount = 0;

            CalculateHeight(Root, 1, ref totalHeight, ref branchCount);

            return branchCount == 0 ? 0 : (double)totalHeight / branchCount;
        }

        private void CalculateHeight(RandomNode node, int currentHeight, ref int totalHeight, ref int branchCount)
        {
            if (node == null)
                return;

            // Если это листовой узел
            if (node.Left == null && node.Right == null)
            {
                totalHeight += currentHeight; // Суммируем высоту
                branchCount++; // Увеличиваем счетчик ветвей
            }

            // Рекурсивно проходим влево и вправо
            CalculateHeight(node.Left, currentHeight + 1, ref totalHeight, ref branchCount);
            CalculateHeight(node.Right, currentHeight + 1, ref totalHeight, ref branchCount);
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

        public RandomNode Search(int key)
        {
            return SearchNode(Root, key);
        }

        private RandomNode SearchNode(RandomNode node, int key)
        {
            if (node == null)
            {
                SearchCount++;
                return new RandomNode(-1);
            }
            if (node.Value == key) { SearchCount++; return node; }
            SearchCount++;
            return key < node.Value ? SearchNode(node.Left, key) : SearchNode(node.Right, key);
        }

        // Метод для красивого вывода дерева
        public void PrintTree()
        {
            PrintTree(Root, "", true);
        }
        public void InOrderTraversalLeft(Action<int> action)
        {
            InOrderTraversalRecursive(Root, action);
        }

        private void InOrderTraversalRecursive(RandomNode node, Action<int> action)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left, action);
                action(node.Value);
                InOrderTraversalRecursive(node.Right, action);
            }
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
