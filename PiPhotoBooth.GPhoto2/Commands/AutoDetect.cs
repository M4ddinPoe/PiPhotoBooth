namespace PiPhotoBoot.Commands;

using ResponseParser;

internal sealed class AutoDetect : BaseCommand
{
    private readonly AutoDetectParser parser;
    
    protected override string Command
    {
        get { return "gphoto2 --auto-detect"; }
        set {}
    }

    public List<string> Execute()
    {
        var response = this.ExecuteCommand();
        return this.parser.Execute(response);
    }
}