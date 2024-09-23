using BinaryTrea;

class MainClass
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();

        Console.WriteLine();
        tree.Root = new Node(1);
        tree.Root.Left = new Node(2);
        tree.Root.Left.Left = new Node(3);
        tree.Root.Left.Right = new Node(4);
        tree.Root.Left.Left.Right = new Node(5);
        tree.Root.Left.Left.Right.Left = new Node(6);

        Console.WriteLine("Размер дерева: " + tree.Size());
        Console.WriteLine("Высота дерева: " + tree.Height());
        Console.WriteLine($"Средняя высота дерева: {(double)tree.AverageHeight():F2}") ;
        Console.WriteLine("Контрольная сумма дерева: " + tree.Checksum()+"\n");

        Console.WriteLine("Обход дерева слева направо:");
        tree.InOrderTraversalLeft(value => Console.Write(value + " "));
        Console.WriteLine();
        Console.WriteLine("Обход дерева сверху вниз:");
        tree.InOrderTraversalAbove(value => Console.Write(value + " "));
        Console.WriteLine();
        Console.WriteLine("Обход дерева снизу вверх:");
        tree.InOrderTraversalBelow(value => Console.Write(value + " "));

        Console.WriteLine("\nВывод дерева:");
        tree.Print();
    }
}