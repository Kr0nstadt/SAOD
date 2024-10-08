﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrea
{
    class BinaryTree
{
    public Node Root;

    public BinaryTree()
    {
        Root = null;
    }

    // Добавление узла в дерево
    public void Add(int value)
    {
        Root = AddRecursive(Root, value);
    }

    private Node AddRecursive(Node node, int value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        if (value < node.Value)
        {
            node.Left = AddRecursive(node.Left, value);
        }
        else
        {
            node.Right = AddRecursive(node.Right, value);
        }

        return node;
    }

    // Вычисление размера дерева
    public int Size()
    {
        return SizeRecursive(Root);
    }

    private int SizeRecursive(Node node)
    {
        if (node == null) return 0;
        return 1 + SizeRecursive(node.Left) + SizeRecursive(node.Right);
    }

    // Вычисление высоты дерева
    public int Height()
    {
        return HeightRecursive(Root);
    }

    private int HeightRecursive(Node node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(HeightRecursive(node.Left), HeightRecursive(node.Right));
    }

        // Вычисление средней высоты дерева
        public double AverageHeight()
        {
            int totalHeight = 0;
            int nodeCount = 0;

            CalculateHeight(Root, 1, ref totalHeight, ref nodeCount);

            return nodeCount == 0 ? 0 : (double)totalHeight / nodeCount;
        }

        private void CalculateHeight(Node node, int currentHeight, ref int totalHeight, ref int nodeCount)
        {
            if (node == null)
                return;

            // Увеличиваем количество узлов
            nodeCount++;

            // Добавляем текущую высоту к общей сумме
            totalHeight += currentHeight;

            // Рекурсивно вызываем для левого и правого дочерних узлов
            CalculateHeight(node.Left, currentHeight + 1, ref totalHeight, ref nodeCount);
            CalculateHeight(node.Right, currentHeight + 1, ref totalHeight, ref nodeCount);
        }



        private int TotalHeight(Node node, int currentHeight)
    {
        if (node == null) return 0;
        return currentHeight + TotalHeight(node.Left, currentHeight + 1) + TotalHeight(node.Right, currentHeight + 1);
    }

    // Контрольная сумма для дерева
    public int Checksum()
    {
        return ChecksumRecursive(Root);
    }

    private int ChecksumRecursive(Node node)
    {
        if (node == null) return 0;
        return node.Value + ChecksumRecursive(node.Left) + ChecksumRecursive(node.Right);
    }


    // Обход дерева слева направо (инфиксный обход)
    public void InOrderTraversalLeft(Action<int> action)
    {
        InOrderTraversalRecursive(Root, action);
    }

    private void InOrderTraversalRecursive(Node node, Action<int> action)
    {
        if (node != null)
        {
                InOrderTraversalRecursive(node.Left, action);
                action(node.Value);
                InOrderTraversalRecursive(node.Right, action);
        }
    }
        // снизу вверх
        public void InOrderTraversalBelow(Action<int> action)
        {
            //InOrderTraversalRecursiveBelow(Root, action);
            InOrderTraversalNonRecursiveBelow(Root, action);
        }

        private void InOrderTraversalRecursiveBelow(Node node, Action<int> action)
        {
            if (node != null)
            {
                
                InOrderTraversalRecursive(node.Left, action);
                InOrderTraversalRecursive(node.Right, action);
                Console.Write(node.Value);
            }
        }

        private void InOrderTraversalNonRecursiveBelow(Node node, Action<int> action)
        {
            Stack<int> sNodes = new Stack<int>();
            InOrderTraversalNonRecursiveAbove(node, i => { sNodes.Push(i); });

            foreach (int i in sNodes)
            {
                action(i);
            }
        }
        // Сверху вниз
        public void InOrderTraversalAbove(Action<int> action)
        {
            //InOrderTraversalRecursiveAbove(Root, action);
            InOrderTraversalNonRecursiveAbove(Root, action);
        }

        private void InOrderTraversalRecursiveAbove(Node node, Action<int> action)
        {
            if (node != null)
            {
                Console.Write(node.Value);
                InOrderTraversalRecursive(node.Left, action);
                InOrderTraversalRecursive(node.Right, action);
            }
        }

        private void InOrderTraversalNonRecursiveAbove(Node? node, Action<int> action)
        {
            Queue<Node> qNodes = new Queue<Node>();
            if (node != null)
            {
                qNodes.Enqueue(node);
            }


            while (qNodes.Count > 0)
            {
                Node current = qNodes.Dequeue();
                if (current.Left != null)
                {
                    qNodes.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    qNodes.Enqueue(current.Right);
                }

                action(current.Value);

            }
        }

        // Вывод дерева в консоль

        public void Print()
        {
            PrintRecursive(Root, "", true);
        }
        private void PrintRecursive(Node node, string indent, bool last)
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
                Console.WriteLine($"{node.Value}({node.Index - 1})");
                PrintRecursive(node.Left, indent, false);
                PrintRecursive(node.Right, indent, true);

            }
        }

    }
}
