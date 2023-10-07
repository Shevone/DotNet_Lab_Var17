namespace DotnetLabVar17.Models.OtherModels;

public class Harvest 
{
    // Класс Уроожай
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
    public int Id { get; set; }
    private List<Farmer> Farmers { get; }
    public DateTime Date { get;}
    public Dictionary<Plant, int> HarvestedСrop { get;}// Словарь - key : растени и value: количество сборов

    public Harvest()
    {
        Id = SetNextId;
        Farmers = new List<Farmer>();
        HarvestedСrop = new Dictionary<Plant, int>();
        Date = DateTime.Now;
    }
    // Добавление фермера в сбор
    public bool AddFarmer(Farmer farmer)
    {
        if (Farmers.Count == 10)
        {
            return false;
        }
        Farmers.Add(farmer);
        farmer.HarvestCount++;
        return true;
    }
    //Удаление фермера из сбора
    public bool RemoveFarmer(int farmerId)
    {
        foreach (var farmer in Farmers.Where(farmer => farmer.Id == farmerId))
        {
            Farmers.Remove(farmer);
            farmer.HarvestCount--;
            return true;
        }
        return false;
    }
    // Добавление растения в урожайный сбор
    public bool AddPlant(Plant plant, int count)
    {
        if (HarvestedСrop.ContainsKey(plant)) return false;
        HarvestedСrop.Add(plant, count);
        return true;
    }
}   