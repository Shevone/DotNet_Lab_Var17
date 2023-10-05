namespace DotnetLabVar17.Models.OtherModels;

public class Farmer 
{
    // Класс фермер
    public int Id { get; set; }
    public string Name { get;}
    public int HarvestCount { get; set;}

    public Farmer(string name)
    {
        Name = name;
        HarvestCount = 0;
    }
}