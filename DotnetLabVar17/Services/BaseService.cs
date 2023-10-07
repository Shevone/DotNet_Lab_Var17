using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Repository;
using DotnetLabVar17.Services.Interfaces;

namespace DotnetLabVar17.Services;

// Класс сервиса их которого происходит вызов всей логики
public class BaseService 
{
    private DbRepository _dbRepository; // репозиторий бд
    private ILogger _logger; // Интерфейс логирования
    private Mutex _mutex; // Мьютекс для многопточных методов
    

    public BaseService(ILogger logger, DbRepository dbRepository)
    {
        _logger = logger;
        _dbRepository = dbRepository;
        Start();
    }
    public void AddPlantToHarvest(int harvestId, PlantType type, int plantId, int count)
    {
        var dict = _dbRepository.AddPlantToHarvest(harvestId,type,plantId,count);
        if (dict != null)
        {
            _logger.OnPlantToHarv((Harvest)dict["Harvest"], (Plant)dict["Plant"], count);
        }
    }

    public void AddFarmerToHarvest(int harvestId, int farmerId)
    {
        var dict =_dbRepository.AddFarmerToHarvest(harvestId,farmerId);
        _logger.OnFarmerToHarm((Harvest)dict["Harvest"], (Farmer)dict["Farmer"]);
    }

    public void RemoveFarmerFromHarv(int harvId, int farmerId)
    {
        var dict =_dbRepository.RemoveFarmerFromHarv(harvId, farmerId);
        if (dict != null)
        {
            _logger.OnFarmerRemoveFromHarv((Harvest)dict["Harvest"], (Farmer)dict["Farmer"]);
        }
    }

    public void HarvestInfo(int harvId)
    {
        var h = _dbRepository.GetHarvest(harvId);
        if(h != null) _logger.HarvestInfo(h);
        
    }
    // Передача нового растения и логрование 
    public void CreatePlant(Plant newPlant)
    {
        _dbRepository.CreatePlant(newPlant);
        _logger.OnPlantCreating(newPlant);
    }
    // Передача нового фермера и логирование
    public void CreateFarmer(Farmer newFarmer)
    {
        try
        {
            _dbRepository.CreateFarmer(newFarmer);
        }
        catch (FarmerExistException e)
        {
            // выброс пользовательского исключения
           _logger.OnCustomException(e);
           return;
        }
        _logger.OnFarmerCreating(newFarmer);
    }
    // Передача нового урожайного сбора и логирование
    public void CreateHarvest(Harvest newHarvest)
    {
        _dbRepository.CreateHarvest(newHarvest);
        _logger.OnHarvestCreating(newHarvest);
    }
    // Метод вызываемый на старте
    private void Start()
    {
        var countOfLoad = _dbRepository.LoadFromFile();
        _logger.OnStartWorking(countOfLoad);
        _mutex = new Mutex();
    }
    // Метож конца работы
    public void End()
    {
        // Ждем пока мьютекс освободится и только после этого завершаем етод
        var busy = IsBusy(_mutex);
        while (busy)
        {
            busy = IsBusy(_mutex);
        }
        _logger.OnEndOfWorking();
    }
    // Проверка на занятость мьютекса
    private static bool IsBusy(Mutex aMutex) {
        // Если занят то возвращает true
        var res = aMutex.WaitOne(0);
        if (res)
            aMutex.ReleaseMutex();  // если был свободен, то мы его заняли и нужно освободить
        return !res;
    }
    // Вызов сортировки в новом потоке
    public void  PlantSort<TKey>(PlantType plantType, Func<Plant, TKey> sortParam)
    {
       
        new Thread(StartSort).Start();// Открываем поток
        
        
        void StartSort()
        {
            _logger.OnPlantSorting(plantType);
            // Занмаем мьютекс
            _mutex.WaitOne();
            
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
            
            // Освобождаем мьютекс после выполнения
            _mutex.ReleaseMutex();
            _logger.OnSortEnd(plantType, countDisplacedElements);
            
            
        }
    }
        
}
public enum PlantType{
    Vegetables,
    Fruits,
    Berries
}