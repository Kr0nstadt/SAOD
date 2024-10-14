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
            values[i] = i;
        }
        balancedTree.Add(values);

        Console.WriteLine("\n\t\t\tLab2.Идеально сбалансированное дерево:\nИДС на 100 элементов :");
        balancedTree.PrintTree();
        balancedTree.InOrderTraversalLeft(node => Console.Write($"{node.Value}({node.Index}) "));
        Console.WriteLine();
        Console.WriteLine($"Средняя высота: {balancedTree.AverageHeight():F2}");
        Console.WriteLine($"Высота: {balancedTree.Height()}");
        Console.WriteLine($"Теоритическая высота :{Math.Log2(values.Length + 1)}");
        Console.WriteLine($"Размер: {balancedTree.Size()}");
        Console.WriteLine($"Контрольная сумма: {balancedTree.Checksum()}");
        Console.WriteLine($"Является ли деревом поиска: {balancedTree.IsBinarySearchTree()}");
        // Поиск элемента
        Console.WriteLine($"Поиск элемента по ключу 30: {balancedTree.Search(30)}");
        Console.WriteLine("\n");

        //Console.WriteLine("   N   | Средняя высота СДП | Средняя высота ИСДП ");
        //Console.WriteLine("--------------------------------------------------");
        //Console.WriteLine($"  10   |\t{GetHeightRandom(10):F2}\t    |\t{GetHeightBalance(10)}  ");
        //Console.WriteLine($"  50   |\t{GetHeightRandom(50):F2}\t    |\t{GetHeightBalance(50)}  ");
        //Console.WriteLine($"  100  |\t{GetHeightRandom(100):F2}\t    |\t{GetHeightBalance(100)}  ");
        //Console.WriteLine($"  200  |\t{GetHeightRandom(200):F2}\t    |\t{GetHeightBalance(200)}  ");
        //Console.WriteLine($"  400  |\t{GetHeightRandom(400):F2}\t    |\t{GetHeightBalance(400)}  ");

        //Console.WriteLine("\n\nСравнение трудоемкостей поиска на 500 узлах, ключ 45\n");
        //Console.WriteLine("  Нашло ? |\tСДП\t|\tИСДТ\t|   Высота СДП  |\tВысота ИСДТ");
        //Console.WriteLine("---------------------------------------------------------------------------");

        //RandomBinaryTree  rnTrue = new RandomBinaryTree();
        //rnTrue.Add(GenerateArray(-500));
        //BalancedBinaryTree bTrue = new BalancedBinaryTree();
        //bTrue.Add(GenerateArray(-500));
        //bTrue.Search(45);
       
        //Console.WriteLine($"   {rnTrue.Search(45).Value}   |\t{rnTrue.SearchCount}\t|\t {bTrue.SearchCount}\t|\t{rnTrue.Height(rnTrue.Root)}\t|\t{bTrue.Height(bTrue.Root)}");
        //RandomBinaryTree rnFalse = new RandomBinaryTree();
        //rnFalse.Add(GenerateArray(500));
        //BalancedBinaryTree bFalse = new BalancedBinaryTree();
        //bFalse.Add(GenerateArray(500));
        //bFalse.Search(-1);

        //Console.WriteLine($"   {rnFalse.Search(-1).Value}  |\t{rnFalse.SearchCount}\t|\t {bFalse.SearchCount}\t|\t{rnTrue.Height(rnFalse.Root)}\t|\t{bFalse.Height(bTrue.Root)}");
        //RandomNode.Counter = 0;


        //// Случайное дерево поиска
        RandomBinaryTree randomTree = new RandomBinaryTree();
        int[] arra = new int[100]; 
        for (int i = 0; i < 100; i++)
        {
            int val = rand.Next(1,111);
            if(arra.Contains(val) == false)
            {
                arra[i] = val;
            }
            else { i--; }
        }
        for(int i  = 0; i < arra.Length; i++)
        {
            randomTree.Add(arra[i]);
        }
        RandomNode.Counter = 0;
        //Console.WriteLine("\n\n\n\t\t\tLab3.Случайное дерево поиска:");
        //Console.WriteLine("Рекурсивное дерево");
        ////randomTree.PrintTree();
        //randomTree.InOrderTraversalLeft(value => Console.Write(value + " "));
        //Console.WriteLine();
        //Console.WriteLine($"Средняя высота: {randomTree.AverageHeight():F2}");
        //Console.WriteLine($"Высота: {randomTree.Height()}");
        //Console.WriteLine($"Контрольная сумма: {randomTree.Checksum()}");
        //Console.WriteLine($"Размер: {randomTree.Size()}");
        //Console.WriteLine($"Является ли деревом поиска: {randomTree.IsBinarySearchTree()}");

        //Console.WriteLine();
        RandomBinaryTree randomTreeTwo = new RandomBinaryTree();
        RandomNode.Counter = 0;

      
        for (int i = 0; i < arra.Length; i++)
        {
            randomTreeTwo.Add(arra[i]);
        }

       // Console.WriteLine("C двойственностью дерево");
       //// randomTreeTwo.PrintTree();
       // randomTreeTwo.InOrderTraversalLeft(value => Console.Write(value + " "));
       // Console.WriteLine();
       // Console.WriteLine($"Средняя высота: {randomTreeTwo.AverageHeight()}");
       // Console.WriteLine($"Высота: {randomTreeTwo.Height()}");
       // Console.WriteLine($"Контрольная сумма: {randomTreeTwo.Checksum()}");
       // Console.WriteLine($"Размер: {randomTreeTwo.Size()}");
       // Console.WriteLine($"Является ли деревом поиска: {randomTreeTwo.IsBinarySearchTree()}");
        BalancedBinaryTree balancedBinaryTree = new BalancedBinaryTree();
        balancedBinaryTree.Add(arra);
        // Поиск элемента
        //Console.WriteLine($"Поиск элемента по ключу 50: {randomTree.Search(50)}\n\n\n");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine(" N = 100 | Размер | Контр. сумма | Высота | Средн. высота |");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine($"   ИСДП  |   {balancedBinaryTree.Size()}  | \t{balancedBinaryTree.Checksum()}\t |   {balancedBinaryTree.Height()}    | {balancedBinaryTree.AverageHeight()}");
        Console.WriteLine($"   CДП1  |   {randomTree.Size()}  | \t{randomTree.Checksum()}\t |   {randomTree.Height()}   | {randomTree.AverageHeight()}");
        Console.WriteLine($"   CДП2  |   {randomTreeTwo.Size()}  | \t{randomTreeTwo.Checksum()} \t |    {randomTreeTwo.Height()}  |  {randomTreeTwo.AverageHeight()}");



        Console.WriteLine("\n\n");
        Console.WriteLine("Лаба на удаление в СДП");
        RandomNode.Counter = 0;
        RandomBinaryTree randomBinaryTree = new RandomBinaryTree();
        int[] NewInt = new int[15];
        for (int i = 0; i < NewInt.Length; i++)
        {
            int val = rand.Next(1, 111);
            if (NewInt.Contains(val) == false)
            {
                NewInt[i] = val;
            }
            else { i--; }
        }
        for (int i = 0; i < NewInt.Length; i++)
        {
            randomBinaryTree.Add(NewInt[i]);
        }
        Console.WriteLine("Дерево до удаления :");
        randomBinaryTree.PrintTree();
        randomBinaryTree.InOrderTraversalLeft(node => Console.Write($"{node.Value}({node.Index}) "));
        Console.WriteLine();
        string flag = "";
        while (true)
        {
            flag = Console.ReadLine();
            int val = 0;

            try{val = Int32.Parse(flag); }
            catch { break;}
            if(NewInt.Contains(val) == false)
            {
                Console.WriteLine("Такого значения нет в дереве");
            }
            else
            {
                randomBinaryTree.Delete(val);
                Console.WriteLine("Удаление вершины " + val);
                randomBinaryTree.PrintTree();
                randomBinaryTree.InOrderTraversalLeft(node => Console.Write($"{node.Value}({node.Index}) "));
            }
        }






