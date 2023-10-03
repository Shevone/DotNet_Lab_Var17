using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17.Models;

public abstract class Plant
{
    public int Id { get; set; }
    public string PlantName { get;}
    public string Color { get; }

    public bool IsRipped { get; }
    public DateTime PlantingDate { get;}
    
    protected Plant(string plantName, string color)
    {
        PlantName = plantName;
        Color = color;
        IsRipped = false;
        PlantingDate = DateTime.Now;
    }
}