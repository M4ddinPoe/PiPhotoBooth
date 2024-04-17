namespace PiPhotoBoot.Commands;

using ResponseParser;

internal sealed class AutoDetect : BaseCommand
{
    private readonly AutoDetectParser parser = new();
    
    protected override string Command
    {
        get { return "gphoto2 --auto-detect"; }
        set {}
    }

    public async Task<List<string>> ExecuteAsync()
    {
        var response = await this.ExecuteCommandAsync();
        return this.parser.Execute(response);
    }
}