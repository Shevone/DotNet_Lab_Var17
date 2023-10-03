namespace DotnetLabVar17.Models;

public class Berry : Plant
{
    private bool IsMedicinal { get; set; } // полезная ли для лечения(например смородина)
    private bool Sour { get; set; } // кислый
    private bool Sweet { get;set; } // сладкий

    public Berry(string plantName, string color, bool isMedicinal, bool sour, bool sweet) : base(plantName, color)
    {
        IsMedicinal = isMedicinal;
        Sour = sour;
        Sweet = sweet;
    }
}