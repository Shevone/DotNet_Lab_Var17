using DotnetLabVar17.Models;

namespace DotnetLabVar17.Repository;
// Интерфейс записывателя в файл
// Содержит 2 метода - дозавпись в файл и выгрузка из файла
public interface IFileWriter<T>
{
    void FileWrite(List<T> list);
    List<T>? LoadDataFromFile();
}