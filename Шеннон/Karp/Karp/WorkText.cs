using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karp
{
    internal class WorkText
    {
        public string Text => _text;
        public WorkText()
        {
            string part = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\Шеннон\\Karp\\Karp\\TextFile.txt";
            _text = File.ReadAllText(part);
        }
        private string _text;
    }
}
