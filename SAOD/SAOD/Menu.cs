using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SAOD.Solution;

namespace SAOD
{
    internal class Menu
    {
        private Struct str = new Struct();
        private Sort sort;
        private List<Element> SearchResSort;
        public Menu()
        {
            bool exit = true;
            sort = new Sort(new Struct());

            while (exit)
            {
                int ResMainMenu = MainMenu();
                switch (ResMainMenu)
                {
                    case 1:
                        Console.WriteLine("База данных 4  до сортировки");
                        BaseMenu();
                        break;
                    case 2:
                        Console.WriteLine("База данных 4 после сортровки Вильяма - Флойда(пирамидальная)");
                        SortNenu();
                        break;
                    case 3:
                        Console.WriteLine("Двоичное Б - дерево построено по ключу номеру квартиры");
                        TreeMenu();
                        break;
                    case 4:
                        Console.WriteLine("Кодировка Хаффмана ");
                        CodeMenu();
                        break;
                    default:
                        exit = false;
                        break;
                }
            }
        }
        private void CodeMenu()

        {
            bool exit = true;
            int flag = 0;
            while (exit)
            {
                Console.WriteLine("Выберите команду :\n" +
                    "1) B главное меню");
                Haffmen haffmen = new Haffmen();
                foreach(var item in haffmen.Alfavit)
                {
                    Console.WriteLine($"{item.Key,-3} | {item.Value}");
                }
                Console.WriteLine($"-----------------------------------------------------------------------------" +
                    $"\nСредняя длина кодового слова : {haffmen.srLength}\n" +
                    $"Энтопия дискретного источника : {haffmen.entopia}\n" +
                    $"Неравенство крафта : {haffmen.kraft} <=  1\n" +
                    $"Избыточность : {Math.Abs(haffmen.entopia - haffmen.srLength)}\n" +
                    $"Количесво символов : 166");
                try
                {
                    string temp = Console.ReadLine();
                    flag = int.Parse(temp);
                }
                catch
                {
                    Console.WriteLine("Неизвестная команда");
                }
                if (flag == 1)
                {
                    exit = false;
                }
            }
        }
        private void TreeMenu()
        {
            bool exit = true;
            int flag = 0;
            int n = 0;
            BTree tree = new BTree();
            Dbds root = null;
            if (SearchResSort != null)
            {
                foreach (var item in SearchResSort)
                {
                    tree.Insert(item, ref root);
                }
                tree.InOrderTraversal(root);
                while (exit)
                {
                    Console.WriteLine("Выберите команду :\n" +
                        "1) Поиск в дереве;\n" +
                        "2) В главное меню;");
                    try
                    {
                        string temp = Console.ReadLine();
                        flag = int.Parse(temp);
                    }
                    catch
                    {
                        Console.WriteLine("Неизвестная команда");
                    }


                    if (flag == 1)
                    {
                        Console.WriteLine("Введите номер квартиры для поиска ");
                        string strkey = Console.ReadLine();
                        int key = 0;
                        try
                        {
                            key = int.Parse(strkey);
                            List<Element> TreeRes = tree.Search(key, root);
                            foreach (var item in TreeRes)
                            {
                                Console.Write(item.ToString());
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Не корректный ключ");
                        }
                    }

                    if (flag == 2)
                    {
                        exit = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Нужно сделать поиск в отсортированной базе");
            }
        }
        private void SortNenu()
        {
            bool exit = true;
            int flag = 0;
            int n = 0;
            sort.Page(n);
            while (exit)
            {
                Console.WriteLine("Выберите команду :\n" +
                    "1) Следующая страница;\n" +
                    "2) Предыдущая страница;\n" +
                    "3) Вся база\n" +
                    "4) Поиск\n" +
                    "5) В главное меню;");
                try
                {
                    string temp = Console.ReadLine();
                    flag = int.Parse(temp);
                }
                catch
                {
                    Console.WriteLine("Неизвестная команда");
                }
                if (flag == 1)
                {
                    if (n != str.Elements.Count)
                    {
                        sort.Page(n += 20);
                    }
                    else
                    {
                        Console.WriteLine("Это последняя страница ");
                    }
                }
                if (flag == 2)
                {
                    if (n >= 20)
                    {
                        sort.Page(n -= 20);
                    }
                    else
                    {
                        Console.WriteLine("Это первая страница");
                    }
                }
                if (flag == 3)
                {
                    Console.WriteLine(sort);
                }
                if (flag == 4)
                {
                    Console.WriteLine("Введите дату для поиска записи");
                    string key = Console.ReadLine();
                    List<Element> SearchRes = sort.BinarySearch(key);
                    SearchResSort = SearchRes;
                    if (SearchRes.Count > 0)
                    {
                        foreach (Element e in SearchRes)
                        {
                            Console.Write(e.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Записи с такой датой не существует в базе данных");
                    }
                }
                if (flag == 5)
                {
                    exit = false;
                }
            }
        }
        private void BaseMenu()
        {
            bool exit = true;
            int flag = 0;
            int n = 0;
            Struct obj = new Struct();
            str.Page(n);
            while (exit)
            {
                Console.WriteLine("Выберите команду :\n" +
                    "1) Следующая страница;\n" +
                    "2) Предыдущая страница;\n" +
                    "3) Вся база\n" +
                    "4) Поиск\n" +
                    "5) В главное меню;");
                try
                {
                    string temp = Console.ReadLine();
                    flag = int.Parse(temp);
                }
                catch
                {
                    Console.WriteLine("Неизвестная команда");
                }
                if (flag == 1)
                {
                    if(n != str.Elements.Count)
                    {
                        str.Page(n += 20);
                    }
                    else
                    {
                        Console.WriteLine("Это последняя страница ");
                    }
                }
                if (flag == 2)
                {
                    if(n >= 20)
                    {
                        str.Page(n -= 20);
                    }
                    else
                    {
                        Console.WriteLine("Это первая страница");
                    }
                }
                if(flag == 3)
                {
                    Console.WriteLine(str);
                }
                if(flag == 4)
                {
                    Console.WriteLine("Введите дату для поиска записи");
                    string key = Console.ReadLine();
                    List<Element> SearchRes = str.BinarySearch(key);
                    if(SearchRes.Count > 0)
                    {
                        foreach(Element e in SearchRes)
                        {
                            Console.Write(e.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Записи с такой датой не существует в базе данных");
                    }
                }
                if (flag == 5)
                {
                    exit = false;
                }
            }
        }
        private int MainMenu()
        {
            int flag = 0;
            Console.WriteLine("Выберите команду :\n" +
                    "1) База данных до сортировки;\n" +
                    "2) База данных после сортировки;\n" +
                    "3) Дерево;\n" +
                    "4) Кодировка;\n" +
                    "5) Завершение работы программы;");
            bool exit = true;
            while (exit)
            {
                try
                {
                    string temp = Console.ReadLine();
                    flag = int.Parse(temp);
                }
                catch
                {
                    Console.WriteLine("Неизвестная команда");
                }
                if(flag == 1 ||
                    flag ==  2 ||
                    flag == 3 ||
                    flag == 4 ||
                    flag == 5)
                {
                    exit = false;
                }
            }
            return flag;
        }
    }
}
