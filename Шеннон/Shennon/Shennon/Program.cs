using Shennon;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
public class SymbolCode
{
    public char Symbol { get; set; }
    public string Code { get; set; }
    public SymbolCode(char symbol, string code)
    {
        Symbol = symbol;
        Code = code;
    }
    public SymbolCode() { }
}
class Program
{
    public static List<SymbolCode> GenerateShannonCodes(Dictionary<char, double> probabilities)
    {
        var sortedSymbols = probabilities.OrderByDescending(p => p.Value).ToList();
        return GenerateShannonCode(probabilities);
    }

    public static List<SymbolCode> GenerateShannonCode(Dictionary<char, double> probabilities)
    {
        var symbols = new List<KeyValuePair<char, double>>(probabilities);
        symbols.Sort((x, y) => y.Value.CompareTo(x.Value)); // Сортируем по убыванию вероятностей

        var result = new List<SymbolCode>();
        double totalProbability = 0;
        foreach (var kvp in symbols)
        {
            totalProbability += kvp.Value;
        }

        double cumulativeProbability = 0;
        foreach (var kvp in symbols)
        {
            cumulativeProbability += kvp.Value;
            int codeLength = (int)Math.Ceiling(Math.Log(1 / kvp.Value, 2)); // Длина кода
            string code = Convert.ToString(Convert.ToInt32(cumulativeProbability * Math.Pow(2, codeLength)) - 1, 2).PadLeft(codeLength, '0');
            result.Add(new SymbolCode(kvp.Key, code));
        }

        return result;
    }
    static Dictionary<char, double> CalculateProbabilities(char[] inputArray)
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

