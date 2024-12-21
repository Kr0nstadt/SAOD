using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shennon
{
    class GilbertMoore
    {
        public Dictionary<char, string> codes = new Dictionary<char, string>();
        private Dictionary<char, double> symbolProbabilities;

        public GilbertMoore(Dictionary<char, double> dict)
        {
            symbolProbabilities = dict;
            BuildCodes();
        }

        private void BuildCodes()
        {
            // Сортируем символы по вероятности
            var sortedSymbols = symbolProbabilities.OrderByDescending(x => x.Value).ToList();
            GenerateCodes();
        }

        private void GenerateCodes()
        {
            double comm = 0;
            double pr = 0.0;
            double sigma = 0.0;
            var dic = from entry in symbolProbabilities orderby entry.Value descending select entry;
            foreach(var item in dic)
            {
                /*comm += item.Value / 2.0;
                pr += item.Value;
                double code = item.Value + (comm * 0.5);
                string CodeString = Convert.ToString(DoubleToCode(code), 2);
                int n =(int) -(Math.Ceiling(Math.Log2(item.Value))) + 1;
                codes[item.Key] = CodeString.Substring(0, n);*/

                comm += item.Value;
                sigma = comm + item.Value / 2.0;
                int n = (int)-(Math.Ceiling(Math.Log2(item.Value / 2.0)));
                long l = DoubleToCode(sigma);
                string CodeString = Convert.ToString(l, 2).PadLeft(64, '0');
                codes[item.Key] = CodeString.Substring(13, n);
            }
        }

        private long DoubleToCode(double code)
        {
            string[] codeStr = code.ToString().Split(",");
            string SubStr = "0," + codeStr[1];
            /*for(int i = 0; i < 10; i++)
            {
                if (!(codeStr[i] == '0' ||codeStr[i] == ','))
                {
                    SubStr += codeStr[i];
                }
            }*/
            return BitConverter.DoubleToInt64Bits(Double.Parse(SubStr));
        }
        static string ConvertToBinary(double number)
        {
            // Разделяем число на целую и дробную части
            long integerPart = (long)number;
            double fractionalPart = number - integerPart;

            // Преобразуем целую часть в двоичную
            string integerBinary = ConvertIntegerToBinary(integerPart);

            // Преобразуем дробную часть в двоичную
            string fractionalBinary = ConvertFractionToBinary(fractionalPart);

            return fractionalBinary;
        }

        static string ConvertIntegerToBinary(long integerPart)
        {
            if (integerPart == 0) return "0";

            string binary = "";
            while (integerPart > 0)
            {
                binary = (integerPart % 2) + binary;
                integerPart /= 2;
            }
            return binary;
        }

        static string ConvertFractionToBinary(double fractionalPart)
        {
            string binary = "";
            int count = 0;

            // Ограничиваем количество итераций для предотвращения бесконечного цикла
            while (fractionalPart > 0 && count < 10)
            {
                fractionalPart *= 2;
                if (fractionalPart >= 1)
                {
                    binary += "1";
                    fractionalPart -= 1;
                }
                else
                {
                    binary += "0";
                }
                count++;
            }

            return binary;
        }
    }
}
