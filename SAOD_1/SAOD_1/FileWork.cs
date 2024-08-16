using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_1
{
    internal class FileWork
    {
        public FileWork()
        {
            string partOriginal = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\Original.txt";
            string GenerationOriginal = "";
            Random random = new Random();
            for(int i = 0; i < 300; i++)
            {
                GenerationOriginal += random.Next(1, 99).ToString() + " ";
            }

            File.WriteAllText(partOriginal, GenerationOriginal);
            _memoryOriginal = GetFileSize(partOriginal);
            List<int> number = ReadNumbersFromFile(partOriginal);

            string partFixedVariable = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\FixedVariable.txt";
            string GeneretionFixedVariable = "";
            for(int i = 0;i < number.Count;i++)
            {
                GeneretionFixedVariable += new FixedVariable(i).Result ;
            }
            File.WriteAllText(partFixedVariable, GeneretionFixedVariable);
            _memoryFixedVariable = GetFileSize(partFixedVariable);

            string partGammaElias = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\GammaElias.txt";
            string GeneretionGammaElias = "";
            for (int i = 0; i < number.Count; i++)
            {
                GeneretionGammaElias += new GammaElias(i).Result;
            }
            File.WriteAllText(partGammaElias, GeneretionGammaElias);
            _memoryGammaElias = GetFileSize(partGammaElias);

            string partOmegaElias = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\OmegaElias.txt";
            string GeneretionOmegaElias = "";
            for (int i = 0; i < number.Count; i++)
            {
                GeneretionOmegaElias += new OmegaElias(i).Result ;
            }
            File.WriteAllText(partOmegaElias, GeneretionOmegaElias);
            _memoryOmegaElias = GetFileSize(partOmegaElias);

        }
        private long _memoryOriginal;
        private long _memoryFixedVariable;
        private long _memoryGammaElias;
        private long _memoryOmegaElias;
        private long GetFileSize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            return fileInfo.Length; // Возвращает размер файла в байтах
        }
        private List<int> ReadNumbersFromFile(string path)
        {
            List<int> numbers = new List<int>();

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] words = line.Split(new char[] { ' ', '\t', ',', ';', '.' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (int.TryParse(word, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }

            return numbers;
        }
    }
}
