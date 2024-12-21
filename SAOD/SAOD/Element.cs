using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    public class Element 
    {
        public string Name;
        public string Strit;
        public int NamHome;
        public int NamApart;
        public string Date;
        public Element(string name, string strit, int namHome, int namApart, string date)
        {
            Name = name;
            Strit = strit;
            NamHome = namHome;
            NamApart = namApart;
            Date = date;
        }
        public override string ToString()
        {
            return $" {Name,-32} | {Strit,-18} | {NamHome,-10} | {NamApart,-14} | {Date,-10} \n";
        }
    }
}
