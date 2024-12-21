using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karp
{
    class RabinKarpSearch
    {
        private const int d = 256; // Количество символов в алфавите
        private const int q = 101; // Простое число для хеширования
        private int _c = 0;
        public int C => _c;
        public  List<int> RabinKarp( string pattern)
        {
            _c = 0;
            WorkText v = new WorkText();
            string text = v.Text;
            List<int> indices = new List<int>();
            int n = text.Length;
            int m = pattern.Length;
            int p = 0;
            int t = 0; 
            int h = 1;

            for (int i = 0; i < m - 1; i++)
            {
                h = (h * d) % q;
            }

            for (int i = 0; i < m; i++)
            {
                p = (d * p + pattern[i]) % q;
                t = (d * t + text[i]) % q;
            }

            for (int i = 0; i <= n - m; i++)
            {
                if (p == t)
                {
                    bool match = true;
                    for (int j = 0; j < m; j++)
                    {
                        _c++;
                        if (text[i + j] != pattern[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        indices.Add(i);
                    }
                }

                if (i < n - m)
                {
                    t = (d * (t - text[i] * h) + text[i + m]) % q;

                    if (t < 0)
                    {
                        t += q;
                    }
                }
            }

            return indices;
        }
    }
}
