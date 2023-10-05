using DotnetLabVar17.Models.OtherModels;

namespace DotnetLabVar17;

public class FarmerExistException : Exception
{
    // Пользовательская ошибка, вызываемая при попытке добавления фермера со схожем именем
    public Farmer Farmer { get; }
    public FarmerExistException(string message, Farmer farmer) : base(message)
    {
        Farmer = farmer;
    }

   
}