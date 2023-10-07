using DotnetLabVar17.Models;
using DotnetLabVar17.Models.OtherModels;

namespace Lab17Test.HarvestTests;

[TestFixture]
public class HarvestTest
{
    private Harvest _harvest;
    private List<Farmer> _farmers;
    [SetUp]
    public void SetUp()
    {
        _harvest = new Harvest();
        _farmers = new List<Farmer>
        {
            new ("Иван"),
            new ("Василий"),
            new ("Николай"),
            new ("Артём"),
            new ("Егор"),
            new ("Данила"),
            new ("Марк"),
            new ("Илья"),
            new ("Макар"),
            new ("Арсений")
        };
    }
    [TearDown]
    public void AfterTest()
    {
        _harvest = new Harvest();
        _farmers.Clear();
    }
    [Test]
    // Проверяем что при доблении фермеров возвращается true
    public void AddFarmerTest()
    {
        foreach (var farmer in _farmers)
        {
            Assert.That(_harvest.AddFarmer(farmer), Is.True);
        }
    }
    [Test]
    // При колечесвте фермеров больше 10 
    // Вощвращается False и добавление отклоняется
    public void AddMoreThan10Farmers()
    {
        foreach (var farmer in _farmers)
        {
            Assert.That(_harvest.AddFarmer(farmer), Is.True);
        }
        Assert.That(_harvest.AddFarmer(new Farmer("Новенький")), Is.False);
    }
    [Test]
    // Удаление рандомного фермера из списка по его Id
    public void DeleteFarmer()
    {
        foreach (var farmer in _farmers)
        {
            _harvest.AddFarmer(farmer);
        }
        var rnd = new Random().Next(0, _farmers.Count-1);
        var randomFarmer = _farmers[rnd];

        Assert.IsTrue(_harvest.RemoveFarmer(randomFarmer.Id));
    }
    [Test]
    // Попытка удалить передать не существующий id в метод удаления фермера из урожайного сбора
    public void DeleteFarmerWithNonExistentId()
    {
        foreach (var farmer in _farmers)
        {
            _harvest.AddFarmer(farmer);
        }
        var rnd = new Random().Next(_farmers.Count, 1000);

        Assert.IsFalse(_harvest.RemoveFarmer(rnd));
    }
    [Test]
    // Тест добавления нового растения в урожайный сбор
    public void AddPlantTest()
    {
        var fruit = new Fruit("Яблоко", "зеленый", DateTime.Now, false, "кисло-сладкий", true);
        Assert.IsTrue(_harvest.AddPlant(fruit, 5));
        // Попытка добавить уже записанный продукт
        Assert.IsFalse(_harvest.AddPlant(fruit,1000));
    }
}