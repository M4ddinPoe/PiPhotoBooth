namespace PiPhotoBoot;

using Commands;
using ResultMonad;

public class GPhoto2
{
    private readonly AutoDetect autoDetect = new();
    private readonly CaptureImageAndDownload captureImageAndDownload = new (); 
    
    public Task<List<string>> AutoDetectAsync()
    {
        return this.autoDetect.ExecuteAsync();
    }

    public async Task<ResultWithError<string>> CaptureImageAndDownloadAsync(string filename)
    {
        return await this.captureImageAndDownload.ExecuteAsync(filename);
    }
}