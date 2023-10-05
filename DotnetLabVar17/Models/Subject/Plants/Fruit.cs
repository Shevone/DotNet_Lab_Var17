namespace DotnetLabVar17.Models;
[Serializable]
// Атрибут необходим для сериализации в xml
public class Fruit : Plant
{
    public string Taste { get; set; } // вкус
    public bool IsEdiblePeel { get; set; } // съедованя ли кожура

    public Fruit()
    {
        
    }
    public Fruit(string plantName, string color, DateTime plantingDate, bool isRipped, string taste, bool isEdiblePeel) : base(plantName, color, plantingDate, isRipped)
    {
        Taste = taste;
        IsEdiblePeel = isEdiblePeel;
    }
    
}   