using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Services;

namespace DotnetLabVar17.Repository;

public class DbRepository
{
    private List<Harvest> Harvests { get;}
    private List<Farmer> Farmers { get;}
    private IPlantList<Berry?> Berries { get;  }
    private IPlantList<Fruit> Fruits { get;  }
    private IPlantList<Vegetable> Vegetables { get; }

    public DbRepository(Config config)
    {
        Berries = new PlantList<Berry?>(config.BerriesPath!);
        Fruits =  new PlantList<Fruit>(config.FruitsPath!);
        Vegetables =  new PlantList<Vegetable>(config.VegetablePath!);
        
        Harvests = new List<Harvest>();
        Farmers = new List<Farmer>();
       
    }
    // Метод вызвающийся при старте программы для считывания из xml
    public Dictionary<string, int> LoadFromFile()
    {
        // Отдаем словарь - количество загруженных данных
        var dict = new Dictionary<string, int>
        {
            { "Fruits", Fruits.Load() },
            { "Vegetables", Vegetables.Load() },
            { "Berries", Berries.Load() }
        };
        return dict;
    }
    // Создание урожая
    public void CreateHarvest(Harvest newHarvest)
    {
        Harvests.Add(newHarvest);
    }
    // Создание нового фермера
    public void CreateFarmer(Farmer newFarmer)
    {
        // если фермер с таким именем существет, то выбрасываем исключение
        if (Farmers.FirstOrDefault(f => f.Name == newFarmer.Name) != null)
        {
            throw new FarmerExistException("Farmer with the same name already exist", newFarmer);
        }
        Farmers.Add(newFarmer);
    }
    // Добавление в бд нового растения
    public void CreatePlant(Plant newPlant)
    {
        // В зависимости от типа кидаем в опред. кастомную коллекцию 
        switch (newPlant)
        {
            case Berry berry:
                Berries.Add(berry);
                return;
            case Vegetable plant:
                Vegetables.Add(plant);
                return;
            case Fruit fruit:
                Fruits.Add(fruit);
                return;
        }
    }
    // Передача сортировки передается на слой ниже в репозиторий
    // Вызвов сортровки фруктов
    public int SortFruits<TKey>(Func<Plant, TKey> sortParam)
    {
        return Fruits.MySort(sortParam);
    }
    // Вызвов сортровки овощей
    public int SortVegetables<TKey>(Func<Plant, TKey> sortParam)
    {
         return Fruits.MySort(sortParam);
    }
    // Вызвов сортровки Ягод
    public int SortBerries<TKey>(Func<Plant, TKey> sortParam)
    {
        return Fruits.MySort(sortParam);
    }
    // ДОбавление фермера в сбор урожая
    public void AddFarmerToHarvest(int harvestId, int farmerId)
    {
        var curHarv = Harvests.FirstOrDefault(x => x.Id == harvestId);
        var curFarmer = Farmers.FirstOrDefault(x => x.Id == farmerId);
        if (curFarmer != null && curHarv != null)
        {
            curHarv.AddFarmer(curFarmer);
        }
    }
    // Добваление растения в урожай
    public void AddPlantToHarvest(int harvestId, PlantType type, int plantId, int count)
    {
        var curHarv = Harvests.FirstOrDefault(x => x.Id == harvestId);
        Plant? curPlant = type switch
        {
            PlantType.Berries => Berries.Find(plantId),
            PlantType.Fruits => Fruits.Find(plantId),
            PlantType.Vegetables => Vegetables.Find(plantId),
            _ => null
        };
        if (curPlant == null || curHarv == null) return;
        curHarv.AddPlant(curPlant, count);
    }
}