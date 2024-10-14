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
        public void Delete(int key)
        {
            Root = DeleteRec(Root, key);
        }

        private RandomNode DeleteRec(RandomNode node, int key)
        {

            if (node == null)
            {
                return node;
            }


            if (key < node.Value)
            {
                node.Left = DeleteRec(node.Left, key);
            }
            else if (key > node.Value)
            {
                node.Right = DeleteRec(node.Right, key);
            }
            else
            {

                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }

                node.Value = MinValue(node.Right);

                node.Right = DeleteRec(node.Right, node.Value);
            }

            return node;
        }

        private int MinValue(RandomNode node)
        {
            int minValue = node.Value;
            while (node.Left != null)
            {
                minValue = node.Left.Value;
                node = node.Left;
            }
            return minValue;
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
 
        public void AddTwo(int[] value)
        {
            for(int i = 0; i < value.Length; i++)
            {
                 AddToTreeTwo(value[i]);   
            }
        }

        private void AddToTreeTwo( int value)
        {
            if (Root == null)
            {
                Root = new RandomNode(value);
                return;
            }
            RandomNode current = Root;
            RandomNode parent = null;
            while (true)
            {
                parent = current;
                if(value < current.Value)
                {
                    current = current.Left;
                    if(current == null)
                    {
                        parent.Left = new RandomNode(value);
                        return;
                    }
                }
                else
                {
                    current = current.Right;
                    if(current == null)
                    {
                        parent.Right = new RandomNode(value);
                        return;
                    }
                }
            }

            
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
        public void InOrderTraversalLeft(Action<RandomNode> action)
        {
            InOrderTraversalRecursive(Root, action);
        }

        private void InOrderTraversalRecursive(RandomNode node, Action<RandomNode> action)
        {
            if (node != null)
            {
                
                InOrderTraversalRecursive(node.Left, action);
                action(node);
                InOrderTraversalRecursive(node.Right, action);
            }
        }
        public override string ToString()
        {
            return $"{Root.Value}({Root.Index})";
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
                Console.WriteLine($"{node.Value}({node.Index})");
                PrintTree(node.Left, indent, false);
                PrintTree(node.Right, indent, true);
            }
        }
        public void PrintVal(Dictionary<string, int> wordCount)
        {
            PrintVal(Root, "", true, wordCount);
        }
        private void PrintVal(RandomNode node, string indent, bool last, Dictionary<string, int> wordCount)
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
                //if(wordCount.TryGetValue(node.Value, out string value))
                {

                }
                Console.WriteLine($"{node.Value}({node.Index})");
                PrintTree(node.Left, indent, false);
                PrintTree(node.Right, indent, true);
            }
        }
    }
}
