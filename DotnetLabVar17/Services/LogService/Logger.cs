using System.Text;
using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Services.Interfaces;

namespace DotnetLabVar17.Services;
/// <summary>
/// Класс логирования
/// </summary>
public class Logger : ILogger
{
    public delegate void EventHandler(string message);

    public event EventHandler? PrintMethods; // Событие - лог
    public void OnPlantCreating(Plant newPlant)
    {
        // Событие по добавлению в список Растения
        PrintMethods?.Invoke($"{DateTime.Now}| Посажен(о) {newPlant.PlantName} ,присвоен id:{newPlant.Id}, цвет {newPlant.Color}");
    }

    public void OnFarmerCreating(Farmer newFarmer)
    {
        // Событие по добавлению в список фермера
        PrintMethods?.Invoke($"{DateTime.Now}| Добавлен фермер {newFarmer.Name}, присвоен id : {newFarmer.Id}");
    }

    public void OnHarvestCreating(Harvest newHarvest)
    {
        // Создание урожайного сбора
        var resultString = new StringBuilder();
        resultString.Append("==============================\n");
        resultString.Append($"{newHarvest.Date}| Собарн новый урожай: {newHarvest.Id}:\n");
        foreach (var crop in newHarvest.HarvestedСrop)
        {
            resultString.Append($"{crop.Key.PlantName} - {crop.Value}\n");
        }
        resultString.Append("==============================\n");
        PrintMethods?.Invoke(resultString.ToString());
    }
    public void OnPlantSorting(PlantType type)
    {
        // Сортировка кастомной коллекции
        var resString = $"{DateTime.Now}| Cтарт сортировки списка {type.ToString()} ";
        PrintMethods?.Invoke(resString);
    }
    public void OnStartWorking(Dictionary<string, int> dictionary)
    {
        // Старт приложения
        var sb = new StringBuilder();
        foreach (var key in dictionary.Keys)
        {
            sb.Append($"\n{key} : {dictionary[key]}");
        }
        var resString = $"\n{DateTime.Now}| Программа запущена. Загружено  из файла:" +
                        $"{sb}";
        PrintMethods?.Invoke(resString);
        
    }
    public void OnEndOfWorking()
    {
        // Конец работы приложения
        var resString = $"{DateTime.Now}| Программа завершила работу\n";
        PrintMethods?.Invoke(resString);
    }

    public void OnCustomException(FarmerExistException ex)
    {
        // Метод вызываемый при вылете кастомной ошибки(наличие фермера в бд)
        var resStr = $"{DateTime.Now}| {ex.Message}: {ex.Farmer.Name}";
        PrintMethods?.Invoke(resStr);
    }

    public void OnException(Exception exception)
    {
        // Метод вызываемыый при вылете обыкновенной ошибки
        var res = $"{DateTime.Now}| {exception.Message}";
        PrintMethods?.Invoke(res);
    }

    public void OnSortStart()
    {
        // Логировние начала сортировки кастомной коллекции
        var res =  $"{DateTime.Now}| Начало сортировки";
        PrintMethods?.Invoke(res);
    }

    public void OnSortEnd(int count)
    {
        // Логирование завершения сортировки кастомной коллекции
        // Принимает на вход количесвто переставленных элементов
        var res =  $"{DateTime.Now}| Конец сортировки сортировки. Перемещено элементов: {count}";
        PrintMethods?.Invoke(res);
    }
}