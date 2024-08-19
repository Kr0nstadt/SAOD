using ISDP_and_SDP;

class Program
{
    static void Main(string[] args)
    {
        // Случайное дерево поиска
        RandomBinaryTree randomTree = new RandomBinaryTree();

        Random rand = new Random();
        for (int i = 0; i < 10; i++)
        {
            randomTree.Add(rand.Next(10, 100));
        }

        Console.WriteLine("Случайное дерево поиска:");
        randomTree.PrintTree();
        Console.WriteLine($"Средняя высота: {randomTree.AverageHeight()}");
        Console.WriteLine($"Является ли деревом поиска: {randomTree.IsBinarySearchTree()}");

        // Поиск элемента
        Console.WriteLine($"Поиск элемента по ключу 50: {randomTree.Search(50)}");

        // Идеально сбалансированное дерево поиска
        BalancedBinaryTree balancedTree = new BalancedBinaryTree();
        int[] values = new int[7];
        for (int i = 0; i < values.Length; i++)
        {
           values[i] = rand.Next(10, 100);
        }

        balancedTree.Add(values);

        Console.WriteLine("\nИдеально сбалансированное дерево:");
        balancedTree.PrintTree();
        Console.WriteLine($"Средняя высота: {balancedTree.AverageHeight()}");
        Console.WriteLine($"Является ли деревом поиска: {balancedTree.IsBinarySearchTree()}");

        // Поиск элемента
        Console.WriteLine($"Поиск элемента по ключу 30: {balancedTree.Search(30)}");
        Console.WriteLine("\n");

        Console.WriteLine("   N   | Средняя высота СДП | Средняя высота ИСДП ");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"  10   |\t{GetHeightRandom(10)}\t    |\t{GetHeightBalance(10)}  ");
        Console.WriteLine($"  50   |\t{GetHeightRandom(50)}\t    |\t{GetHeightBalance(50)}  ");
        Console.WriteLine($"  100  |\t{GetHeightRandom(100)}\t    |\t{GetHeightBalance(100)}  ");
        Console.WriteLine($"  200  |\t{GetHeightRandom(200)}\t    |\t{GetHeightBalance(200)}  ");
        Console.WriteLine($"  400  |\t{GetHeightRandom(400)}\t    |\t{GetHeightBalance(400)}  ");

        Console.WriteLine("\n\nСравнение трудоемкостей поиска на 300 узлах\n");
        Console.WriteLine("  Нашло ? |\tСДП\t|\tИСДТ\t|   Высота СДП  |\tВысота ИСДТ");
        Console.WriteLine("---------------------------------------------------------------------------");

        RandomBinaryTree  rnTrue = new RandomBinaryTree();
        rnTrue.Add(GenerateArray(300));
        BalancedBinaryTree bTrue = new BalancedBinaryTree();
        bTrue.Add(GenerateArray(300));
        bTrue.Search(45);
       
        Console.WriteLine($"   {rnTrue.Search(45)}   |\t{rnTrue.SearchCount}\t|\t {bTrue.SearchCount}\t|\t{rnTrue.Height(rnTrue.Root)}\t|\t{bTrue.Height(bTrue.Root)}");
        RandomBinaryTree rnFalse = new RandomBinaryTree();
        rnFalse.Add(GenerateArray(300));
        BalancedBinaryTree bFalse = new BalancedBinaryTree();
        bFalse.Add(GenerateArray(300));
        bFalse.Search(-1);

        Console.WriteLine($"   {rnFalse.Search(-1)}  |\t{rnFalse.SearchCount}\t|\t {bFalse.SearchCount}\t|\t{rnTrue.Height(rnFalse.Root)}\t|\t{bFalse.Height(bTrue.Root)}");
    }
    static double GetHeightBalance(int n)
    {
        BalancedBinaryTree balancedBinaryTree = new BalancedBinaryTree();
        balancedBinaryTree.Add(GenerateArray(n));
        return balancedBinaryTree.AverageHeight();
    }
    static double GetHeightRandom(int n)
    {
        RandomBinaryTree randomBinaryTree = new RandomBinaryTree();
        randomBinaryTree.Add(GenerateArray(n));
        return randomBinaryTree.AverageHeight();
    }
    static int[] GenerateArray(int n)
    {
        int[] array = new int[n];
        Random random = new Random();
        for(int i = 0;i < n; i++)
        {
            array[i] = random.Next(10, 99);
        }
        return array;
    }
}