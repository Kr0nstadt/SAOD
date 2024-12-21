using AVL;

class Program
{
    static void Main(string[] args)
    {
        BTree btree = new BTree();
        int[] array = GenerateRandArray(100);
        Console.WriteLine("Lab_7. Двоичное Б-дерево.\nДвоичное Б-дерево из 100 вершин :");
        foreach (int i in array)
        {
            btree.Add(i);
        }

        //Console.WriteLine("Размер дерева: " + btree.Size());
        //Console.WriteLine("Сумма всех элементов: " + btree.Sum());
        Console.WriteLine("Высота дерева: " + btree.Height());
        Console.WriteLine($"Средняя высота: {btree.AverageHeight():F2}");
        //Console.WriteLine("Количество уровней: " + btree.Levels());
        //btree.PrintTree();
        Console.WriteLine("Обход дерева с лева направо:");
        btree.InOrderTraversalLeft((int x) => Console.Write(x + " "));
        Console.WriteLine("\n\n\n");
        AVLTree avltree = new AVLTree();
        foreach(int i in array)
        {
            avltree.Add(i);
        }
        Console.WriteLine("_______________________________________________________________");
        Console.WriteLine("   n = 100   | Размер | Контр. сумма | Высота | Средн. высота |");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine($"  AVL дерево |   {avltree.Size()}  |     {avltree. Sum()}     |   {avltree.Height()}    |      {avltree.AverageHeight():F2}     |");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine($"  Б - дерево |   {btree.Size()}  |     {btree.Sum()}     |   {btree.Height()}    |      {btree.AverageHeight():F2}     |");
        Console.WriteLine("_______________________________________________________________");
       
        

       
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