namespace DotnetLabVar17.Models;

public class Config
{
    // Класс конфигурации
    public Config(string? logPath, string? fruitsPath, string? vegetablePath, string? berriesPath)
    {
        FruitsPath = fruitsPath;
        VegetablePath = vegetablePath;
        BerriesPath = berriesPath;
        LogPath = logPath;
    }

    public static Config ReturnExceptionConfig(Exception e)
    {
        return new Config(null, null, null, null) { Exception = e };
    }

    public  string? VegetablePath { get; } // путь до файла, содержащий список овощей
    public  string? FruitsPath { get; }// путь до файла, содержащий список фруктов
    public  string? BerriesPath { get; }// путь до файла, содержащий список ягод
    public string? LogPath { get; } // путь до лог файла
    public Exception? Exception { get; set; } //Ошибка

    public bool CheckFilePathValid()
    {
        if (!File.Exists(LogPath) || LogPath[^4..] != ".txt") return false;
        var pathList = new List<string?>() { VegetablePath, FruitsPath, BerriesPath };
        foreach (var fileName in pathList)
        {
            if (!File.Exists(fileName)) return false;
            var j = fileName[^5..].ToLower();
            var x = fileName[^4..].ToLower();
            var r = j == ".json" || x == ".xml";
            if (r != true) return false;
        }
        return true;
    }
}