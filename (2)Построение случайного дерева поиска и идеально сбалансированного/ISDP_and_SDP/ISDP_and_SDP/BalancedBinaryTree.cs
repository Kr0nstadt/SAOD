using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ISDP_and_SDP
{
    class BalancedBinaryTree
    {
        public BalancedNode Root;
        private int _searchCount;
        public int SearchCount
        {
            get { return _searchCount; }
            set { _searchCount += value; }
        }
        public BalancedBinaryTree()
        {
            Root = null;
        }

        public void Add(int[] values)
        {
            Array.Sort(values);
            Root = AddToTree(values, 0, values.Length - 1);
        }

        private BalancedNode AddToTree(int[] values, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            BalancedNode node = new BalancedNode(values[mid]);

            node.Left = AddToTree(values, start, mid - 1);
            node.Right = AddToTree(values, mid + 1, end);

            return node;
        }
        // Вычисление размера дерева
        public int Size()
        {
            return SizeRecursive(Root);
        }

        private int SizeRecursive(BalancedNode node)
        {
            if (node == null) return 0;
            return 1 + SizeRecursive(node.Left) + SizeRecursive(node.Right);
        }
        // Контрольная сумма для дерева
        public int Checksum()
        {
            return ChecksumRecursive(Root);
        }

        private int ChecksumRecursive(BalancedNode node)
        {
            if (node == null) return 0;
            return node.Value + ChecksumRecursive(node.Left) + ChecksumRecursive(node.Right);
        }

        // Вычисление высоты дерева
        public int Height()
        {
            return HeightRecursive(Root);
        }

        private int HeightRecursive(BalancedNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(HeightRecursive(node.Left), HeightRecursive(node.Right));
        }
        public double AverageHeight()
        {
            int totalHeight = 0;
            int branchCount = 0;

            CalculateHeight(Root, 1, ref totalHeight, ref branchCount);

            return branchCount == 0 ? 0 : (double)totalHeight / branchCount;
        }

        private void CalculateHeight(BalancedNode node, int currentHeight, ref int totalHeight, ref int branchCount)
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

        public int Height(BalancedNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private int CountNodes(BalancedNode node)
        {
            if (node == null) return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        public bool IsBinarySearchTree()
        {
            return IsBSTUtil(Root, int.MinValue, int.MaxValue);
        }

        private bool IsBSTUtil(BalancedNode node, int min, int max)
        {
            if (node == null) return true;

            if (node.Value < min || node.Value > max)
                return true;

            return IsBSTUtil(node.Left, min, node.Value - 1) && IsBSTUtil(node.Right, node.Value + 1, max);
        }

        public BalancedNode Search(int key)
        {
            return SearchNode(Root, key);
        }

        private BalancedNode SearchNode(BalancedNode node, int key)
        {
            if (node == null) { return new BalancedNode(-1); }
            if (node.Value == key) { return node; }
            SearchCount++; return key < node.Value ? SearchNode(node.Left, key) : SearchNode(node.Right, key);
        }
        // Обход дерева слева направо (инфиксный обход)
        public void InOrderTraversalLeft(Action<int> action)
        {
            InOrderTraversalRecursive(Root, action);
        }

        private void InOrderTraversalRecursive(BalancedNode node, Action<int> action)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left, action);
                action(node.Value);
                InOrderTraversalRecursive(node.Right, action);
            }
        }

        // Метод для красивого вывода дерева
        public void PrintTree()
        {
            PrintTree(Root, "", true);
        }

        private void PrintTree(BalancedNode node, string indent, bool last)
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
