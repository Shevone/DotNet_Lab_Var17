using System.Collections;
using DotnetLabVar17.Models;

namespace DotnetLabVar17;

public class PlantList<T> : IPlantList<T>, IEnumerable<T> where T : Plant
{
    private readonly List<T> _list;
    private readonly Stack<int> _id;
  
    public List<T> List => new (_list);
    public string Type { get; set; }
    public PlantList()
    {
        _list = new List<T>();
        _id = new Stack<int>();
        _id.Push(1);
        Type = typeof(T).ToString();
    } 
    public void Add(T item)
    {
        item.Id = _id.Pop();
        _id.Push(item.Id + 1); 
        _list.Add(item);
    }

    public void Remove(T item)
    {
        _id.Push(item.Id);
        _list.Remove(item);
    }

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

    private static bool FirstBiggerSecond<TKey>(TKey a, TKey b)
    {
        if (typeof(TKey) != typeof(int)) return string.Compare(a?.ToString(), b?.ToString()) == 1;
        var i1 = Convert.ToInt32(a);
        var i2 = Convert.ToInt32(b);
        return i1 > i2;
    }
    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
}