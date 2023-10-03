namespace DotnetLabVar17.Models;

public class Fruit : Plant
{
    private string Taste { get; } // вкус
    private bool IsEdiblePeel { get;  } // съедованя ли кожура

    public Fruit(string plantName, string color, string taste, bool isEdiblePeel) : base(plantName, color)
    {
        Taste = taste;
        IsEdiblePeel = isEdiblePeel;
    }
}   