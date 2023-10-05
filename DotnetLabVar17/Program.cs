using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Repository;
using DotnetLabVar17.Services;

namespace DotnetLabVar17;


public class Program
{
    public static void Main(string[] args)
    {
        
        var chernika = new Berry("Черника", "Черный", DateTime.Now, false, true, true, false);
        var ejevika = new Berry("Ежевика", "Черный", DateTime.Now, false, true, true, false);
        var apple = new Fruit("Яблоко", "pзеленый", DateTime.Now, false,"Сладкий", true);
        var grusha = new Fruit("Груша", "pзеленый",  DateTime.Now, false,"Сладкий", true);
        // Считываем файл конфига адрес которого за хардкожен в методе
        // ReadConfig()
        var cfg = ReadConfig();
        // Если считывание конфига происходит с ошибкой, то выбасываем exception
        if (cfg?.Exception != null)
        {
            throw cfg.Exception;
        }
        // Инициализируем все сервисы
        var logger = new Logger();
        var fileLog = new FilePrint(cfg!.LogPath);
        var consoleLog = new ConsolePrint();
        // Добавляем в сервис логирования метод по выводу логов в консоль и файл
        logger.PrintMethods += fileLog.FileLogPrint;
        logger.PrintMethods += consoleLog.ConsoleLogPrint;
        // Инициализируем репозиторий бд
        var db = new DbRepository(cfg);
        var service = new BaseService(logger, db);
        
        
        service.CreatePlant(apple);
        service.CreatePlant(grusha);
        service.CreatePlant(chernika);
        service.CreatePlant(ejevika);
        service.PlantSort(PlantType.Fruits, plant => plant.PlantName);
        service.PlantSort(PlantType.Fruits, plant => plant.Id);
        
        service.End();


    }
   
    private static Config? ReadConfig()
    {
        // Считываем конфиг из appsetting.json
        // При отсутвии файла отдаем исключение
        Config? newConfig;
        var configFilePath = Directory.GetCurrentDirectory()[..^16] + "appsettings.json";
        if (!File.Exists(configFilePath))
        {
            newConfig = new Config(null, null, null,null)
            {
                Exception = new Exception($"There is no config file in directory {configFilePath}")
            };
            return newConfig;
        }
        var jsString = File.ReadAllText(configFilePath);
        try
        {
            // Проверяем существуют ли файлы указанные в конфиге
            var curCfg = JsonSerializer.Deserialize<Config>(jsString);
            newConfig = new Config(
                Directory.GetCurrentDirectory()[..^16] + curCfg.LogPath,
                Directory.GetCurrentDirectory()[..^16] + curCfg.FruitsPath,
                Directory.GetCurrentDirectory()[..^16] + curCfg.VegetablePath,
                Directory.GetCurrentDirectory()[..^16] + curCfg.BerriesPath
            );
            if(!CheckFileValid(newConfig?.BerriesPath) || !CheckFileValid(newConfig?.FruitsPath) || !CheckFileValid(newConfig?.VegetablePath))
            {
                throw new Exception("Error of db files");
            }
        }
        catch (Exception e)
        {
            newConfig = new Config(null,null,null,null){Exception = e};
            return newConfig;
        }
        // Если все хорошо то возвращаем конфиг без исключений
        newConfig!.Exception = null;
        return newConfig;
    }
    private static bool CheckFileValid(string? fileName)
    {
        // Проверка на валидность
        // Расширение json or xml
        // И наличие в папке
        if (!File.Exists(fileName)) return false;
        var j = fileName[^5..].ToLower();
        var x = fileName[^4..].ToLower();
        var r = j == ".json" || x == ".xml";
        return r;
    }
}