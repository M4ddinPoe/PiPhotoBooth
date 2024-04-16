namespace PiPhotoBoot.Commands;

internal sealed class CaptureImageAndDownload : BaseCommand
{
    protected override string Command { get; set; }

    public void Execute(string filename)
    {
        this.Command = $"capture-image-and-download --keep-raw --filename /home/pi/ppb/{filename}";
        this.ExecuteCommand();
    }
}