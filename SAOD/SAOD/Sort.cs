using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SAOD
{
    internal class Sort
    {
        public Struct str;
        public Sort(Struct str)
        {
            ViliamFloid(str.Elements);
            ReversList(str.Elements);
            this.str = str;
        }
        private void ViliamFloid(List<Element> Elements)
        {
            int L, R;
            int n = Elements.Count;
            L = (int)((n - 1) / 2);
            while (L >= 0)
            {
                BildHeap(Elements, L, n - 1);
                L--;
            }
            R = n - 1;
            while (R > 0)
            {
                Element temp = Elements[0];
                Elements[0] = Elements[R];
                Elements[R] = temp;
                R--;
                BildHeap(Elements, 0, R);
            }
        }
        private void BildHeap(List<Element> Data, int L, int R)
        {
            Element x = Data[L];
            int i = L;
            while (true)
            {
                int j = 2 * i;
                if (j > R)
                {
                    break;
                }

                if ((j < R) && (Less(Data[j + 1] , Data[j])== 1))
                {
                    j += 1;
                }

                if (Less(x , Data[j]) == 1)
                {
                    break;
                }

                Data[i] = Data[j];
                i = j;
                Data[i] = x;
            }
        }
        public List<Element> BinarySearch(string key)
        {
            List<Element> indices = new List<Element>();
            int left = 0;
            int right = str.Elements.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (Less(str.Elements[mid].Date, key) == 1)
                {
                    left = mid + 1;
                }
                else if (Less(str.Elements[mid].Date, key) == -1)
                {
                    right = mid - 1;
                }
                else
                {
                    indices.Add(str.Elements[mid]);
                    int temp = mid - 1;
                    while (temp >= 0 && Less(str.Elements[temp].Date, key) == 0)
                    {
                        indices.Add(str.Elements[temp]);
                        temp--;
                    }

                    temp = mid + 1;
                    while (temp < str.Elements.Count && Less(str.Elements[temp].Date, key) == 0)
                    {
                        indices.Add(str.Elements[temp]);
                        temp++;
                    }

                    break;
                }
            }
            return indices;
        }
        private int Less(string one, string two)
        {
            string[] strone = one.Split('-', StringSplitOptions.RemoveEmptyEntries);
            string[] strtwo = two.Split("-", StringSplitOptions.RemoveEmptyEntries);
            int[] oneArray = new int[3];
            int[] twoArray = new int[3];
            for (int j = 0; j < 3; j++)
            {
                oneArray[j] = int.Parse(strone[j]);
                twoArray[j] = int.Parse(strtwo[j]);
            }

            if (oneArray[2] > twoArray[2]) { return -1; }
            if (oneArray[2] < twoArray[2]) { return 1; }
            if (oneArray[2] == twoArray[2])
            {
                if (oneArray[1] > twoArray[1]) { return -1; }
                if (oneArray[1] < twoArray[1]) { return 1; }
                if (oneArray[1] == twoArray[1])
                {
                    if (oneArray[0] > twoArray[0]) { return -1; }
                    if (oneArray[0] < twoArray[0]) { return 1; }
                    if (oneArray[0] == oneArray[0])
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }
        private int Less(Element one, Element two)
        {
            string[] strone = one.Date.Split('-',StringSplitOptions.RemoveEmptyEntries);
            string[] strtwo = two.Date.Split("-",StringSplitOptions.RemoveEmptyEntries);
            int[] oneArray = new int[3];
            int[] twoArray = new int[3];
            for (int j = 0; j < 3; j++)
            {
                oneArray[j] = int.Parse(strone[j]);
                twoArray[j] = int.Parse(strtwo[j]);
            }

            if (oneArray[2] > twoArray[2]) { return -1; }
            if (oneArray[2] < twoArray[2]) { return 1; }
            if (oneArray[2] == twoArray[2])
            {
                if (oneArray[1]> twoArray[1]) { return -1; }
                if (oneArray[1]< twoArray[1]) { return 1; }
                if (oneArray[1] == twoArray[1])
                {
                    if (oneArray[0]> twoArray[0]) { return -1; }
                    if (oneArray[0] < twoArray[0]) { return 1; }
                    if (oneArray[0] == oneArray[0])
                    {
                        if (one.Strit[0] > one.Strit[0]) { return -1; }
                        if (one.Strit[0] < one.Strit[0]) { return 1; }
                        else { return 1; }
                    }
                }
            }
            return 1;
        }
        public void Page(int n)
        {
            Console.WriteLine("               ФИО               |       Улица       | Номер дома | Номер квартиры |    Дата    ");
            for (int i = n; i < n + 20; i++)
            {
                Console.Write($"{(i + 1),-3} {str.Elements[i].ToString()}");
            }
        }
        private void ReversList(List<Element> list)
        {
            for(int i = 0; i < list.Count/2; i++)
            {
                Element e = list[list.Count -1-i];
                list[list.Count-1-i] = list[i];
                list[i] = e;
            }
        }
    }
}
