using System.Xml.Serialization;
using DotnetLabVar17.Models;

namespace DotnetLabVar17.Repository;

public class XmlWriter<T> : IFileWriter<T>
{
    public XmlWriter(string filePath)
    {
        _filePath = filePath;
    }

    private readonly string _filePath; // Путь до файла

    public void FileWrite(List<T> list)
    {
        // Сериализация и запись в xml
        var xml = new XmlSerializer(typeof(List<T>));
        using var fs = new FileStream(_filePath, FileMode.OpenOrCreate);
        xml.Serialize(fs, list);
    }

    public List<T>? LoadDataFromFile()
    {
        try
        {
            // При ошибке десериализации перезаписываем файл на пустой и возвращаем null
            // Перезаписываем файл для того чтобы убрать ошибку де сериализации в будущем
            var xml = new XmlSerializer(typeof(List<T>));
            using var fs = new FileStream(_filePath, FileMode.OpenOrCreate);
            var xmlList = (List<T>)xml.Deserialize(fs)!;
            return xmlList;
        }
        catch (Exception e)
        {
            File.Create(_filePath).Close();
        }

        return null;

    }
}  