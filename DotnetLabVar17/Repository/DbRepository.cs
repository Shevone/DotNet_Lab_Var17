using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17.Repository;

public class DbRepository
{
    private List<Harvest> Harvests { get;}
    private List<Farmer> Farmers { get;}
    private IPlantList<Berry> Berries { get;  }
    private IPlantList<Fruit> Fruits { get;  }
    private IPlantList<Vegetable> Vegetables { get; }

    public DbRepository(IPlantList<Berry> berries, IPlantList<Fruit> fruits, IPlantList<Vegetable> vegetables)
    {
        Berries = berries;
        Fruits = fruits;
        Vegetables = vegetables;

        Harvests = new List<Harvest>();
        Farmers = new List<Farmer>();
       
    }

    public void CreateHarvest(Harvest newHarvest)
    {
        Harvests.Add(newHarvest);
    }

    public void CreateFarmer(Farmer newFarmer)
    {
        if (Farmers.FirstOrDefault(f => f.Name == newFarmer.Name) != null)
        {
            throw new FarmerExistException("Farmer with the same name already exist", newFarmer);
        }
        Farmers.Add(newFarmer);
    }

    public void CreatePlant(Plant newPlant)
    {
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
    public int SortFruits<TKey>(Func<Plant, TKey> sortParam)
    {
        return Fruits.MySort(sortParam);
    }
    public int SortVegetables<TKey>(Func<Plant, TKey> sortParam)
    {
         return Fruits.MySort(sortParam);
    }
    public int SortBerries<TKey>(Func<Plant, TKey> sortParam)
    {
        return Fruits.MySort(sortParam);
    }
    public void AddFarmerToHarvest(int harvestId, int farmerId)
    {
        var curHarv = Harvests.FirstOrDefault(x => x.Id == harvestId);
        var curFarmer = Farmers.FirstOrDefault(x => x.Id == farmerId);
        if (curFarmer != null && curHarv != null)
        {
            curHarv.AddFarmer(curFarmer);
        }
    }
}