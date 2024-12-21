using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    internal class Haffmen
    {
        private static string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        private string tread = Path.Combine(directoryPath, "testBase4.dat");
        private Dictionary<char, double> frequencies;
        public Dictionary<char, string> Alfavit;
        public double kraft => Kraft(Alfavit);
        public double srLength => SRLenght(Alfavit);
        public double entopia => Entopia(Alfavit, frequencies);
        private double SRLenght(Dictionary<char, string> code)
        {
            double SRLenght = 0.0;
            foreach (var val in code)
            {
                SRLenght += frequencies[val.Key] * val.Value.Length;
            }
            return SRLenght;
        }
        static double Entopia(Dictionary<char, string> code, Dictionary<char, double> ShennonAlpha)
        {
            double Entopia = 0.0;
            foreach (var val in code)
            {
                double temp = ShennonAlpha[val.Key];
                Entopia -= temp * Math.Log2(temp);
            }
            return Entopia;
        }

        public Haffmen()
        {
            byte[] file = File.ReadAllBytes(tread);
            char[] filechar = System.Text.Encoding.GetEncoding(866).GetString(file).ToCharArray();
            int val = filechar.Distinct().Count();
            Dictionary<char, double> frequencies = CalculateProbabilities(filechar);
            Node root = BuildHuffmanTree(frequencies);
            var Codes = new Dictionary<char, string>();
            GenerateCodes(root, "", Codes);
            Alfavit = SortDictionaryByValueLength(Codes);
        }
        private double Kraft(Dictionary<char, string> code)
        {
            double Kraft = 0.0;
            foreach (var val in code)
            {
                Kraft += 1 / (Math.Pow(2, val.Value.Length));
            }
            return Kraft;
        }
        private Dictionary<char, string> SortDictionaryByValueLength(Dictionary<char, string> dictionary)
        {
            var sorted = dictionary.OrderBy(kvp => kvp.Value.Length);
            return sorted.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        private Dictionary<char, double> CalculateProbabilities(char[] inputArray)
        {
            var frequencyDict = new Dictionary<char, int>();
            int totalCount = inputArray.Length;

            foreach (var c in inputArray)
            {
                if (frequencyDict.ContainsKey(c))
                {
                    frequencyDict[c]++;
                }
                else
                {
                    frequencyDict[c] = 1;
                }
            }

            var probabilityDict = new Dictionary<char, double>();
            foreach (var kvp in frequencyDict)
            {
                probabilityDict[kvp.Key] = (double)kvp.Value / totalCount;
            }
            frequencies = probabilityDict;
            return probabilityDict;
        }
        private static Node BuildHuffmanTree(Dictionary<char, double> frequencies)
        {
            var priorityQueue = new SortedSet<Node>(Comparer<Node>.Create((x, y) =>
            {
                int result = x.Frequency.CompareTo(y.Frequency);
                if (result == 0)
                    return x.Symbol.CompareTo(y.Symbol);
                return result;
            }));

            foreach (var kvp in frequencies)
            {
                priorityQueue.Add(new Node(kvp.Key, kvp.Value));
            }

            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue.Min;
                priorityQueue.Remove(left);
                var right = priorityQueue.Min;
                priorityQueue.Remove(right);

                var parent = new Node(left, right);
                priorityQueue.Add(parent);
            }

            return priorityQueue.Min;
        }

        private static void GenerateCodes(Node node, string code, Dictionary<char, string> huffmanCodes)
        {
            if (node == null) return;
            if (node.Left == null && node.Right == null)
            {
                huffmanCodes[node.Symbol] = code;
                return;
            }

            if (node.Left != null)
                GenerateCodes(node.Left, code + "0", huffmanCodes);

            if (node.Right != null)
                GenerateCodes(node.Right, code + "1", huffmanCodes);
        }
    }
}
