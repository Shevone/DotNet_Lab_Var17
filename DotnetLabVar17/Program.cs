using System.Globalization;
using System.Text;
using System.Text.Json;
using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Repository;
using DotnetLabVar17.Services;

namespace DotnetLabVar17;


public class Program
{
    public static void Main(string[] args)
    {
        /*List<Farmer> list = new List<Farmer>()
        {
            new Farmer("Bacy"),
            new Farmer("Nick"),
            new Farmer("Egr")
        };
    
        list.OrderBy(x => x.Name);*/
        
        var cfg = ReadConfig();
        if (cfg?.Exception != null)
        {
            throw cfg.Exception;
        }
        
        var logger = new Logger();
        var fileLog = new FilePrint(cfg!.LogPath);
        var consoleLog = new ConsolePrint();
        logger.PrintMethods += fileLog.FileLogPrint;
        logger.PrintMethods += consoleLog.ConsoleLogPrint;
        logger.OnStartWorking();
        var mutexObj = new Mutex();
        var fruitList = new PlantList<Fruit>();
        var berries = new PlantList<Berry>();
        var vegetables = new PlantList<Vegetable>();
        var db = new DbRepository(berries,fruitList , vegetables);
        
        var service = new BaseService(logger, db);
        
        var apple = new Fruit("Яблоко", "pзеленый", "Сладкий", true);
        service.CreatePlant(apple);
        var grusha = new Fruit("Груша", "pзеленый", "Сладкий", true);
        service.CreatePlant(grusha);
       
        service.PlantSort(PlantType.Fruits, plant => plant.PlantName, mutexObj);
        service.PlantSort(PlantType.Fruits, plant => plant.Id, mutexObj);
        EndOfWorking();
        void EndOfWorking()
        {
            var busy = IsBusy(mutexObj);
            while (busy)
            {
                busy = IsBusy(mutexObj);
            }
            logger.OnEndOfWorking();
        }


    }

    private static void End(Mutex mutexObj)
    {
        
    }
    private static bool IsBusy(Mutex aMutex) {
        // Если занят то возвращает true
        var res = aMutex.WaitOne(0);
        if (res)
            aMutex.ReleaseMutex();  // если был свободен, то мы его заняли и нужно освободить
        return !res;
    }
    private static Config? ReadConfig()
    {
        Config? newConfig;
        var configFilePath = Directory.GetCurrentDirectory()[..^16] + "appsettings.json";
        if (!File.Exists(configFilePath))
        {
            newConfig = new Config(null)
            {
                Exception = new Exception($"There is no config file in directory {configFilePath}")
            };
            return newConfig;
        }
        var jsString = File.ReadAllText(configFilePath);
        try
        {
            newConfig = JsonSerializer.Deserialize<Config>(jsString);
        }
        catch (Exception e)
        {
            newConfig = new Config(null){Exception = e};
            return newConfig;
        }
        newConfig!.Exception = null;
        return newConfig;
    }
}