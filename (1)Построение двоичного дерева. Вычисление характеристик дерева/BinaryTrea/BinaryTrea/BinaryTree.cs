using System;
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
        int totalHeight = TotalHeight(Root, 1);
        int size = Size();
        return size > 0 ? (double)totalHeight / size : 0;
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
    public void InOrderTraversal(Action<int> action)
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

                Console.WriteLine(node.Value);
                PrintRecursive(node.Left, indent, false);
                PrintRecursive(node.Right, indent, true);
            }
        }

    }
}
