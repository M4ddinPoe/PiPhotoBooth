namespace PiPhotoBoot.UseCases;

public interface ICheckCameraConnected
{
    Task<bool> ExecuteAsync();
}

internal sealed class CheckCameraConnected : ICheckCameraConnected
{
    private readonly GPhoto2 gPhoto2 = new();
    
    public async Task<bool> ExecuteAsync()
    {
        var cameras = await this.gPhoto2.AutoDetectAsync();
        return cameras.Count > 0;
    }
}