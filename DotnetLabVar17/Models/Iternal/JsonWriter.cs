using System.Text.Json;
using DotnetLabVar17.Models;

namespace DotnetLabVar17.Repository;

public class JsonWriter<T> : IFileWriter<T>
{
    private readonly string _filePath;// Путь до файла записи

    public JsonWriter(string filePath)
    {
        _filePath = filePath;
    }
    public void FileWrite(List<T> list)
    {
        // Сериализация и запись v json
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsoString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(_filePath, jsoString);
    }

    public List<T>? LoadDataFromFile()
    {
        try
        {
            // При ошибке десериализации перезаписываем файл на пустой и возвращаем null
            // Перезаписываем файл для того чтобы убрать ошибку де сериализации в будущем
            var jsString = File.ReadAllText(_filePath);
            var jsonList = JsonSerializer.Deserialize<List<T>>(jsString);
            return jsonList;
        }
        catch (Exception e)
        {
            File.Create(_filePath).Close();
        }
        return null;
    }
}