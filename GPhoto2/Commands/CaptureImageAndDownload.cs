namespace GPhoto2.Commands;

using ResponseParser;
using ResultMonad;

internal sealed class CaptureImageAndDownload : BaseCommand
{
    private readonly CaptureImageAndDownloadParser captureImageAndDownloadParser = new();
    protected override string Command { get; set; }

    public async Task<ResultWithError<string>> ExecuteAsync(string filename)
    {
        this.Command = $"gphoto2 --capture-image-and-download --keep-raw --filename '{filename}'";
        var response = await this.ExecuteCommandAsync();

        return this.captureImageAndDownloadParser.Execute(response);
    }
}