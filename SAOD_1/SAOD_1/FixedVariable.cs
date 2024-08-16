using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_1
{
    internal class FixedVariable
    {
        public string Result => _result;
        public FixedVariable(int val)
        {
            if (val < 0) { _result = "0000"; }
            else
            {
                var firstPartInt = Convert.ToString(val, 2).Length;
                string firstPart = Convert.ToString(firstPartInt, 2).PadLeft(4, '0');

                string txt = Convert.ToString(val, 2);
                string secondRart = "";
                for (int i = 1; i < txt.Length; i++)
                {
                    secondRart += txt[i];
                }
                _result = firstPart + " " + secondRart;
            }
        }
        private string _result;
    }
}
