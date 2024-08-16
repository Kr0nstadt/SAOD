using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_1
{
    internal class OmegaElias
    {
        public OmegaElias(int val)
        {
            _result = Encode(val);
        }
        public string Result => _result;
        private string Encode(int val)
        {
            if (val < 0) { return "null"; }
            if(val == 1) { return "0"; }
            if(val == 2 || val == 3) { return Convert.ToString(val, 2) + "0";}
            string res = "";
            string firstPart = Convert.ToString(Convert.ToString(val, 2).Length - 1,2);
            if(firstPart.Length > 2)
            {
                res = "10" + firstPart + Convert.ToString(val, 2) + "0";
            }
            else
            {
                res = firstPart + Convert.ToString(val, 2) + "0";
            }
            return res;
        }
        private string _result;
    }
}
