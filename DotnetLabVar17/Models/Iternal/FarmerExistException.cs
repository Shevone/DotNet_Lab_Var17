using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17;

public class FarmerExistException : Exception
{
    public Farmer Farmer { get; }
    public FarmerExistException(string message, Farmer farmer) : base(message)
    {
        Farmer = farmer;
    }

   
}