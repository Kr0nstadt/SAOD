using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karp
{
    internal class Perebor
    {
        public List<int> Index => _index;
        public int C => _c;
        public Perebor(string key)
        {
            _text = _workText.Text;
            _key = key;
            _index = new List<int>(0);
            GetIndex();
        }
        private void GetIndex()
        {
            int n = _text.Length;
            int m = _key.Length;
            for (int i = 0; i<=n - m; i++)
            {
               int j;
               for( j = 0; j < m; j++)
               {
                    _c++;
                    if (_text[i + j] != _key[j])
                    {
                        break;
                    }
               }
               if(j == m)
               {
                    _index.Add(i);  
               }
            }
        }
        private int _c = 0;
        private string _key;
        private List<int> _index;
        private string _text;
        private static WorkText _workText = new WorkText();
    }
}
