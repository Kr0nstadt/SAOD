using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shennon
{
    public class FanoCoding
    {
        public static List<SymbolCode> GenerateFanoCode(Dictionary<char, double> probabilities)
        {
            var symbols = probabilities.OrderByDescending(kvp => kvp.Value).ToList();
            var result = new List<SymbolCode>();
            GenerateFanoCodeRecursive(symbols, result, string.Empty);
            return result;
        }

        private static void GenerateFanoCodeRecursive(List<KeyValuePair<char, double>> symbols, List<SymbolCode> result, string code)
        {
            if (symbols.Count == 0)
                return;

            if (symbols.Count == 1)
            {
                result.Add(new SymbolCode(symbols[0].Key, code));
                return;
            }

            double total = symbols.Sum(kvp => kvp.Value);
            double cumulative = 0;
            int splitIndex = 0;

            for (int i = 0; i < symbols.Count; i++)
            {
                cumulative += symbols[i].Value;

                if (cumulative >= total / 2)
                {
                    splitIndex = i;
                    break;
                }
            }

            var leftSymbols = symbols.Take(splitIndex + 1).ToList();
            var rightSymbols = symbols.Skip(splitIndex + 1).ToList();

            GenerateFanoCodeRecursive(leftSymbols, result, code + "0");
            GenerateFanoCodeRecursive(rightSymbols, result, code + "1");
        }
    }
}
