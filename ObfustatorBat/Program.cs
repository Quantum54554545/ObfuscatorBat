using Console = Colorful.Console;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            Console.WriteLine("Перетащите файл .bat на иконку этого приложения.");
            return;
        }

        string filePath = args[0];
        if (File.Exists(filePath) && Path.GetExtension(filePath).ToLower() == ".bat")
        {
            byte[] hexPrefix = { 0xFF, 0xFE, 0x26, 0x63, 0x6C, 0x73, 0x0D, 0x0A, 0xFF, 0xFE, 0x0A, 0x0D };
            byte[] originalFileBytes = File.ReadAllBytes(filePath);
            byte[] newFileBytes = new byte[hexPrefix.Length + originalFileBytes.Length];

            Array.Copy(hexPrefix, 0, newFileBytes, 0, hexPrefix.Length);
            Array.Copy(originalFileBytes, 0, newFileBytes, hexPrefix.Length, originalFileBytes.Length);

            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), "save.bat");
            File.WriteAllBytes(newFilePath, newFileBytes);

            Console.WriteLine($"Файл успешно обновлен и сохранен как {newFilePath}");
        }
        else
        {
            Console.WriteLine("Неправильный файл. Пожалуйста, перетащите корректный файл .bat.");
        }
    }
}
