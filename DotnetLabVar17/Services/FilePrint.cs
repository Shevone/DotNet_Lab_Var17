namespace DotnetLabVar17.Services;

public class FilePrint
{
    public FilePrint(string fileName)
    {
        _filePath = fileName;
        if (!File.Exists(fileName))
        {
            File.Create(fileName);
        }
    }

    private readonly string _filePath;
    public void FileLogPrint(string message)
    {
        using StreamWriter writer = new StreamWriter(_filePath, true);
        writer.WriteLine(message);
    }
}