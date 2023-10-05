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
    public void Start()
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
    public void PlantSort<TKey>(PlantType plantType, Func<Plant, TKey> sortParam)
    {
        new Thread(StartSort).Start();// Открываем поток
        
        void StartSort()
        {
            // Занмаем мьютекс
            _mutex.WaitOne();
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
            _logger.OnSortEnd(countDisplacedElements);
            // Освобождаем мьютекс после выполнения
            _mutex.ReleaseMutex();
        }
    }
    
}
public enum PlantType{
    Vegetables,
    Fruits,
    Berries
}