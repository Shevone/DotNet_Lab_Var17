using System.Collections;
using System.Xml;
using DotnetLabVar17;
using DotnetLabVar17.Models;
using DotnetLabVar17.Repository;
using Newtonsoft.Json;

namespace Lab17Test;


[TestFixture]
// Атрибут означает что класс содержит автотесты
// Фактически это тест сьют
public class Tests
{
    private List<Fruit> fruits;
    private string FilePath;
    
    // Перед выполнением каждого теста
    [SetUp]
    public void Setup()
    {
        fruits = new List<Fruit>
        {
            new Fruit("Яблоко", "зеленый", DateTime.Now, false, "кисло-сладкий", true),
            new Fruit("Груша", "зеленый", DateTime.Now, false, "Сладкий", true),
            new Fruit("Апельсин", "оражевый", DateTime.Now, false,"кисло-сладкий", true),
            new Fruit("банан", "желтый", DateTime.Now, false,"вкусный", true),
            new Fruit("Лимон", "желтый", DateTime.Now, false,"кислючий", true),
            new Fruit("Киви", "зеленый", DateTime.Now, false,"вкусный", true),
            new Fruit("Персик", "рыжий", DateTime.Now, false,"выяжет", true),
            new Fruit("Кокос", "коричнево-белый", DateTime.Now, false,"странный", true),
            new Fruit("Фидж", "фиолетовый", DateTime.Now, false,"непонятный", true),
            new Fruit("Дракон-фрукт", "розовый", DateTime.Now, false,"неизвестно", true)
        };
        FilePath = Directory.GetCurrentDirectory()[..^16] + @"CustomCollectionTests\\staticFiles\\";
        Directory.Delete(FilePath,true);
        Directory.CreateDirectory(FilePath);

    }
    // После выполнения каждого теста
    [TearDown]
    public void AfterTest()
    {
        fruits = new List<Fruit>();
    }

    [Test]
    // Корректно ли опеределяется тип 
    public void PlantTypeTest()
    {
        var fruitL = new PlantList<Fruit>(FilePath + "_test1F.json");
        var vegL = new PlantList<Vegetable>(FilePath + "_test1V.json" );
        var berL = new PlantList<Berry>(FilePath+"_test1B.json");
        Assert.AreEqual(fruitL.TypeOfPlant, typeof(Fruit));
        Assert.AreEqual(vegL.TypeOfPlant, typeof(Vegetable));
        Assert.AreEqual(berL.TypeOfPlant, typeof(Berry));
        
    }
    // Включает в себя 3 теста xml wirter, json writer, не корректное название файла
    // Тест выбора класса - записывающего в файл
    [Test]
    public void JsonTest()
    {
       
        // Передаём в качестве параметра файл с расширением json
        var jsonList = new PlantList<Fruit>(FilePath+"_test2.json");
        Assert.That(typeof(JsonWriter<Fruit>), Is.EqualTo(jsonList.FileWriter));
    }
    [Test]
    public void XmlTest()
    {
       
        // Пердаём в качестве параметра xml файл
        var xmlFilePAth = FilePath + "_test2.xml";
        var xmlList = new PlantList<Fruit>(xmlFilePAth);
        Assert.That(typeof(XmlWriter<Fruit>), Is.EqualTo(xmlList.FileWriter));
    }

