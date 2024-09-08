using ISDP_and_SDP;

class Program
{
    static void Main(string[] args)
    {
        
        // Идеально сбалансированное дерево поиска
        BalancedBinaryTree balancedTree = new BalancedBinaryTree();
        Random rand = new Random();
        int[] values = new int[100];
        for (int i = 0; i < values.Length; i++)
        {
           values[i] = rand.Next(10, 100);
        }

        balancedTree.Add(values);

        Console.WriteLine("\n\t\t\tLab2.Идеально сбалансированное дерево:\nИДС на 100 элементов :");
        balancedTree.PrintTree();
        balancedTree.InOrderTraversalLeft(value => Console.Write(value + " "));
        Console.WriteLine();
        Console.WriteLine($"Средняя высота: {balancedTree.AverageHeight()}");
        Console.WriteLine($"Высота: {balancedTree.Height()}");
        Console.WriteLine($"Теоритическая высота :{Math.Log2(values.Length + 1)}");
        Console.WriteLine($"Размер: {balancedTree.Size()}");
        Console.WriteLine($"Контрольная сумма: {balancedTree.Checksum()}");
        Console.WriteLine($"Является ли деревом поиска: {balancedTree.IsBinarySearchTree()}");
        // Поиск элемента
        Console.WriteLine($"Поиск элемента по ключу 30: {balancedTree.Search(30)}");
        Console.WriteLine("\n");

        Console.WriteLine("   N   | Средняя высота СДП | Средняя высота ИСДП ");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"  10   |\t{GetHeightRandom(10):F2}\t    |\t{GetHeightBalance(10)}  ");
        Console.WriteLine($"  50   |\t{GetHeightRandom(50):F2}\t    |\t{GetHeightBalance(50)}  ");
        Console.WriteLine($"  100  |\t{GetHeightRandom(100):F2}\t    |\t{GetHeightBalance(100)}  ");
        Console.WriteLine($"  200  |\t{GetHeightRandom(200):F2}\t    |\t{GetHeightBalance(200)}  ");
        Console.WriteLine($"  400  |\t{GetHeightRandom(400):F2}\t    |\t{GetHeightBalance(400)}  ");

        Console.WriteLine("\n\nСравнение трудоемкостей поиска на 500 узлах, ключ 45\n");
        Console.WriteLine("  Нашло ? |\tСДП\t|\tИСДТ\t|   Высота СДП  |\tВысота ИСДТ");
        Console.WriteLine("---------------------------------------------------------------------------");

        RandomBinaryTree  rnTrue = new RandomBinaryTree();
        rnTrue.Add(GenerateArray(500));
        BalancedBinaryTree bTrue = new BalancedBinaryTree();
        bTrue.Add(GenerateArray(500));
        bTrue.Search(45);
       
        Console.WriteLine($"   {rnTrue.Search(45).Value}   |\t{rnTrue.SearchCount}\t|\t {bTrue.SearchCount}\t|\t{rnTrue.Height(rnTrue.Root)}\t|\t{bTrue.Height(bTrue.Root)}");
        RandomBinaryTree rnFalse = new RandomBinaryTree();
        rnFalse.Add(GenerateArray(500));
        BalancedBinaryTree bFalse = new BalancedBinaryTree();
        bFalse.Add(GenerateArray(500));
        bFalse.Search(-1);

        Console.WriteLine($"   {rnFalse.Search(-1).Value}  |\t{rnFalse.SearchCount}\t|\t {bFalse.SearchCount}\t|\t{rnTrue.Height(rnFalse.Root)}\t|\t{bFalse.Height(bTrue.Root)}");



        // Случайное дерево поиска
        RandomBinaryTree randomTree = new RandomBinaryTree();
        
        for (int i = 0; i < 100; i++)
        {
            randomTree.Add(rand.Next(10, 100));
        }

        Console.WriteLine("\n\n\n\t\t\tLab3.Случайное дерево поиска:");
        Console.WriteLine("Рекурсивное дерево");
        randomTree.PrintTree();
        randomTree.InOrderTraversalLeft(value => Console.Write(value + " "));
        Console.WriteLine();
        Console.WriteLine($"Средняя высота: {randomTree.AverageHeight()}");
        Console.WriteLine($"Высота: {randomTree.Height()}");
        Console.WriteLine($"Контрольная сумма: {randomTree.Checksum()}");
        Console.WriteLine($"Размер: {randomTree.Size()}");
        Console.WriteLine($"Является ли деревом поиска: {randomTree.IsBinarySearchTree()}");

        Console.WriteLine();
        RandomBinaryTree randomTreeTwo = new RandomBinaryTree();

        for (int i = 0; i < 100; i++)
        {
            randomTreeTwo.AddTwo(rand.Next(10, 100));
        }

 
        Console.WriteLine("C двойственностью дерево");
        randomTreeTwo.PrintTree();
        randomTreeTwo.InOrderTraversalLeft(value => Console.Write(value + " "));
        Console.WriteLine();
        Console.WriteLine($"Средняя высота: {randomTreeTwo.AverageHeight()}");
        Console.WriteLine($"Высота: {randomTreeTwo.Height()}");
        Console.WriteLine($"Контрольная сумма: {randomTreeTwo.Checksum()}");
        Console.WriteLine($"Размер: {randomTreeTwo.Size()}");
        Console.WriteLine($"Является ли деревом поиска: {randomTreeTwo.IsBinarySearchTree()}");

        // Поиск элемента
        Console.WriteLine($"Поиск элемента по ключу 50: {randomTree.Search(50)}\n\n\n");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine(" N = 100 | Размер | Контр. сумма | Высота | Средн. высота |");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine($"   ИСДП  |   {GetSizeBalance(100).Size()}  | \t{GetSizeBalance(100).Checksum()}\t |   {GetSizeBalance(100).Height()}    | {GetSizeBalance(100).AverageHeight()}");
        Console.WriteLine($"   CДП1  |   {GetRandomRek(100).Size()}  | \t{GetRandomRek(100).Checksum()}\t |   {GetRandomRek(100).Height()}   | {GetRandomRek(100).AverageHeight()}");
        Console.WriteLine($"   CДП2  |   {GetRandomTwo(100).Size()}  | \t{GetRandomTwo(100).Checksum()}\t |   {GetRandomTwo(100).Height()}   | {GetRandomTwo(100).AverageHeight()}");
    }
    static RandomBinaryTree GetRandomTwo(int n)
    {
        RandomBinaryTree randomBinaryTree = new RandomBinaryTree();
        randomBinaryTree.AddTwo(GenerateArray(n));
        return randomBinaryTree;
    }
    static RandomBinaryTree GetRandomRek(int n)
    {
        RandomBinaryTree randomBinaryTree = new RandomBinaryTree();
        randomBinaryTree.Add(GenerateArray(n));
        return randomBinaryTree;
    }
    static BalancedBinaryTree GetSizeBalance(int n)
    {
        BalancedBinaryTree balancedBinaryTree = new BalancedBinaryTree();
        balancedBinaryTree.Add(GenerateArray(n));
       return balancedBinaryTree;
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