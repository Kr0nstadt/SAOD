using BTreaTwo;

public class Program
{
    const int Pow = 3;
    public static void Main()
    {
        BTree bTree = new BTree(Pow); // Создаем B-дерево с минимальной степенью 3

        // Генерация дерева с рандомными элементами
        bTree.GenerateRandom(99, 1, 100);

        // Выводим дерево
        Console.WriteLine("B-дерево:");
        bTree.Print();

        // Вычисляем высоту дерева
        Console.WriteLine($"Высота дерева: {bTree.Height()}");
        /*
        Console.WriteLine("  N  | Высота | Теоретическая");
        Console.WriteLine("------------------------------");
        Console.WriteLine($" 10  | {HigthTree(10)} | {HigthTeor(10)}");
        Console.WriteLine($" 50  | {HigthTree(50)} | {HigthTeor(50)}");
        Console.WriteLine($" 100 | {HigthTree(100)} | {HigthTeor(100)}");
        Console.WriteLine($" 200 | {HigthTree(200)} | {HigthTeor(200)}");
        Console.WriteLine($" 400 | {HigthTree(400)} | {HigthTeor(400)}");
        */
    }
    public static int HigthTree(int n)
    {
        BTree bTree = new BTree(Pow);
        bTree.GenerateRandom(n, 1, 100);
        return bTree.Height();
    }
    public static double HigthTeor(int n)
    {
        return ((Math.Log2(n + 1) - 1) / (Math.Log2(Pow + 1))) + 1;
    }
}