using System;
using System.Collections.Generic;

public class Node
{
    public int Weight { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int weight)
    {
        Weight = weight;
        Left = null;
        Right = null;
    }
}

public class OptimalSearchTree
{
    public Node Root { get; private set; }

    public void BuildA1(List<Node> nodes)
    {
        bool[] used = new bool[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
            used[i] = false;

        for (int i = 0; i < nodes.Count; i++)
        {
            int maxIndex = -1;
            int maxWeight = 0;

            for (int j = 0; j < nodes.Count; j++)
            {
                if (!used[j] && nodes[j].Weight > maxWeight)
                {
                    maxWeight = nodes[j].Weight;
                    maxIndex = j;
                }
            }

            if (maxIndex != -1)
            {
                used[maxIndex] = true;
                AddToTree(nodes[maxIndex]);
            }
        }
    }

    public void BuildA2(List<Node> nodes, int left, int right)
    {
        if (left > right) return;

        int totalWeight = 0;
        for (int i = left; i <= right; i++)
            totalWeight += nodes[i].Weight;

        int halfWeight = totalWeight / 2;
        int sum = 0;

        for (int i = left; i <= right; i++)
        {
            sum += nodes[i].Weight;
            if (sum >= halfWeight)
            {
                AddToTree(nodes[i]);
                BuildA2(nodes, left, i - 1);
                BuildA2(nodes, i + 1, right);
                break;
            }
        }
    }

    private void AddToTree(Node newNode)
    {
        if (Root == null)
        {
            Root = newNode;
            return;
        }

        AddToTreeRec(Root, newNode);
    }

    private void AddToTreeRec(Node root, Node newNode)
    {
        if (newNode.Weight < root.Weight)
        {
            if (root.Left == null)
                root.Left = newNode;
            else
                AddToTreeRec(root.Left, newNode);
        }
        else
        {
            if (root.Right == null)
                root.Right = newNode;
            else
                AddToTreeRec(root.Right, newNode);
        }
    }

    public double CalculateWeightedHeight(Node node, int depth)
    {
        if (node == null) return 0;

        double leftHeight = CalculateWeightedHeight(node.Left, depth + 1);
        double rightHeight = CalculateWeightedHeight(node.Right, depth + 1);

        return node.Weight * (depth + leftHeight + rightHeight);
    }

    public double AverageWeightedHeight()
    {
        double totalWeight = TotalWeight(Root);
        return totalWeight == 0 ? 0 : CalculateWeightedHeight(Root, -1) / totalWeight;
    }

    private double TotalWeight(Node node)
    {
        if (node == null) return 0;
        return node.Weight + TotalWeight(node.Left) + TotalWeight(node.Right);
    }

    public void PrintTree(Node node, string indent = "")
    {
        if (node != null)
        {
            PrintTree(node.Right, indent + "   ");
            Console.WriteLine(indent + node.Weight);
            PrintTree(node.Left, indent + "   ");
        }
    }

    // Метод для генерации случайных узлов
    public List<Node> GenerateRandomNodes(int count)
    {
        Random rand = new Random();
        List<Node> nodes = new List<Node>();

        for (int i = 0; i < count; i++)
        {
            // Генерируем случайный вес от 1 до 100
            int weight = rand.Next(1, 101);
            nodes.Add(new Node(weight));
        }

        return nodes;
    }
}

class Program
{
    static void Main()
    {
        int numberOfNodes = 10; // Задаем количество узлов

        OptimalSearchTree treeA1 = new OptimalSearchTree();
        List<Node> randomNodesA1 = treeA1.GenerateRandomNodes(numberOfNodes);
        treeA1.BuildA1(randomNodesA1);

        Console.WriteLine("Дерево А1:");
        treeA1.PrintTree(treeA1.Root);
        Console.WriteLine($"Средневзвешенная высота: {treeA1.AverageWeightedHeight()}");

        OptimalSearchTree treeA2 = new OptimalSearchTree();
        List<Node> randomNodesA2 = treeA2.GenerateRandomNodes(numberOfNodes);

        treeA2.BuildA2(randomNodesA2, 0, randomNodesA2.Count - 1);

        Console.WriteLine("\nДерево А2:");
        treeA2.PrintTree(treeA2.Root);
        Console.WriteLine($"Средневзвешенная высота: {treeA2.AverageWeightedHeight()}");
    }
}
