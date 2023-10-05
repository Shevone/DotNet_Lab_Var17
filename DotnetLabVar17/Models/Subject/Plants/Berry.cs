namespace DotnetLabVar17.Models;
[Serializable]
// Атрибут необходим для сериализации в xml
public class Berry : Plant
{
    public bool IsMedicinal { get; set; } // полезная ли для лечения(например смородина)
    public bool Sour { get; set; } // кислый
    public bool Sweet { get; set; } // сладкий

    public Berry()
    {
        
    }
    public Berry(string plantName, string color, DateTime plantingDate, bool isRipped, bool isMedicinal,  bool sour, bool sweet) : base(plantName, color,plantingDate,isRipped)
    {
        IsMedicinal = isMedicinal;
        Sour = sour;
        Sweet = sweet;
    }
}