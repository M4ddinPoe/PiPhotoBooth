namespace PiPhotoBooth.CameraControl.GPhoto;

using GPhoto2;
using ResultMonad;

public class CameraControl : ICameraControl
{
    private readonly GPhoto2 gPhoto2 = new();

    public async Task<bool> IsOnlineAsync()
    {
        var cameras = await gPhoto2.AutoDetectAsync();
        return cameras.Count > 0;
    }

    public Task<ResultWithError<string>> TakePhotoAsync(string filename)
    {
        return gPhoto2.CaptureImageAndDownloadAsync(filename);
    }
}

