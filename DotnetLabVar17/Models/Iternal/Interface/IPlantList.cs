namespace DotnetLabVar17;

public interface IPlantList<T>
{
    public string Type { get; set; }
    public void Add(T item);
    public void Remove(T item);
    public int MySort<TKey>(Func<T, TKey> keySelector);
}