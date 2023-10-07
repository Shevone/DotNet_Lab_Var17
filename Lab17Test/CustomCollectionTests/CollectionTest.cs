using System.Collections;
using System.Xml;
using DotnetLabVar17;
using DotnetLabVar17.Models;
using DotnetLabVar17.Repository;
using Newtonsoft.Json;

namespace Lab17Test;


[TestFixture]
// ������� �������� ��� ����� �������� ���������
// ���������� ��� ���� ����
public class Tests
{
    private List<Fruit> fruits;
    private string FilePath;
    
    // ����� ����������� ������� �����
    [SetUp]
    public void Setup()
    {
        fruits = new List<Fruit>
        {
            new Fruit("������", "�������", DateTime.Now, false, "�����-�������", true),
            new Fruit("�����", "�������", DateTime.Now, false, "�������", true),
            new Fruit("��������", "��������", DateTime.Now, false,"�����-�������", true),
            new Fruit("�����", "������", DateTime.Now, false,"�������", true),
            new Fruit("�����", "������", DateTime.Now, false,"��������", true),
            new Fruit("����", "�������", DateTime.Now, false,"�������", true),
            new Fruit("������", "�����", DateTime.Now, false,"������", true),
            new Fruit("�����", "���������-�����", DateTime.Now, false,"��������", true),
            new Fruit("����", "����������", DateTime.Now, false,"����������", true),
            new Fruit("������-�����", "�������", DateTime.Now, false,"����������", true)
        };
        FilePath = Directory.GetCurrentDirectory()[..^16] + @"CustomCollectionTests\\staticFiles\\";
        Directory.Delete(FilePath,true);
        Directory.CreateDirectory(FilePath);

    }
    // ����� ���������� ������� �����
    [TearDown]
    public void AfterTest()
    {
        fruits = new List<Fruit>();
    }

    [Test]
    // ��������� �� ������������� ��� 
    public void PlantTypeTest()
    {
        var fruitL = new PlantList<Fruit>(FilePath + "_test1F.json");
        var vegL = new PlantList<Vegetable>(FilePath + "_test1V.json" );
        var berL = new PlantList<Berry>(FilePath+"_test1B.json");
        Assert.AreEqual(fruitL.TypeOfPlant, typeof(Fruit));
        Assert.AreEqual(vegL.TypeOfPlant, typeof(Vegetable));
        Assert.AreEqual(berL.TypeOfPlant, typeof(Berry));
        
    }
    // �������� � ���� 3 ����� xml wirter, json writer, �� ���������� �������� �����
    // ���� ������ ������ - ������������� � ����
    [Test]
    public void JsonTest()
    {
       
        // ������� � �������� ��������� ���� � ����������� json
        var jsonList = new PlantList<Fruit>(FilePath+"_test2.json");
        Assert.That(typeof(JsonWriter<Fruit>), Is.EqualTo(jsonList.FileWriter));
    }
    [Test]
    public void XmlTest()
    {
       
        // ������ � �������� ��������� xml ����
        var xmlFilePAth = FilePath + "_test2.xml";
        var xmlList = new PlantList<Fruit>(xmlFilePAth);
        Assert.That(typeof(XmlWriter<Fruit>), Is.EqualTo(xmlList.FileWriter));
    }

    [Test]
    public void WrongNameFileFileWriter()
    {
        // ������� � �������� ��������� ��������� ����� ����
        var rndList = new PlantList<Fruit>("dsadas");
        Assert.That(typeof(JsonWriter<Fruit>), Is.EqualTo(rndList.FileWriter));
    }
    [Test]
    // �������� ������������ ������ FileWriter - �
    public void FileWriterWorkJson()
    {
      
        var list = new PlantList<Fruit>(FilePath+"_test3.json");
        foreach (var fruit in fruits)
        {
            // ��� ���������� ��������� ������������ � ����
            list.Add(fruit);
        }
        // ��������� ��������� �� ����� � ���������� � �������� � �������� ����� ����
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
            // ��� ���������� ��������� ������������ � ����
            list.Add(fruit);
        }
        // ��������� ��������� �� ����� � ���������� � �������� � �������� ����� ����
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
       
        // ���� ����������
        // ��������� ��� ����������� �������� �����
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
       
        // ���� ��������
        // ��������� ��� ����� �������� ������ ����������
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
       
        // ���� ������ �� �������
        // ��������� ��� ����� �������� ���������
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
        // � �������� ���������� ������ � �������� ������ ���������� ����� ����� ID
        var findId = list.List[number].Id;
        var findFruit = list.Find(findId);
        Assert.That(fruits.FirstOrDefault(f => f.Id==findId), Is.EqualTo(findFruit));
    }
    [Test]
    public void IdTest()
    {
      
        // ���� ��������� ������ ID
        //
        var list = new PlantList<Fruit>(FilePath+"_test7.json");
        foreach (var fruit in fruits)
        {
            list.Add(fruit);
        }
        // ����� ��������� �����
        var rnd = new Random();
        var number = rnd.Next(0, 9);
        // � �������� ���������� ������ � �������� ������ ���������� ����� ����� ID
        var removeId = list.List[number].Id;
        // ������ ��� �����
        var fruitToDel = list.Find(removeId);
        // ������� ����� �� ������
        list.Remove(fruitToDel!);
        // ��������� ����� ����� �� ������ � ������ �����
        list.Add(fruits[number]);
        // ���������� ID �������� ����� �������, ��� ��� �������� ������� �� ������ ��� id �������������
        // ���� Id ���������� �������� ��� ���� ����� �����, ����������� ������� ���� �������� ��� Id
        
        Assert.That(removeId, Is.EqualTo(list.List.Last().Id));
    }
    [Test]
    public void SortTest()
    {
       
        // ���� ����������
        // ��������� ��� ���������� �������� ���������
        var list = new PlantList<Fruit>(FilePath+"_sortTest.json");
        // C����� ���
        var nameList = new List<string>();
        foreach (var fruit in fruits)
        {
            nameList.Add(fruit.PlantName);
            list.Add(fruit);
        }
        // ��������� ������ ���
        nameList.Sort();
        // ���������  ������ �� �����
        list.MySort(x => x.PlantName);
        var rnd = new Random();
        var number = rnd.Next(0, 9);
        // ���������, ������ ��������� ����� �� �������,
        // ��� ��� �������� ����� �����, ����������
        // �� ���� �� �������, �������� �� ������ ���
        Assert.That(nameList[number], Is.EqualTo(list.List[number].PlantName));
    }

    
}