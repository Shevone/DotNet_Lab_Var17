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
        var apple = new Fruit("Яблоко", "зеленый", DateTime.Now, false,"Сладкий", true);
        var grusha = new Fruit("Груша", "зеленый",  DateTime.Now, false,"Сладкий", true);
        var baklajan = new Vegetable("Бакалажан", "фиолетовый", DateTime.Now, false,3);
        var ogurech = new Vegetable("Огурец", "зелёный",  DateTime.Now, false, 4);
        var farmers = new List<Farmer>
        {
            new ("Иван"),
            new ("Василий"),
            new ("Николай"),
            new ("Артём"),
            new ("Егор"),
            new ("Данила"),
            new ("Марк"),
            new ("Илья"),
            new ("Макар"),
            new ("Арсений"),
            new ("Андрей"),
            new ("Петр"),
            new ("Александр"),
            new ("Максим")
        };
        var harv1 = new Harvest();
        var harv2 = new Harvest();
        
        var service = Run();
   
        service.CreatePlant(apple);
        service.CreatePlant(grusha);
        service.CreatePlant(chernika);
        service.CreatePlant(ejevika);
        service.CreatePlant(baklajan);
        service.CreatePlant(ogurech);

        foreach (var farmer in farmers)
        {
            service.CreateFarmer(farmer);
        }
        service.PlantSort(PlantType.Fruits, plant => plant.PlantName);
        service.PlantSort(PlantType.Fruits, plant => plant.Id);
        
        service.CreateHarvest(harv1);
        service.CreateHarvest(harv2);
        service.AddFarmerToHarvest(1,1);
        service.AddFarmerToHarvest(1,2);
        service.AddFarmerToHarvest(2,1);
        service.AddFarmerToHarvest(2,8);
        service.AddFarmerToHarvest(2, 4);
        service.AddPlantToHarvest(1,PlantType.Fruits,1,90);
        service.AddPlantToHarvest(1,PlantType.Vegetables,1,1);
        service.AddPlantToHarvest(2,PlantType.Fruits,2,13901);
        service.AddPlantToHarvest(2,PlantType.Berries,2,212);
        service.RemoveFarmerFromHarv(2,4);
        
        
        service.HarvestInfo(1);
        service.HarvestInfo(2);
        
        service.End();


    }
    private static BaseService Run()
    {
        // Считываем файл конфига адрес которого за хардкожен в методе
        // ReadConfig()
        var cfg = ReadConfig();
        // Если считывание конфига происходит с ошибкой, то выбасываем exception
        if (cfg.Exception != null)
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
        return service;
    }
   
    private static Config ReadConfig()
    {
        // Считываем конфиг из appsetting.json
        // При отсутвии файла отдаем исключение
        Config? newConfig;
        var configFilePath = Directory.GetCurrentDirectory()[..^16] + "appsettings.json";
        try
        {
            if (!File.Exists(configFilePath))
            {
                throw new Exception($"There is no config file in directory {configFilePath}");
            }
            var jsString = File.ReadAllText(configFilePath);
       
            // Проверяем существуют ли файлы указанные в конфиге
            var curCfg = JsonSerializer.Deserialize<Config>(jsString);
            newConfig = new Config(
                Directory.GetCurrentDirectory()[..^16] + curCfg.LogPath,
                Directory.GetCurrentDirectory()[..^16] + curCfg.FruitsPath,
                Directory.GetCurrentDirectory()[..^16] + curCfg.VegetablePath,
                Directory.GetCurrentDirectory()[..^16] + curCfg.BerriesPath
            );
            var check = newConfig.CheckFilePathValid();
            if (!check)
            {
                throw new Exception("Error of db files");
            }
        }
        catch (Exception e)
        {
            return Config.ReturnExceptionConfig(e);
        }
        // Если все хорошо то возвращаем конфиг без исключений
        newConfig!.Exception = null;
        return newConfig;
    }
}