namespace DotnetLabVar17.Models;

public class Vegetable : Plant
{
    private double Calories { get;  } // калорийность

    public Vegetable(string plantName, string color, int calories) : base(plantName, color)
    {
        Calories = calories;
    }
}