using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SAOD
{
    internal class Struct
    {
        private static string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        private string tread = Path.Combine(directoryPath, "testBase4.dat");
        
        public Struct()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] name = new byte[32];
            byte[] strit = new byte[18];
            short namhome = 0;
            short namapart = 0;
            byte[] date = new byte[10];
            Elements = new List<Element>();
            using (FileStream fs = new FileStream(tread, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (fs.Read(name) != 0)
                    {
                        int count = fs.Read(strit);
                        namhome = br.ReadInt16();
                        namapart = br.ReadInt16();
                        count = fs.Read(date);
                        Name = Encoding.Unicode.GetString(Encoding.Convert(Encoding.GetEncoding(866), Encoding.Unicode, name));
                        Strit = Encoding.Unicode.GetString(Encoding.Convert(Encoding.GetEncoding(866), Encoding.Unicode, strit));
                        NamHome = namhome;
                        NamApart = namapart;
                        Date = Encoding.Unicode.GetString(Encoding.Convert(Encoding.GetEncoding(866), Encoding.Unicode, date));
                        Element element = new Element(Name, Strit, NamHome, NamApart, Date);
                        Elements.Add(element);
                    }
                }
            }
            
        }
        public List<Element> BinarySearch(string key)
        {
            List<Element> indices = new List<Element>();
            Struct str = new Struct();
            Sort sortbd = new Sort(str);
            int left = 0;
            int right = sortbd.str.Elements.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (Less(sortbd.str.Elements[mid].Date, key) == 1)
                {
                    left = mid + 1;
                }
                else if (Less(sortbd.str.Elements[mid].Date, key) == -1)
                {
                    right = mid - 1;
                }
                else
                {
                    indices.Add(sortbd.str.Elements[mid]);
                    int temp = mid - 1;
                    while (temp >= 0 && Less(sortbd.str.Elements[temp].Date, key) == 0)
                    {
                        indices.Add(sortbd.str.Elements[temp]);
                        temp--;
                    }

                    temp = mid + 1;
                    while (temp < sortbd.str.Elements.Count && Less(sortbd.str.Elements[temp].Date, key) == 0)
                    {
                        indices.Add(sortbd.str.Elements[temp]);
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
        public void Page(int n)
        {
            Console.WriteLine("               ФИО               |       Улица       | Номер дома | Номер квартиры |    Дата    ");
            for(int i = n; i < n + 20; i++)
            {
                Console.Write($"{(i+1) ,-3} {Elements[i].ToString()}");
            }
        }
        public override string ToString()
        {
            string txt = "                 ФИО               |       Улица       | Номер дома | Номер квартиры |    Дата    \n";
            int i = 1;
            foreach (Element element in Elements)
            {
                txt += i.ToString() + " " + element.ToString();
                i++;
            }
            return txt;
        }
        public List<Element> Elements;
        private string Name;
        private string Strit;
        private int NamHome;
        private int NamApart;
        private string Date;
    }
}
