namespace PiPhotoBooth.UseCases;

public interface ICheckCameraConnected
{
    Task<bool> ExecuteAsync();
}

internal sealed class CheckCameraConnected : ICheckCameraConnected
{
    private readonly ICameraControl cameraControl;

    public CheckCameraConnected(ICameraControl cameraControl)
    {
        this.cameraControl = cameraControl;
    }

    public async Task<bool> ExecuteAsync()
    {
        return await this.cameraControl.IsOnlineAsync();
    }
}