/*
        string filePath = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\(2)Построение случайного дерева поиска и идеально сбалансированного\\ISDP_and_SDP\\CCode.txt";

        // Ключевые слова языка C
        HashSet<string> keywords = new HashSet<string>
        {
            "auto", "break", "case", "char", "const", "continue", "default",
            "do", "double", "else", "enum", "extern", "float", "for",
            "goto", "if", "int", "long", "register", "return",
            "short", "signed", "sizeof", "static", "struct", "switch",
            "typedef", "union", "unsigned", "void", "volatile", "while",
            "printf" ,"include","print"// добавьте другие ключевые слова по необходимости
        };

        // Словарь для хранения количества вхождений каждого слова
        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        try
        {
            // Чтение файла
            string code = File.ReadAllText(filePath);

            // Разделение текста на слова
            var words = code.Split(new[] { ' ','_','\n', '\r', '(', ')', '{', '}', '[', ']', ';', ',', '.', '"', '\'','*','&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                // Приведение к нижнему регистру для учета регистра
                string lowerWord = word.ToLower();

                // Проверка, является ли слово ключевым словом
                if (keywords.Contains(lowerWord))
                {
                    if (wordCount.ContainsKey(lowerWord))
                    {
                        wordCount[lowerWord]++;
                    }
                    else
                    {
                        wordCount[lowerWord] = 1;
                    }
                }
            }

            // Вывод результатов
            Console.WriteLine("Ключевые слова и их частота:");
            foreach (var kvp in wordCount.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        RandomNode.Counter = 0;
        RandomBinaryTree tree = new RandomBinaryTree();
        foreach(var key in wordCount.OrderBy(key => key.Key))
        {
            tree.Add(key.Value);
        }
        tree.PrintTree();
*/
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
        bool flag = false;
        if(n < 0)
        {
            n = Math.Abs(n);
            flag = true;
        }
        int[] array = new int[n];
        Random random = new Random();
        for(int i = 0;i < n; i++)
        {
            if( i == 400 && flag )
            {
                array[i] = 45;
            }
            int val = random.Next(1,999);
            if(array.Contains(val) == false)
            {
                array[i] = val;
            }
            else { i--; }
        }
        return array;
    }
}