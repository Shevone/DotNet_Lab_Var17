namespace DotnetLabVar17;

public interface IPlantList<T>
{
    // Интерфейс кастомной коллекции
    public string Type { get; set; }
    public void Add(T item);
    public void Remove(T item);
    public int MySort<TKey>(Func<T, TKey> keySelector);
    public int Load();
    public T? Find(int id);
}