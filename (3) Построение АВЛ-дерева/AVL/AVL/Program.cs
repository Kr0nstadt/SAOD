using AVL;

class Program
{
    static void Main(string[] args)
    {
        AVLTree avlTree = new AVLTree();

        avlTree.Add(10);
        avlTree.Add(20);
        avlTree.Add(30);
        avlTree.Add(40);
        avlTree.Add(50);
        avlTree.Add(25);

        Console.WriteLine("AVL Tree:");
        avlTree.Print();

        Console.WriteLine($"Высота дерева: {avlTree.Height()}");
        Console.WriteLine($"Средняя высота: {avlTree.AverageHeight():F2}");
        Console.WriteLine($"Количество поворотов: {avlTree.RotationCount}");

        Console.WriteLine("  N  | Высота дерева | Средняя высота | Теоритическая");
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine($" 10  |     {NHigth(10):F2}      |      {NAgHigth(10):F2}      | {Theor(10):F2}");
        Console.WriteLine($" 50  |     {NHigth(50):F2}      |      {NAgHigth(50):F2}      | {Theor(50):F2}");
        Console.WriteLine($" 100 |     {NHigth(100):F2}      |      {NAgHigth(100):F2}      | {Theor(100):F2}");
        Console.WriteLine($" 200 |     {NHigth(200):F2}      |      {NAgHigth(200):F2}      | {Theor(200):F2}");
        Console.WriteLine($" 400 |     {NHigth(400):F2}      |      {NAgHigth(400):F2}      | {Theor(400):F2}");
        Console.WriteLine("\n");
        Console.WriteLine("  N  | Количество поворотов при построении ");
        Console.WriteLine("-------------------------------");
        Console.WriteLine($" 10  | {PopaMtravia(10)}");
        Console.WriteLine($" 50  | {PopaMtravia(50)}");
        Console.WriteLine($" 100  | {PopaMtravia(100)}");
        Console.WriteLine($" 200  | {PopaMtravia(200)}");
        Console.WriteLine($" 400  | {PopaMtravia(400)}");
    }
    public static int PopaMtravia(int n)
    {
        AVLTree aVLTree = new AVLTree();
        int bef = aVLTree.RotationCount;
        aVLTree.GenerateRandomTree(n);
        int aft = aVLTree.RotationCount;
        return aft - bef;
    }
    public static int NHigth(int n)
    {
        AVLTree aVLTree = new AVLTree();
        aVLTree.GenerateRandomTree(n);
        return aVLTree.Height();
    }
    public static double NAgHigth(int n)
    {
        AVLTree aVLTree1 = new AVLTree();
        aVLTree1.GenerateRandomTree(n);
        return aVLTree1.AverageHeight();
    }
    public static double Theor(int n)
    {
        return 1.44 * Math.Log2(n + 2) - 0.328;
    }
}