using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17.Services.Interfaces;

public interface ILogger
{
    //Интерфейс логера
    void OnPlantCreating(Plant newPlant);
    void OnFarmerCreating(Farmer newFarmer);
    void OnHarvestCreating(Harvest newHarvest);
    void OnPlantSorting(PlantType type);
    void OnStartWorking(Dictionary<string, int> dictionary);
    void OnEndOfWorking();
    void OnCustomException(FarmerExistException ex);
    void OnException(Exception exception);
    void OnSortStart();
    void OnSortEnd(PlantType type,int count);
    void OnFarmerToHarm(Harvest harv, Farmer farm);
    void OnPlantToHarv(Harvest harv, Plant plant, int count);
    void OnFarmerRemoveFromHarv(Harvest harvest, Farmer farmer);

    public void HarvestInfo(Harvest newHarvest);
    
}