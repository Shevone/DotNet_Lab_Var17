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
    void OnSortEnd(int count);
}