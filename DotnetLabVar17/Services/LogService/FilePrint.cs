using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17.Services;

public class FilePrint
{
    // Класс отвечающий за печать в файл
    private readonly object _locker;
    public FilePrint(string? fileName)
    {
        fileName ??= "log.txt";
        _filePath = fileName;
        if (!File.Exists(fileName))
        {
            File.Create(fileName).Close();
        }

        _locker = new object();
    }

    private readonly string _filePath;
    public void FileLogPrint(string message)
    {
        // Тк запись в вфайл вызывается из нескольких потоков то необходимо синхронизировать потоки в этой точке прграммы
        lock (_locker)
        { 
            
            using var writer = new StreamWriter(_filePath, true);
            writer.WriteLine(message);
        }
        
    }
}