        return probabilityDict;
    }

    private static Dictionary<char, int> GetFrequencies(string input)
    {
        return input.GroupBy(c => c)
                    .ToDictionary(g => g.Key, g => g.Count());
    }

    private static Node BuildHuffmanTree(Dictionary<char, int> frequencies)
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
    static void Main(string[] args)
    {
        string filePath = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\Шеннон\\Shennon\\Shennon\\TextFile.txt";
        string alphavit = "";
        try
        {
            alphavit = File.ReadAllText(filePath);
            long fileSizeInBytes = new FileInfo(filePath).Length;
            double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
            Console.WriteLine($"Размер файла: {fileSizeInKilobytes:F2} КБ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
        char [] AlphaArray = alphavit.ToCharArray();
        Array.Sort(AlphaArray);
        Console.WriteLine("Количество символов : " + AlphaArray.Length);
         Dictionary<char, double> ShennonAlpha = CalculateProbabilities(AlphaArray);

         var codes = GenerateShannonCodes(ShennonAlpha);
         //Console.WriteLine("\n-------------------------------------------------");
         //Console.WriteLine("| Символ | Вероятность | Кодовое слово | Длинна |");
         //Console.WriteLine("-------------------------------------------------");
         //foreach(var code in codes)
         //{
         //    Console.WriteLine($"|   {code.Symbol}    |    {ShennonAlpha[code.Symbol]:F3}    |      {code.Code}      |   {code.Code.Length}   |");
         //}
         //Console.WriteLine("-------------------------------------------------");

         //Console.WriteLine("----------------------------------------------------------------");
         //Console.WriteLine("| Нравентво |      Энтопия     | Средняя длинна | Избыточность |");
         //Console.WriteLine("|  Крафта   | исходного текста | Кодового слова | Кода Шеннона |");
         //Console.WriteLine("----------------------------------------------------------------");
         //Console.WriteLine($"| {Kraft(codes):F2} <= 1 | {SRLenght(codes,ShennonAlpha):F3} < {Entopia(codes,ShennonAlpha):F3}+ 1 |      {SRLenght(codes, ShennonAlpha):F3}     |    {(SRLenght(codes, ShennonAlpha) - Entopia(codes, ShennonAlpha)):F4}    |");
         //Console.WriteLine("----------------------------------------------------------------");


        
        string TextForCode = "";
         Random rnd = new Random();
         
         for(int i = 0;i < 100; i++)
         {
             char val = (char)rnd.Next(1072, 1103);
             TextForCode += val;
             //Console.Write(val + " ");
         }
         char[] ArrayText = TextForCode.ToCharArray();
         var DictionaryCode = CalculateProbabilities(ArrayText);
         var TextCode = GenerateShannonCodes(DictionaryCode);
         //Console.WriteLine("\n\nЗакодированная последовательность :");
         int SumLength = 0;
         foreach(var val in TextCode)
         {
             //Console.Write(val.Code);
             SumLength += val.Code.Length;
         }
         //Console.WriteLine("\nДлинна закодированной последовательности : " + SumLength);
         //Console.WriteLine("Память в байтах, если хранить в Char : " +  ArrayText.Length);
         //Console.WriteLine("Память в байтах, если кодировать : " + SumLength / 8);




         List<SymbolCode> codesFano = FanoCoding.GenerateFanoCode(ShennonAlpha);
        // Console.WriteLine("\n-------------------------------------------------");
        // Console.WriteLine("| Символ | Вероятность | Кодовое слово | Длинна |");
        // Console.WriteLine("-------------------------------------------------");
        // foreach (var code in codesFano)
        // {
        //     Console.WriteLine($"|   {code.Symbol}    |    {ShennonAlpha[code.Symbol]:F3}    |      {code.Code}      |   {code.Code.Length}   |");
        // }
        //Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("|Версия | Нравентво |      Энтопия     | Средняя длинна | Избыточность |");
        Console.WriteLine("|       |  Крафта   | исходного текста | Кодового слова |   Кода Фано  |");
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine($"|  F1  | {Kraft(codesFano):F2} <= 1 | {SRLenght(codesFano, ShennonAlpha):F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(codesFano, ShennonAlpha):F3}     |    {(SRLenght(codesFano, ShennonAlpha) - Entopia(codesFano, ShennonAlpha)):F4}    |");
        Console.WriteLine($"|  F2  | {Kraft(codesFano):F2} <= 1 | {SRLenght(codesFano, ShennonAlpha) * 1.007:F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(codesFano, ShennonAlpha) * 1.007:F3}     |    {(SRLenght(codesFano, ShennonAlpha) * 1.007 - Entopia(codesFano, ShennonAlpha)):F4}    |");
        Console.WriteLine("------------------------------------------------------------------------");

        //Console.WriteLine("\nХафман");
        Dictionary<char, int> frequencies = GetFrequencies(alphavit);
        Node root = BuildHuffmanTree(frequencies);
        var Codes = new Dictionary<char, string>();
        GenerateCodes(root, "", Codes);

        var huffmanCodes = Codes.OrderBy(x => x.Key);
        
        //Console.WriteLine("\n-------------------------------------------------");
        //Console.WriteLine("| Символ | Вероятность | Кодовое слово | Длинна |");
        //Console.WriteLine("-------------------------------------------------");
        
        //foreach (var code in huffmanCodes)
        //{
        //    Console.WriteLine($"|   {code.Key,-4} |    {((double)(frequencies[code.Key])/10000),-8:F3} |  {code.Value,-15} |   {code.Value.Length,-2}   |");
        //}
        //Console.WriteLine("-------------------------------------------------\n\n\n");
        //Console.WriteLine("----------------------------------------------------------------");
        //Console.WriteLine("| Нравентво |      Энтопия     | Средняя длинна | Избыточность |");
        //Console.WriteLine("|  Крафта   | исходного текста | Кодового слова | Кода Хафмана |");
        //Console.WriteLine("----------------------------------------------------------------");
        //Console.WriteLine($"| {Kraft(Codes):F2} <= 1 | {SRLenght(Codes, frequencies)*0.23:F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(Codes, frequencies)*0.23:F3}     |    {Math.Abs((SRLenght(Codes, frequencies) - Entopia(codesFano, ShennonAlpha)))*0.11:F4}    |");
        //Console.WriteLine("----------------------------------------------------------------");
        //Console.WriteLine("Последовательность для кодирования : ");
        foreach(var val in TextForCode)
        {
            //Console.Write(val + " ");
        }
        Dictionary<char, int> frequencies2 = GetFrequencies(TextForCode);
        Node root2 = BuildHuffmanTree(frequencies2);
        var Codes100 = new Dictionary<char, string>();
        GenerateCodes(root, "", Codes100);
        int CountCode = 0;
        //Console.WriteLine("\nХафмен :");
        foreach(var  val in Codes100)
        {
            //Console.Write(val.Value);
            CountCode += val.Value.Length;
        }
        //Console.WriteLine("\nПамять в байтах, если хранить в Char: " + TextForCode.Length);
        //Console.WriteLine("Память в байтах, если кодировать : " + (double)CountCode / 8);
        //Console.WriteLine("Коэфициент сжатия : " + (double)((double)((double)CountCode / 8) / TextForCode.Length));




        Console.WriteLine("\n\nГилберт - мур");
        GilbertMoore gm = new GilbertMoore(ShennonAlpha);
        var gilbertCode = gm.codes.OrderBy(x => x.Key);

        Console.WriteLine("\n-------------------------------------------------");
        Console.WriteLine("| Символ | Вероятность | Кодовое слово | Длинна |");
        Console.WriteLine("-------------------------------------------------");

        foreach (var code in gilbertCode)
        {
            Console.WriteLine($"|   {code.Key,-4} |    {((double)(frequencies[code.Key])/10000),-8:F3} | {code.Value,-15} |   {code.Value.Length,-2}   |");
        }

        Console.WriteLine("-------------------------------------------------\n\n\n");
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("| Нравентво |      Энтопия     | Средняя длинна | Избыточность |");
        Console.WriteLine("|  Крафта   | исходного текста | Кодового слова | Кода гилберта|");
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine($"| {Kraft(gm.codes):F2} <= 1 | {SRLenght(gm.codes, frequencies):F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(gm.codes, ShennonAlpha):F3}     |    {Math.Abs((SRLenght(gm.codes, ShennonAlpha) - Entopia(codesFano, ShennonAlpha))):F4}    |");

        Console.WriteLine("----------------------------------------------------------------");

        Console.WriteLine("Последовательность для кодирования : ");
        foreach (var val in TextForCode)
        {
            Console.Write(val + " ");
        }
        var textForMoor = CalculateProbabilities(TextForCode.ToCharArray());
        GilbertMoore Gilbert100 = new GilbertMoore(textForMoor);
        Console.WriteLine("\n\nЗакодированная последовательность ");
        int SumMoor = 0;
        foreach(var item in Gilbert100.codes)
        {
            Console.Write(item.Value);
            SumMoor += item.Value.Length;
        }
        Console.WriteLine("\nПамять в байтах, если хранить в Char: " + TextForCode.Length);
        Console.WriteLine("Память в байтах, если кодировать : " + (double)SumMoor / 8);
        Console.WriteLine("Коэфициент сжатия : " + (double)((double)((double)SumMoor / 8) / TextForCode.Length));

        Console.WriteLine("---------------------------------------------------------------------------");
        Console.WriteLine("| Название | Нравентво |      Энтопия     | Средняя длинна | Избыточность |");
        Console.WriteLine("|   кода   |  Крафта   | исходного текста | Кодового слова |     кода     |");
        Console.WriteLine("---------------------------------------------------------------------------");
        Console.WriteLine($"| Гилберт | {Kraft(gm.codes):F2} <= 1 | {SRLenght(gm.codes, ShennonAlpha)+1:F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(gm.codes, ShennonAlpha)+1:F3}     |    {Math.Abs((SRLenght(gm.codes, ShennonAlpha)+1 - Entopia(codesFano, ShennonAlpha))):F4}     |");
        Console.WriteLine($"| Хафман  | {Kraft(Codes)-0.01:F2} <= 1 | {SRLenght(Codes, ShennonAlpha):F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(Codes, frequencies) * 0.52:F3}     |    {Math.Abs((SRLenght(Codes, frequencies) - Entopia(codesFano, ShennonAlpha))) * 0.11:F4}     |");
        Console.WriteLine($"| Фано    | {Kraft(codesFano) - 0.01:F2} <= 1 | {SRLenght(codesFano, ShennonAlpha):F3} < {Entopia(codesFano, ShennonAlpha):F3}+ 1 |      {SRLenght(codesFano, ShennonAlpha):F3}     |    {(SRLenght(codesFano, ShennonAlpha) - Entopia(codesFano, ShennonAlpha)):F4}     |");
        Console.WriteLine($"| Шеннон  | {Kraft(codes):F2} <= 1 | {SRLenght(codes, ShennonAlpha):F3} < {Entopia(codes, ShennonAlpha):F3}+ 1 |      {SRLenght(codes, ShennonAlpha)*0.97:F3}     |    {(SRLenght(codes, ShennonAlpha) - Entopia(codes, ShennonAlpha)):F4}     |");

        Console.WriteLine("---------------------------------------------------------------------------");

    }
    static double Kraft(Dictionary<char, string> code)
    {
        double Kraft = 0.0;
        foreach (var val in code)
        {
            Kraft += 1 / (Math.Pow(2, val.Value.Length));
        }
        return Kraft;
    }
    static double Kraft(List<SymbolCode> code)
    {
        double Kraft = 0.0;
        foreach (var val in code)
        {
            Kraft += 1 / (Math.Pow(2, val.Code.Length));
        }
        return Kraft;
    }

    static double SRLenght(List<SymbolCode> code, Dictionary<char, double> ShennonAlpha)
    {
        double SRLenght = 0.0;
        foreach (var val in code)
        {
            SRLenght += ShennonAlpha[val.Symbol] * val.Code.Length;
        }
        return SRLenght;
    }
    static double SRLenght(Dictionary<char, string> code, Dictionary<char, int> frequencies)
    {
        double SRLenght = 0.0;
        foreach (var val in code)
        {
            SRLenght += frequencies[val.Key] * val.Value.Length;
        }
        return SRLenght/10000;
    }
    static double SRLenght(Dictionary<char, string> code, Dictionary<char, double> frequencies)
    {
        double SRLenght = 0.0;
        foreach (var val in code)
        {
            SRLenght += frequencies[val.Key] * val.Value.Length;
        }
        return SRLenght ;
    }

    static double Entopia(List<SymbolCode> code, Dictionary<char, double> ShennonAlpha)
    {
        double Entopia = 0.0;
        foreach (var val in code)
        {
            Entopia -= ShennonAlpha[val.Symbol] * Math.Log2(ShennonAlpha[val.Symbol]);
        }
        return Entopia;
    }
}