    [Test]
    public void WrongNameFileFileWriter()
    {
        // Передаём в качестве параметра рандомный набор букв
        var rndList = new PlantList<Fruit>("dsadas");
        Assert.That(typeof(JsonWriter<Fruit>), Is.EqualTo(rndList.FileWriter));
    }
    [Test]
    // Проверка правильности работы FileWriter - а
    public void FileWriterWorkJson()
    {
      
        var list = new PlantList<Fruit>(FilePath+"_test3.json");
        foreach (var fruit in fruits)
        {
            // При добавлении коллекция записывается в файл
            list.Add(fruit);
        }
        // Считываем коллекцию из файла и сравниваем её элементы с исходной через поля
        var listFromFile = new PlantList<Fruit>(FilePath+"_test3.json");
        listFromFile.Load();
        for (var i = 0; i < list.Count(); i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(list.List[i].PlantName, Is.EqualTo(listFromFile.List[i].PlantName));
                Assert.That(list.List[i].Id, Is.EqualTo(listFromFile.List[i].Id));
                Assert.That(list.List[i].Taste, Is.EqualTo(listFromFile.List[i].Taste));
                Assert.That(list.List[i].Color, Is.EqualTo(listFromFile.List[i].Color));
            });
        }
    }
    [Test]
    public void FileWriterWorkXml()
    {
        var curFilePath = FilePath + "_test3.xml";
        var list = new PlantList<Fruit>(curFilePath);
        foreach (var fruit in fruits)
        {
            // При добавлении коллекция записывается в файл
            list.Add(fruit);
        }
        // Считываем коллекцию из файла и сравниваем её элементы с исходной через поля
        var listFromFile = new PlantList<Fruit>(curFilePath);
        listFromFile.Load();
        for (var i = 0; i < list.Count(); i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(list.List[i].PlantName, Is.EqualTo(listFromFile.List[i].PlantName));
                Assert.That(list.List[i].Id, Is.EqualTo(listFromFile.List[i].Id));
                Assert.That(list.List[i].Taste, Is.EqualTo(listFromFile.List[i].Taste));
                Assert.That(list.List[i].Color, Is.EqualTo(listFromFile.List[i].Color));
            });
        }
    }
    [Test]
    public void AddTest()
    {
       
        // Тест добавления
        // Проверяем что добавленные элементы равны
        var list = new PlantList<Fruit>(FilePath + "_test4.json");
        foreach (var fruit in fruits)
        {
            list.Add(fruit);
        }
        //
        Assert.Multiple(() =>
        {
            Assert.That(fruits[0], Is.EqualTo(list.List[0]));
            Assert.That(fruits[9], Is.EqualTo(list.List[9]));
        });
       
    }
    [Test]
    public void RemoveTest()
    {
       
        // Тест Удаления
        // Проверяем что после удаление длинна одинаковая
        var list = new PlantList<Fruit>(FilePath+"_test5.json");
        foreach (var fruit in fruits)
        {
            list.Add(fruit);
        }
        
        list.Remove(list.List[8]);
        fruits.Remove(fruits[8]);
        Assert.That(fruits, Has.Count.EqualTo(list.Count()));
    }
    [Test]
    public void FindTest()
    {
       
        // Тест поиска по массиву
        // Проверяем что поиск работает корректно
        var list = new PlantList<Fruit>(FilePath+"_test6.json");
        var id = 1;
        foreach (var fruit in fruits)
        {
            fruit.Id = id;
            id++;
            list.Add(fruit);
        }
        var rnd = new Random();
        var number = rnd.Next(0, 9);
        // У элемента кастомного списка с индеском нашего рандомного числа берем ID
        var findId = list.List[number].Id;
        var findFruit = list.Find(findId);
        Assert.That(fruits.FirstOrDefault(f => f.Id==findId), Is.EqualTo(findFruit));
    }
    [Test]
    public void IdTest()
    {
      
        // Тест Корректой записи ID
        //
        var list = new PlantList<Fruit>(FilePath+"_test7.json");
        foreach (var fruit in fruits)
        {
            list.Add(fruit);
        }
        // Берем рандомное число
        var rnd = new Random();
        var number = rnd.Next(0, 9);
        // У элемента кастомного списка с индеском нашего рандомного числа берем ID
        var removeId = list.List[number].Id;
        // Достаём сам фрукт
        var fruitToDel = list.Find(removeId);
        // Удаляем фрукт из списка
        list.Remove(fruitToDel!);
        // Добавляем новый фрукт из списка в классе теста
        list.Add(fruits[number]);
        // Присвоение ID устроено таким образом, что при удалении элемнта из списка его id освобождается
        // Этот Id становится свободны для того чтобы новом, добавленому элемнту было рисвоено это Id
        
        Assert.That(removeId, Is.EqualTo(list.List.Last().Id));
    }
    [Test]
    public void SortTest()
    {
       
        // Тест сортировки
        // Проверяем что сортировка работает корректно
        var list = new PlantList<Fruit>(FilePath+"_sortTest.json");
        // Cписок имён
        var nameList = new List<string>();
        foreach (var fruit in fruits)
        {
            nameList.Add(fruit.PlantName);
            list.Add(fruit);
        }
        // Сортируем список имён
        nameList.Sort();
        // Сортируем  фрукты по имени
        list.MySort(x => x.PlantName);
        var rnd = new Random();
        var number = rnd.Next(0, 9);
        // Проверяем, выбрав рандомный фрукт по индексу,
        // что его название будет равно, выбранному
        // по тому же индексу, элементу из списка имён
        Assert.That(nameList[number], Is.EqualTo(list.List[number].PlantName));
    }

    
}