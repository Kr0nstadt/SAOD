using BinaryTrea;

class MainClass
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();
        
        Random random = new Random();
        int[] array = new int[7];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(1, 10);
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
        tree.Root = new Node(array[0]);
        tree.Root.Left = new Node(array[1]);
        tree.Root.Left.Right = new Node(array[2]);
        tree.Root.Left.Left = new Node(array[3]);
        tree.Root.Left.Left.Right = new Node(array[4]);
        tree.Root.Left.Left.Right.Left = new Node(array[5]);

        Console.WriteLine("Размер дерева: " + tree.Size());
        Console.WriteLine("Высота дерева: " + tree.Height());
        Console.WriteLine("Средняя высота дерева: " + tree.AverageHeight());
        Console.WriteLine("Контрольная сумма дерева: " + tree.Checksum());

        Console.WriteLine("Обход дерева слева направо:");
        tree.InOrderTraversal(value => Console.Write(value + " "));

        Console.WriteLine("\nВывод дерева:");
        tree.Print();
    }
}