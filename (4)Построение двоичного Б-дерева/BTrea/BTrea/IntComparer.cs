using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrea
{
    public class IntComparer : Comparer<Int>
    {
        public override int Compare(Int x, Int y)
        {
            return x.CompareTo(y);
        }
    }
}
