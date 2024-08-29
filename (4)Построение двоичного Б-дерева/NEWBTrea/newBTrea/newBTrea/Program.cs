
using System;

namespace newBTree
{
    class MainClass
    {
        const int Degree = 2;
        static void Main()
        {
            const int Degree = 3;
            Console.WriteLine(MakeHeigth(400));
        }
        static int MakeHeigth(int n)
        {
            var tree = new BTree<int, int>(Degree);
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                tree.Insert(rnd.Next(10, 99), i);
            }
            return tree.Height;
        }
    }
}