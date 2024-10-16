using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AVL
{
    public class AVLTree
    {
        private AVLTreeNode root;
        private int _countAdd;
        private int _countDelete;
        public int CountAdd => _countAdd;
        public int CountDelete => _countDelete; 
        public int RotationCount { get; private set; }

        // Метод для получения высоты узла
        private int GetHeight(AVLTreeNode node)
        {
            return node?.Height ?? 0;
        }

        // Метод для вычисления баланса узла
        private int GetBalance(AVLTreeNode node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }
        public void Delete(int key)
        {
            root = DeleteNode(root, key);
        }
        private AVLTreeNode LL1(AVLTreeNode p,bool val)
        {
            AVLTreeNode q = p.Left;
            if (q.Height == 0) {q.Height = 1;p.Height = -1; }
            else { q.Height = 0;p.Height = 0; }
            p.Left = q.Right;
            q.Right = p;
            p = q;
            return p;
        }
        private AVLTreeNode RR1(AVLTreeNode p, bool val)
        {
            AVLTreeNode q = p.Right;
            if (q.Height == 0) { q.Height = 1; p.Height = -1; }
            else { q.Height = 0; p.Height = 0; }
            p.Right = q.Left;
            q.Left = p;
            p = q;
            return p;
        }
        private AVLTreeNode DeleteNode(AVLTreeNode node, int key)
        {
            if (node == null)
                return node;

            if (key < node.Key)
                node.Left = DeleteNode(node.Left, key);
            else if (key > node.Key)
                node.Right = DeleteNode(node.Right, key);
            else
            {
                if (node.Left == null)
                    return node.Right;
                else if (node.Right == null)
                    return node.Left;

                node.Key = MinValue(node.Right);

                node.Right = DeleteNode(node.Right, node.Key);
            }


            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.Left) >= 0)
            {   _countDelete++;
                return RR1(node);
            }

            // Правый правый случай
            if (balance < -1 && GetBalance(node.Right) <= 0)
            {   _countDelete++;
                return LL1(node);
            }

            // Левый правый случай
            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                _countDelete++;
                node.Left = LL1(node.Left);
                return RR1(node);
            }

            // Правый левый случай
            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                _countDelete++;
                node.Right = RR1(node.Right);
                return LL1(node);
            }

            return node;
        }
        private int MinValue(AVLTreeNode node)
        {
            int minv = node.Key;
            while (node.Left != null)
            {
                minv = node.Left.Key;
                node = node.Left;
            }
            return minv;
        }

        private AVLTreeNode RightRotate(AVLTreeNode y)
        {
            AVLTreeNode x = y.Left;
            AVLTreeNode T2 = x.Right;

            // Выполняем поворот
            x.Right = y;
            y.Left = T2;

            // Обновляем высоты
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            // Возвращаем новый корень
            return x;
        }
        private AVLTreeNode RR1(AVLTreeNode y)
        {
            AVLTreeNode x = y.Left;
            AVLTreeNode T2 = x.Right;

            // Выполняем поворот
            x.Right = y;
            y.Left = T2;

            // Обновляем высоты
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            // Возвращаем новый корень
            return x;
        }
        // Левый поворот
        private AVLTreeNode LeftRotate(AVLTreeNode x)
        {
            AVLTreeNode y = x.Right;
            AVLTreeNode T2 = y.Left;

            // Выполняем поворот
            y.Left = x;
            x.Right = T2;

            // Обновляем высоты
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            // Возвращаем новый корень
            return y;
        }
        private AVLTreeNode LL1(AVLTreeNode x)
        {
            AVLTreeNode y = x.Right;
            AVLTreeNode T2 = y.Left;

            // Выполняем поворот
            y.Left = x;
            x.Right = T2;

            // Обновляем высоты
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            // Возвращаем новый корень
            return y;
        }
        private AVLTreeNode LeftRigthRotate(AVLTreeNode x)
        {
            AVLTreeNode y = x.Right;
            AVLTreeNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
   

            AVLTreeNode k = y.Left;
            AVLTreeNode Tk = x.Right;

            k.Right = y;
            y.Left = Tk;

            k.Height = Math.Max(GetHeight(k.Left), GetHeight(k.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            RotationCount++;
            return x;
        }
        private AVLTreeNode RightLeftRotate(AVLTreeNode y)
        {

            AVLTreeNode k = y.Left;
            AVLTreeNode Tk = k.Right;

            k.Right = y;
            y.Left = Tk;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            k.Height = Math.Max(GetHeight(k.Left), GetHeight(k.Right)) + 1;

            RotationCount++;

            AVLTreeNode l = k.Right;
            AVLTreeNode T2 = l.Left;

            l.Left = k;
            k.Right = T2;

            k.Height = Math.Max(GetHeight(k.Left), GetHeight(k.Right)) + 1;
            l.Height = Math.Max(GetHeight(l.Left), GetHeight(l.Right)) + 1;

           
            return l;
        }

        // Добавление узла
        public void Add(int key)
        {
            root = Add(root, key);
        }
        public void Add(int[] key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                root = Add(root, key[i]);
            }
        }
        private AVLTreeNode Add(AVLTreeNode node, int key)
        {

            if (node == null)
                return new AVLTreeNode(key,0);

            if (key < node.Key)
                node.Left = Add(node.Left, key);
            else if (key > node.Key)
                node.Right = Add(node.Right, key);
            else 
                return node;


            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

        
            int balance = GetBalance(node);


            if (balance > 1 && key < node.Left.Key)
            {
                _countAdd++;
                return RightRotate(node); 
            }

            if (balance < -1 && key > node.Right.Key)
            {
                _countAdd++;
                return LeftRotate(node); 
            }

            if (balance > 1 && key > node.Left.Key)
            {
                _countAdd+=2;
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Правый левый случай
            if (balance < -1 && key < node.Right.Key)
            {
                _countAdd+= 2;
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node; 
        }


        public double AverageHeight()
        {
            if (root == null) return 0;
            int totalLength = 0;
            int branchCount = 0;
            CalculateBranchLengths(root, 1, ref totalLength, ref branchCount);
            return (double)totalLength / branchCount;
        }

        private void CalculateBranchLengths(AVLTreeNode node, int currentLength, ref int totalLength, ref int branchCount)
        {
            if (node == null) return;

            // Если это листовой узел
            if (node.Left == null && node.Right == null)
            {
                totalLength += currentLength;
                branchCount++; 
            }
            CalculateBranchLengths(node.Left, currentLength + 1, ref totalLength, ref branchCount);
            CalculateBranchLengths(node.Right, currentLength + 1, ref totalLength, ref branchCount);
        }

        private int CountNodes(AVLTreeNode node)
        {
            if (node == null) return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        public int Height()
        {
            return GetHeight(root);
        }

        public void Print()
        {
            Print(root, "", true);
        }

        private void Print(AVLTreeNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R----");
                    indent += "   ";
                }
                else
                {
                    Console.Write("L----");
                    indent += "|  ";
                }
                Console.WriteLine($"{node.Key}({node.Height})");
                Print(node.Left, indent, false);
                Print(node.Right, indent, true);
            }
        }
        private void PrintIndex(AVLTreeNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R----");
                    indent += "   ";
                }
                else
                {
                    Console.Write("L----");
                    indent += "|  ";
                }
                Console.WriteLine($"{node.Key}({node.Index})");
                PrintIndex(node.Left, indent, false);
                PrintIndex(node.Right, indent, true);
            }
        }
        public void GenerateRandomTree(int numberOfNodes)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfNodes; i++)
            {
                int value = random.Next(10, 100); // Генерируем случайное число от 10 до 99
                Add(value); // Метод для вставки значения в дерево
            }
        }

        public void InOrderTraversalLeft()
        {
            InOrderTraversalRecursive(root);
        }

        private void InOrderTraversalRecursive(AVLTreeNode node)
        {
            if (node != null)
            { 
                InOrderTraversalRecursive(node.Left);
                Console.Write(node.Key + "(" + node.Index + ")" + " ");
                InOrderTraversalRecursive(node.Right);
            }
        }
        public void InOrderTraversalApp()
        {
            InOrderTraversalRecursiveApp(root);
        }

        private void InOrderTraversalRecursiveApp(AVLTreeNode node)
        {
            if (node != null)
            {
                Console.Write(node.Key + "(" + node.Index + ")" + " ");

                InOrderTraversalRecursiveApp(node.Left);

                InOrderTraversalRecursiveApp(node.Right);
            }
        }

        public void InOrderTraversalDovn()
        {
            InOrderTraversalRecursiveDovn(root);
        }

        private void InOrderTraversalRecursiveDovn(AVLTreeNode node)
        {
            if (node != null)
            {
                InOrderTraversalRecursiveDovn(node.Left);

                InOrderTraversalRecursiveDovn(node.Right);
                Console.Write(node.Key + "(" + node.Index + ")" + " ");
            }
            
        }
        public int Size()
        {
            return SizeRecursive(root);
        }

        private int SizeRecursive(AVLTreeNode node)
        {
            if (node == null) return 0;
            return 1 + SizeRecursive(node.Left) + SizeRecursive(node.Right);
        }
        // Контрольная сумма для дерева
        public int Checksum()
        {
            return ChecksumRecursive(root);
        }

        private int ChecksumRecursive(AVLTreeNode node)
        {
            if (node == null) return 0;
            return node.Key + ChecksumRecursive(node.Left) + ChecksumRecursive(node.Right);
        }
        private AVLTreeNode RightLeft(AVLTreeNode node)
            {
                AVLTreeNode NewNode = LeftRotate(node.Left);
                return RightRotate(NewNode);
            }
        private AVLTreeNode LeftRight(AVLTreeNode node)
        {
            AVLTreeNode NewNode = RightRotate(node.Right);
            return LeftRotate(NewNode);
        }


        public void PrintTreeIndex()
        {
            int indexNode = 1;
            Queue<AVLTreeNode> qNodes = new Queue<AVLTreeNode>();
            if (root != null)
            {
                qNodes.Enqueue(root);
            }


            while (qNodes.Count > 0)
            {
                AVLTreeNode current = qNodes.Dequeue();
                if (current.Left != null)
                {
                    qNodes.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    qNodes.Enqueue(current.Right);
                }

                //action(current.Value);
                current.Index = indexNode;
                ++indexNode;

            }

            PrintIndex(root, "", true);
        }
    }
}
