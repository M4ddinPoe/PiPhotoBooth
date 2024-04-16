namespace PiPhotoBoot.UseCases;

public interface ICheckCameraConnected
{
    Task<bool> ExecuteAsync();
}

public class CheckCameraConnected : ICheckCameraConnected
{
    private readonly GPhoto2 gPhoto2 = new();
    
    public async Task<bool> ExecuteAsync()
    {
        var cameras = this.gPhoto2.AutoDetect();
        return cameras.Count > 0;
    }
}