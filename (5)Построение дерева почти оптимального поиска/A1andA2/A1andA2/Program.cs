using System;

class Node
{
    public int Key { get; set; }
    public int Weight { get; set; }
    public bool Use { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int key, int weight)
    {
        Key = key;
        Weight = weight;
        Use = false;
        Left = null;
        Right = null;
    }
}

class OptimalBinarySearchTree
{
    private Node root;

    public OptimalBinarySearchTree()
    {
        root = null;
    }

    // Метод для добавления узла в дерево
    private void AddNode(Node newNode)
    {
        if (root == null)
        {
            root = newNode;
        }
        else
        {
            AddNodeRec(root, newNode);
        }
    }

    private void AddNodeRec(Node current, Node newNode)
    {
        if (newNode.Key < current.Key)
        {
            if (current.Left == null)
            {
                current.Left = newNode;
            }
            else
            {
                AddNodeRec(current.Left, newNode);
            }
        }
        else
        {
            if (current.Right == null)
            {
                current.Right = newNode;
            }
            else
            {
                AddNodeRec(current.Right, newNode);
            }
        }
    }

    // Метод для построения дерева
    public void BuildTree(Node[] nodes)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Use = false; // Инициализируем use как ЛОЖЬ
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            int maxIndex = -1;
            int maxWeight = 0;

            for (int j = 0; j < nodes.Length; j++)
            {
                if (!nodes[j].Use && nodes[j].Weight > maxWeight)
                {
                    maxWeight = nodes[j].Weight;
                    maxIndex = j;
                }
            }

            if (maxIndex != -1)
            {
                nodes[maxIndex].Use = true; // Устанавливаем use как ИСТИНА
                AddNode(nodes[maxIndex]); // Добавляем в дерево
            }
        }
    }

    // Метод для генерации дерева с заданным количеством случайных узлов
    public static Node[] GenerateRandomNodes(int count)
    {
        Random rand = new Random();
        Node[] nodes = new Node[count];

        for (int i = 0; i < count; i++)
        {
            int key = rand.Next(1, 100); // Генерация случайного ключа
            int weight = rand.Next(1, 10); // Генерация случайного веса
            nodes[i] = new Node(key, weight);
        }

        return nodes;
    }

    // Метод для вывода дерева (для проверки)
    public void PrintTree(Node node, string indent = "")
    {
        if (node != null)
        {
            PrintTree(node.Right, indent + "   ");
            Console.WriteLine(indent + node.Key + " (Weight: " + node.Weight + ")");
            PrintTree(node.Left, indent + "   ");
        }
    }

    public Node GetRoot()
    {
        return root;
    }
}

// Пример использования
class Program
{
    static void Main()
    {
        int numberOfNodes = 10; // Задайте количество узлов
        Node[] randomNodes = OptimalBinarySearchTree.GenerateRandomNodes(numberOfNodes);

        OptimalBinarySearchTree obst = new OptimalBinarySearchTree();
        obst.BuildTree(randomNodes);

        Console.WriteLine("Дерево после построения:");
        obst.PrintTree(obst.GetRoot());
    }
}
