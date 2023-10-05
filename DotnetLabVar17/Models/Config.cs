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

    public  string? VegetablePath { get; } // путь до файла, содержащий список овощей
    public  string? FruitsPath { get; }// путь до файла, содержащий список фруктов
    public  string? BerriesPath { get; }// путь до файла, содержащий список ягод
    public string? LogPath { get; } // путь до лог файла
    public Exception? Exception { get; set; } //Ошибка
}