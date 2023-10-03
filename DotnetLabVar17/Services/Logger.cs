using System.Text;
using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;
using DotnetLabVar17.Services.Interfaces;

namespace DotnetLabVar17.Services;

public class Logger : ILogger
{
    public delegate void EventHandler(string message);

    public event EventHandler? PrintMethods;
    public void OnPlantCreating(Plant newPlant)
    {
        PrintMethods?.Invoke($"{DateTime.Now}| Посажен(о) {newPlant.PlantName} ,присвоен id:{newPlant.Id}, цвет {newPlant.Color}");
    }

    public void OnFarmerCreating(Farmer newFarmer)
    {
        PrintMethods?.Invoke($"{DateTime.Now}| Добавлен фермер {newFarmer.Name}, присвоен id : {newFarmer.Id}");
    }

    public void OnHarvestCreating(Harvest newHarvest)
    {
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
        var resString = $"{DateTime.Now}| Cтарт сортировки списка {type.ToString()} ";
        PrintMethods?.Invoke(resString);
    }
    public void OnStartWorking()
    {
        var resString = $"\n{DateTime.Now}| Программа запущена";
        PrintMethods?.Invoke(resString);
    }
    public void OnEndOfWorking()
    {
        
        var resString = $"{DateTime.Now}| Программа завершила работу\n";
        PrintMethods?.Invoke(resString);
    }

    public void OnCustomException(FarmerExistException ex)
    {
        var resStr = $"{DateTime.Now}| {ex.Message}: {ex.Farmer.Name}";
        PrintMethods?.Invoke(resStr);
    }

    public void OnException(Exception exception)
    {
        var res = $"{DateTime.Now}| {exception.Message}";
        PrintMethods?.Invoke(res);
    }

    public void OnSortStart()
    {
        var res =  $"{DateTime.Now}| Начало сортировки";
        PrintMethods?.Invoke(res);
    }

    public void OnsSortEnd(int count)
    {
        var res =  $"{DateTime.Now}| Конец сортировки сортировки. Перемещено элементов: {count}";
        PrintMethods?.Invoke(res);
    }
}