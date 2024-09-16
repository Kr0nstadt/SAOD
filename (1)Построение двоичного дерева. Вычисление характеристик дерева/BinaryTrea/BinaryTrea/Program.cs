using BinaryTrea;

class MainClass
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();
        
        Random random = new Random();
        int[] array = new int[6];
        for (int i = 0; i < array.Length; i++)
        {
            int g = random.Next(1,20);
            if(true)
            {
                array[i] = i + 1;
            }
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
        tree.Root = new Node(array[0]);
        tree.Root.Left = new Node(array[1]);
        tree.Root.Left.Right = new Node(array[3]);
        tree.Root.Left.Left = new Node(array[2]);
        tree.Root.Left.Left.Right = new Node(array[4]);
        tree.Root.Left.Left.Right.Left = new Node(array[5]);

        Console.WriteLine("Размер дерева: " + tree.Size());
        Console.WriteLine("Высота дерева: " + tree.Height());
        Console.WriteLine("Средняя высота дерева: " + (double)tree.AverageHeight());
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