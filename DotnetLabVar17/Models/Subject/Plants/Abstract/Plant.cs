using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17.Models;
[Serializable]
// Атрибут необходим для сериализации в xml
public abstract class Plant
{
    // Абстркатный класс растение
    public int Id { get; set; }
    public string PlantName { get; set;} // Имя
    public string Color { get; set;} // Цвет
    public bool IsRipped { get; set;} // Уже собрали?
    public DateTime PlantingDate { get; set;} // Когда посадили

    protected Plant(string plantName, string color, DateTime plantingDate, bool isRipped)
    {
        PlantName = plantName;
        Color = color;
        IsRipped = isRipped;
        PlantingDate = plantingDate;
    }

    protected Plant()
    {
        
    }
}