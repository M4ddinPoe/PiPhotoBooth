namespace PiPhotoBoot.UseCases;

using PiPhotoBooth.Repository;

public interface IMakePhoto
{
    Task ExecuteAsync();
}

public class MakePhoto : IMakePhoto
{
    private readonly GPhoto2 gPhoto2 = new();
    
    public async Task ExecuteAsync()
    {
        // get filename
        this.gPhoto2.CaptureImageAndDownload("");
    }
}