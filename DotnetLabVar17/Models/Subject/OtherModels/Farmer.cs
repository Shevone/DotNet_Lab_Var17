namespace DotnetLabVar17.Models.OtherModels;

public class Farmer 
{
    // Класс фермер
    private static int _freeId = 0;

    public static int SetNextId
    {
        get
        {
            if (_freeId == 0)
            {
                _freeId = 1;
            }
            var i = _freeId;
            _freeId = i + 1;
            return i;
        }
        set => _freeId = value >= 1 ? value : 1;
    }

    public  int Id { get; set; }
    public string Name { get;}
    public int HarvestCount { get; set;}

    public Farmer(string name)
    {
        Id = SetNextId;
        Name = name;
        HarvestCount = 0;
    }
}