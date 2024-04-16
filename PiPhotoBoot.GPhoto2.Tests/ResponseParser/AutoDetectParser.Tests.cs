namespace PiPhotoBoot.GPhoto2.Tests.ResponseParser;

using FluentAssertions;
using PiPhotoBoot.ResponseParser;

public class AutoDetectParser_Tests
{
    [Fact]
    public void TestEmptyResult()
    {
        string response = """
                          Model                          Port
                          ----------------------------------------------------------
                          """;

        var autoDetectParser = new AutoDetectParser();
        var models = autoDetectParser.Execute(response);

        models.Should().BeEmpty();
    }
    
    [Fact]
    public void TestSingle()
    {
        string response = """
                          Model                          Port
                          ----------------------------------------------------------
                          Canon PowerShot G2             usb:
                          """;

        var autoDetectParser = new AutoDetectParser();
        var models = autoDetectParser.Execute(response);

        models.Should().HaveCount(1);
        models.First().Should().Be("Canon PowerShot G2");
    }
    
    [Fact]
    public void TestMultiple()
    {
        string response = """
                          Model                          Port
                          ----------------------------------------------------------
                          Canon PowerShot G2             usb:
                          Nikon D5200                    usb:
                          """;

        var autoDetectParser = new AutoDetectParser();
        var models = autoDetectParser.Execute(response);

        models.Should().HaveCount(2);
        models.First().Should().Be("Canon PowerShot G2");
        models[1].Should().Be("Nikon D5200");
    }
}