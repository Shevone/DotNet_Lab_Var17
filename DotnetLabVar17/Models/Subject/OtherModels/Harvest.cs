namespace DotnetLabVar17.Models.OtherModels;

public class Harvest 
{
    // Класс Уроожай
    public int Id { get; set; }
    private List<Farmer> Farmers { get; }
    public DateTime Date { get;}
    public Dictionary<Plant, int> HarvestedСrop { get;}// Словарь - key : растени и value: количество сборов

    public Harvest(Farmer harvestMan)
    {
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
        return true;
    }
    //Удаление фермера из сбора
    public bool RemoveFarmer(int farmerId)
    {
        foreach (var farmer in Farmers.Where(farmer => farmer.Id == farmerId))
        {
            Farmers.Remove(farmer);
            return true;
        }

        return false;
    }
    // Добавление растения в урожайный сбор
    public bool AddPlant(Plant plant, int count)
    {
        if (HarvestedСrop.Keys.Contains(plant)) return false;
        HarvestedСrop.Add(plant, count);
        return true;
    }
}   