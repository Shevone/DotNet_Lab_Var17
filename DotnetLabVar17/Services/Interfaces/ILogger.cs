using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17.Services.Interfaces;

public interface ILogger
{
    void OnPlantCreating(Plant newPlant);
    void OnFarmerCreating(Farmer newFarmer);
    void OnHarvestCreating(Harvest newHarvest);
    void OnPlantSorting(PlantType type);
    void OnStartWorking();
    void OnEndOfWorking();
    void OnCustomException(FarmerExistException ex);
    void OnException(Exception exception);
    void OnSortStart();
    void OnsSortEnd(int count);
}