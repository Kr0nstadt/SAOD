using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD.Solution
{
    internal class BTree
    {
        private List<Element> elements = new List<Element>();
        public void Insert(Element el,ref Dbds node)
        {
            elements.Add(el);
        }
        public void InOrderTraversal(Dbds node)
        {
            elements.Sort((x, y)=> y.NamApart.CompareTo(x.NamApart));
            foreach (Element el in elements)
            {
                Console.Write(el.ToString());
            }
        }
        public List<Element> Search (int key, Dbds node)
        {
            List<Element> list = new List<Element>();
            foreach (Element el in elements)
            {
                if(el.NamApart == key)
                {
                    list.Add(el);
                }
            }
            return list;
        }
    }
}
