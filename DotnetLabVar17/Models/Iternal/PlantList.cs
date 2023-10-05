using System.Collections;
using DotnetLabVar17.Models;
using DotnetLabVar17.Repository;

namespace DotnetLabVar17;

public class PlantList<T> : IPlantList<T>, IEnumerable<T> where T : Plant
{
    // Кастомная коллекция содержит список элментов, стек для выделения id а так же filewriter
    private readonly List<T> _list;
    private readonly Stack<int> _id;

    private readonly IFileWriter<T>? _fileWriter;
    public List<T> List => new (_list);
    public string Type { get; set; }
    public PlantList(string filePath)
    {
        // В зависиости от расширения файла выбираем в какой тип будем записывтаь
        if (filePath[^5..].ToLower() == ".json")
        {
            _fileWriter = new JsonWriter<T>(filePath);
        }
        else if (filePath[^4..].ToLower() == ".xml")
        {
            _fileWriter = new XmlWriter<T>(filePath);
        }
        _list = new List<T>();
        _id = new Stack<int>();
        _id.Push(1);
        Type = typeof(T).ToString();
    } 
    // Добавление
    public void Add(T item)
    {
        item.Id = _id.Pop();
        _id.Push(item.Id + 1); 
        _list.Add(item);
        _fileWriter?.FileWrite(List);
    }
    //Удаление
    public void Remove(T item)
    {
        _id.Push(item.Id);
        _list.Remove(item);
        _fileWriter?.FileWrite(List);
    }
    // Загрузка из файла
    public int Load()
    {
        var dataFromFile = _fileWriter?.LoadDataFromFile();
        if (dataFromFile == null) return 0;
        _list.AddRange(dataFromFile);
        _id.Pop();
        _id.Push(_list[^1].Id+1);
        return dataFromFile.Count;

    }

    public T? Find(int id)
    {
        return _list.FirstOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Сортировка кастомной коллекции
    ///
    /// Реаллизована быстрая сортировка, с подсчётом количества перемеений элементтов
    /// Параметр для сортировки передаётся через лямбда выражение
    /// </summary>
    public int MySort<TKey>(Func<T, TKey> keySelectedFunc)
    {
        var count = 0;
        var len = _list.Count;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                var a = keySelectedFunc(_list[j]);
                var b = keySelectedFunc(_list[j + 1]);
                if (FirstBiggerSecond(a, b))
                {
                    (_list[j], _list[j + 1]) = (_list[j + 1], _list[j]);
                    count++;
                }
            }
        }

        return count;
    }
    // Костыльное сравнение элментов
    // Не смог привести элементы к одному типу - поэтому сравниваем их либо как int либо как string
    private static bool FirstBiggerSecond<TKey>(TKey a, TKey b)
    {
        if (typeof(TKey) != typeof(int)) return string.Compare(a?.ToString(), b?.ToString()) == 1;
        var i1 = Convert.ToInt32(a);
        var i2 = Convert.ToInt32(b);
        return i1 > i2;
    }
    // Enumerator
    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
}