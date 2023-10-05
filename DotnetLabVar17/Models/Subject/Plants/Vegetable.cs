namespace DotnetLabVar17.Models;
[Serializable]
// Атрибут необходим для сериализации в xml
public class Vegetable : Plant
{
    public double Calories { get; set;  } // калорийность

    public Vegetable()
    {
        
    }
    public Vegetable(string plantName, string color, DateTime plantingDate, bool isRipped, int calories) : base(plantName, color, plantingDate, isRipped)
    {
        Calories = calories;
    }
}