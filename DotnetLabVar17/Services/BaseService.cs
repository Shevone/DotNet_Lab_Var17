using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Repository;
using DotnetLabVar17.Services.Interfaces;

namespace DotnetLabVar17.Services;

public class BaseService
{
    private DbRepository _dbRepository;
    private ILogger _logger;

    public BaseService(ILogger logger, DbRepository dbRepository)
    {
        _logger = logger;
        _dbRepository = dbRepository;
    }
    

    public void CreatePlant(Plant newPlant)
    {
        _dbRepository.CreatePlant(newPlant);
        _logger.OnPlantCreating(newPlant);
    }

    public void CreateFarmer(Farmer newFarmer)
    {
        try
        {
            _dbRepository.CreateFarmer(newFarmer);
        }
        catch (FarmerExistException e)
        {
           _logger.OnCustomException(e);
           return;
        }
        _logger.OnFarmerCreating(newFarmer);
    }

    public void CreateHarvest(Harvest newHarvest)
    {
        _dbRepository.CreateHarvest(newHarvest);
        _logger.OnHarvestCreating(newHarvest);
    }
    public void PlantSort<TKey>(PlantType plantType, Func<Plant, TKey> sortParam, Mutex mutex)
    {   
        new Thread(StartSort).Start();
        
        void StartSort()
        {
            mutex.WaitOne();
            _logger.OnPlantSorting(plantType);
            var countDisplacedElements = 0;
            switch (plantType)
            {
                case PlantType.Fruits:
                    countDisplacedElements = _dbRepository.SortVegetables(sortParam);
                    break;
                case PlantType.Berries :
                    countDisplacedElements = _dbRepository.SortFruits(sortParam);
                    break;
                case PlantType.Vegetables :
                    countDisplacedElements = _dbRepository.SortBerries(sortParam);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(plantType), plantType, null);
            }
            _logger.OnsSortEnd(countDisplacedElements);
            mutex.ReleaseMutex();
        }
    }
}
public enum PlantType{
    Vegetables,
    Fruits,
    Berries
}