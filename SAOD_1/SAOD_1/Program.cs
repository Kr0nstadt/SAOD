using System;
using System.Text;
using SAOD_1;

class MainClass
{
    public static void Main()
    {
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("| Число\t| Fixed + Variable\t| Гамма\t\t| Омега\t\t|");
        Console.WriteLine("-----------------------------------------------------------------");
        for (int i = 0; i < 18; i++)
        {
            Console.WriteLine($"| {i}\t| {new FixedVariable(i).Result}\t\t| {new GammaElias(i).Result}\t\t| {new OmegaElias(i).Result}\t\t|");
        }
        Console.WriteLine("-----------------------------------------------------------------");
        FileWork fileWork = new FileWork();
        Console.WriteLine(fileWork.MemoryOriginal + "  " + fileWork.MemoryFixedVariable);
    }
    public static long GetFileSize(string filePath)
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
}
