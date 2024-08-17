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
            string partOriginal = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\Original.bin";
            List<int> GenerationOriginal = new List<int>();
            Random random = new Random();
            for(int i = 0; i < 300; i++)
            {
                GenerationOriginal.Add(random.Next(1, 99));
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(partOriginal, FileMode.Create)))
            {
                writer.Write(ConvertIntListToByteArray(GenerationOriginal));
            }
            _memoryOriginal = GetFileSize(partOriginal);
            List<int> number = ReadNumbersFromFile(partOriginal);

            string partFixedVariable = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\FixedVariable.bin";
            string GeneretionFixedVariable = "";
            using (BinaryWriter write = new BinaryWriter(File.Open(partFixedVariable, FileMode.Create)))
            {
                for (int i = 0; i < number.Count; i++)
                {
                    write.Write(ConvertBinaryStringToByteArray(new FixedVariable(number[i]).Result));
                }
            }
            _memoryFixedVariable = GetFileSize(partFixedVariable);

            string partGammaElias = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\GammaElias.bin";
            string GeneretionGammaElias = "";
            using (BinaryWriter write = new BinaryWriter(File.Open(partGammaElias, FileMode.Create)))
            {
                for (int i = 0; i < number.Count; i++)
                {
                    write.Write(ConvertBinaryStringToByteArray(new GammaElias(number[i]).Result));
                }
            }
            File.WriteAllText(partGammaElias, GeneretionGammaElias);
            _memoryGammaElias = GetFileSize(partGammaElias);

            string partOmegaElias = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\OmegaElias.bin";
            string GeneretionOmegaElias = "";
            using (BinaryWriter write = new BinaryWriter(File.Open(partOmegaElias, FileMode.Create)))
            {
                for (int i = 0; i < number.Count; i++)
                {
                    write.Write(ConvertBinaryStringToByteArray(new OmegaElias(number[i]).Result));
                }
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
        private byte[] ConvertBinaryStringToByteArray(string binaryString)
        {
            int length = binaryString.Length;         
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < length; i += 8)
            {
                string byteString = binaryString.Substring(i, 8);
                byte b = Convert.ToByte(byteString, 2);
                bytes.Add(b);
            }

            return bytes.ToArray(); 
        }

        private byte[] ConvertIntListToByteArray(List<int> intList)
        {
            // Проверка на наличие значений вне диапазона
            foreach (int value in intList)
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException("Все значения должны быть в диапазоне от 0 до 255.");
                }
            }

            // Преобразование списка целых чисел в массив байтов
            byte[] byteArray = new byte[intList.Count];
            for (int i = 0; i < intList.Count; i++)
            {
                byteArray[i] = (byte)intList[i]; // Приведение типа к байту
            }

            return byteArray; // Возвращаем массив байтов
        }
    }
}
