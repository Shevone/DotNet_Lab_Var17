using DotnetLabVar17.Models;

namespace Lab17Test.ConfigModelTests;

[TestFixture]
public class ConfigTest
{
    private string _curRoot;

    [SetUp]
    public void SetUp()
    {
        _curRoot = Directory.GetCurrentDirectory()[..^16];
    }
    [Test]
    public void CheckWithEmptyParam()
    {
        // Проверка что при передачи null параметров выдаёт false
        var cfg = new Config(null,null,null,null);
        Assert.That(cfg.CheckFilePathValid(), Is.False);
    }
    [Test]
    public void CheckWithNonExistFileParam()
    {
        // Проверка что при передачи названий не существующих файлов выдаёт false
        var cfg = new Config(_curRoot +"log_test.txt",_curRoot +"fr_test.json",_curRoot +"vg_test.json",_curRoot +"ber_test.json");
        Assert.That(cfg.CheckFilePathValid(), Is.False);
    }
    [Test]
    public void CheckWithWrongExtensionParam()
    {
        // Проверка что при передачи файлов с неправильным расширением выдаёт false
        var cfg = new Config(_curRoot +"log_test.json",_curRoot +"fr_test.txt",_curRoot +"vg_test.yaml",_curRoot +"ber_test.docx");
        Assert.That(cfg.CheckFilePathValid(), Is.False);
    }
    [Test]
    public void CheckWithCorrectParam()
    {
        Console.WriteLine(_curRoot);
        // Проверка что присуществующих файлов с правильным расширением выводит true
        var cfg = new Config(_curRoot +@"ConfigModelTests\staticFiles\log.txt",_curRoot +@"ConfigModelTests\staticFiles\fr.json",_curRoot +@"ConfigModelTests\staticFiles\veg.json",_curRoot +@"ConfigModelTests\staticFiles\ber.json");
        Assert.That(cfg.CheckFilePathValid(), Is.True);
    }
    
}