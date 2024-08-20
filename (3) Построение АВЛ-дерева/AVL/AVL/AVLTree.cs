using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class AVLTree
    {
        private AVLTreeNode root;
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

        // Правый поворот
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

            RotationCount++; // Увеличиваем счетчик поворотов
            return x; // Новый корень
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

            RotationCount++; // Увеличиваем счетчик поворотов
            return y; // Новый корень
        }

        // Добавление узла
        public void Add(int key)
        {
            root = Add(root, key);
        }

        private AVLTreeNode Add(AVLTreeNode node, int key)
        {
            // Стандартная вставка BST
            if (node == null)
                return new AVLTreeNode(key);

            if (key < node.Key)
                node.Left = Add(node.Left, key);
            else if (key > node.Key)
                node.Right = Add(node.Right, key);
            else // Дубликаты не допускаются
                return node;

            // Обновляем высоту предка узла
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            // Проверяем баланс
            int balance = GetBalance(node);

            // Если узел стал несбалансированным, выполняем соответствующие повороты

            // Левый левый случай
            if (balance > 1 && key < node.Left.Key)
                return RightRotate(node);

            // Правый правый случай
            if (balance < -1 && key > node.Right.Key)
                return LeftRotate(node);

            // Левый правый случай
            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Правый левый случай
            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node; // Возвращаем (неизмененный) узел
        }

        // Метод для вычисления средней высоты дерева
        public double AverageHeight()
        {
            if (root == null) return 0; // Если дерево пустое, возвращаем 0
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
                totalLength += currentLength; // Суммируем длину ветки
                branchCount++; // Увеличиваем счетчик веток
            }

            // Рекурсивно вызываем для левого и правого детей
            CalculateBranchLengths(node.Left, currentLength + 1, ref totalLength, ref branchCount);
            CalculateBranchLengths(node.Right, currentLength + 1, ref totalLength, ref branchCount);
        }

        private int CountNodes(AVLTreeNode node)
        {
            if (node == null) return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        // Метод для получения высоты дерева
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
                Console.WriteLine(node.Key);
                Print(node.Left, indent, false);
                Print(node.Right, indent, true);
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

        // Метод для вставки значения в AVL-дерево
        private void Insert(int value)
        {
            // Логика вставки узла в AVL-дерево
            // Это может быть ваш существующий метод вставки
            root = InsertNode(root, value);
        }

        private AVLTreeNode InsertNode(AVLTreeNode node, int value)
        {
            // Логика вставки узла и балансировки дерева
            // ...
            return node; // Возвращаем корень обновленного дерева
        }
    }
}
