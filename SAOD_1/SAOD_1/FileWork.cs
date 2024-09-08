using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            List<int> number = new List<int>();
            for (int i = 0; i < 300; i++)
            {
                GenerationOriginal.Add(random.Next(10, 99));
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(partOriginal, FileMode.Create)))
            {
                writer.Write(ConvertIntListToByteArray(GenerationOriginal));
            }
            _memoryOriginal = 300 * 32;

            string partFixedVariable = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\FixedVariable.bin";
            string [] GeneretionFixedVariable = new string[GenerationOriginal.Count];
            
            for(int i  = 0; i < GenerationOriginal.Count; i++)
            {
                GeneretionFixedVariable[i] = new FixedVariable(GenerationOriginal[i]).Result;
            }
            byte[] ByteArrayFixed = ConvertBinaryStringsToBytes(GeneretionFixedVariable);
            using (BinaryWriter write = new BinaryWriter(File.Open(partFixedVariable, FileMode.Create)))
            {
                for (int i = 0; i < ByteArrayFixed.Length; i++)
                {
                    write.Write(ByteArrayFixed[i]);
                }
            }
            _memoryFixedVariable = GetFileSize(partFixedVariable);

            string partGammaElias = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\GammaElias.bin";
            string[] GeneretionGammaElias = new string[GenerationOriginal.Count];
            for (int i = 0; i < GenerationOriginal.Count; i++)
            {
                GeneretionGammaElias[i] = new GammaElias(GenerationOriginal[i]).Result;
            }
            byte[] ByteArrayGammaElias = ConvertBinaryStringsToBytes(GeneretionGammaElias);
            using (BinaryWriter write = new BinaryWriter(File.Open(partGammaElias, FileMode.Create)))
            {
                for (int i = 0; i < ByteArrayGammaElias.Length; i++)
                {
                    write.Write(ByteArrayGammaElias[i]);
                }
            }
            _memoryGammaElias = GetFileSize(partGammaElias);

            string partOmegaElias = "C:\\Users\\karpo\\OneDrive\\Рабочий стол\\SAOD\\SAOD_1\\GammaElias.bin";
            string[] GeneretionOmegaElias = new string[GenerationOriginal.Count];
            for (int i = 0; i < GenerationOriginal.Count; i++)
            {
                GeneretionOmegaElias[i] = new OmegaElias(GenerationOriginal[i]).Result;
            }
            byte[] ByteArrayOmegaElias = ConvertBinaryStringsToBytes(GeneretionOmegaElias);
            using (BinaryWriter write = new BinaryWriter(File.Open(partOmegaElias, FileMode.Create)))
            {
                for (int i = 0; i < ByteArrayOmegaElias.Length; i++)
                {
                    write.Write(ByteArrayOmegaElias[i]);
                }
            }
            _memoryOmegaElias = GetFileSize(partOmegaElias);

        }

        public long MemoryOriginal => _memoryOriginal;
        public long MemoryFixedVariable => _memoryFixedVariable;
        public long MemoryGammaElias => _memoryGammaElias;
        public long MemoryOmegaElias => _memoryOmegaElias;

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
        private List<int> ReadNumbersFromFile(string filePath)
        {
            List<int> numbers = new List<int>();

            try
            {
                // Читаем все строки из файла
                string content = File.ReadAllText(filePath);
                // Разбиваем строку по пробелам и конвертируем в целые числа
                string[] numberStrings = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string numberString in numberStrings)
                {
                    if (int.TryParse(numberString, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }

            return numbers;
        }

        private byte[] ConvertBinaryStringsToBytes(string[] binaryStrings)
        {
            List<byte> bytes = new List<byte>();
            string stringCode = "";
            for(int i = 0; i < binaryStrings.Length; i++)
            {
                 binaryStrings[i] = binaryStrings[i].Replace(" ","");
                stringCode += binaryStrings[i];
            }
            foreach (char c in stringCode)
            {
                if (c == '1')
                {
                    bytes.Add((byte)1);
                }
                else if (c == '0')
                {
                    bytes.Add((byte)0);
                }
                else
                {
                    //Кидаем исключение
                }
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
