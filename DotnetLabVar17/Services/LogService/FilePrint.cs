namespace DotnetLabVar17.Services;

public class FilePrint
{
    // Класс отвечающий за печать в файл
    public FilePrint(string? fileName)
    {
        fileName ??= "log.txt";
        _filePath = fileName;
        if (!File.Exists(fileName))
        {
            File.Create(fileName);
        }
    }

    private readonly string _filePath;
    public void FileLogPrint(string message)
    {
        // Запись сообщения лога в файл
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine(message);
    }
}