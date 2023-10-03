namespace DotnetLabVar17.Models.OtherModels;

public class Harvest 
{
    public int Id { get; set; }
    private List<Farmer> Farmers { get; }
    public DateTime Date { get;}
    public Dictionary<Plant, int> HarvestedСrop { get;}

    public Harvest(Farmer harvestMan)
    {
        Farmers = new List<Farmer>();
        HarvestedСrop = new Dictionary<Plant, int>();
        Date = DateTime.Now;
    }
    public bool AddFarmer(Farmer farmer)
    {
        if (Farmers.Count == 10)
        {
            return false;
        }
        Farmers.Add(farmer);
        return true;
    }
    public bool RemoveFarmer(int farmerId)
    {
        foreach (var farmer in Farmers.Where(farmer => farmer.Id == farmerId))
        {
            Farmers.Remove(farmer);
            return true;
        }

        return false;
    }
}   