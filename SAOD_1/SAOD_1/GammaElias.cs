using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_1
{
    internal class GammaElias
    {
        public GammaElias(int val)
        {
            _result = Encode(val);
        }
        public string Result => _result;
        private  string Encode(int number)
        {
            
            if (number <= 0) { return "null"; }
                
            string binary = Convert.ToString(number, 2);
            int length = binary.Length;
            StringBuilder prefix = new StringBuilder();
            for (int i = 0; i < length - 1; i++)
            {
                prefix.Append('0');
            }
            return prefix.ToString() + binary;
        }
        private string _result;
    }
}
