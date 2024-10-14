using AVL;

class Program
{
    static void Main(string[] args)
    {
        /*
        AVLTree avlTree = new AVLTree();
        int[] array = GenerateRandArray(100);
        avlTree.Add(array);
        Console.WriteLine("Lab_5.Построение AVL дерева :\n\nДерево на 100 элементов :\nЭлементы и их высоты");
        avlTree.Print();
        Console.WriteLine("\n\nЭлементы и нумерация");
        avlTree.PrintTreeIndex();
        Console.WriteLine("\n\nОбход слева направо : ");
        avlTree.InOrderTraversalLeft();
        Console.WriteLine("\n\nСверху вниз : ");
        avlTree.InOrderTraversalApp();
        Console.WriteLine("\n\nСнизу ввверх : ");
        avlTree.InOrderTraversalDovn();

        BalancedBinaryTree balTree = new BalancedBinaryTree();
        balTree.Add(array);


        Console.WriteLine("\n\n\n__________________________________________________________");
        Console.WriteLine(" n = 100 | Размер | Контр. сумма | Высота | Средн. высота |");
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine($"   ИСДП  |  {balTree.Size()}   |     {balTree.Checksum()}     |   {balTree.Height()}    |      {balTree.AverageHeight():F2}     |");
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine($"   АВЛ   |  {avlTree.Size()}   |     {avlTree.Checksum()}     |   {avlTree.Height()}    |      {avlTree.AverageHeight():F2}     |");
        Console.WriteLine("__________________________________________________________");
        */
        Console.WriteLine("\n\n\nLab_6.Удаление из AVL");
        const int CountExperement = 10;
        int[] CountAdd = new int[CountExperement];
        int[]CountDelete = new int[CountExperement];
        for(int i = 0; i <  CountExperement; i++)
        {
            AVLTree treeExp = new AVLTree();
            int[] arr = GenerateRandArray(500);
            treeExp.Add(arr);
            CountAdd[i] = treeExp.CountAdd;
            Console.WriteLine(CountAdd[i]);
            for(int k = 0; k < 500; k++)
            {
                treeExp.Delete(arr[k]);
            }
            CountDelete[i] = treeExp.CountDelete;
            Console.WriteLine(CountDelete[i]);
        }
        Console.WriteLine($"Проверка на {CountExperement} деревьях из 500 элементов\n" +
            $"Среднее количество поворотов при добавлении {CountAdd.Sum() / CountExperement} это вероятнось {(double)((double)((double)CountAdd.Sum() / (double)CountExperement)/500)}\n" +
            $"Среднее количество поворотов при удалении {CountDelete.Sum() / CountExperement} это вероятнось {(double)((double)((double)CountDelete.Sum() / (double)CountExperement) / 500)}");
        AVLTree tree = new AVLTree();
        int[] NewInt = GenerateRandArray(25);
        tree.Add(NewInt);
        Console.WriteLine("\n\nДерево до удаления : ");
        tree.Print();
        string flag = "";
        while (true)
        {
            flag = Console.ReadLine();
            int val = 0;

            try { val = Int32.Parse(flag); }
            catch { break; }
            if (NewInt.Contains(val) == false)
            {
                Console.WriteLine("Такого значения нет в дереве");
            }
            else
            {
                tree.Delete(val);
                Console.WriteLine("Удаление вершины " + val);
                tree.PrintTreeIndex();
                tree.InOrderTraversalLeft();
            }
        }
    }
    static int[] GenerateRandArray(int n)
    {
        int[] randArray = new int[n];
        Random rand = new Random(); 
        for(int i = 0; i < n; i++)
        {
            var val = rand.Next(1, n + 25);
            if(!randArray.Contains(val))
            {
                randArray[i] = val;
            }
            else
            {
                i--;
            }
        }
        return randArray;
    }

}