namespace PiPhotoBooth.UseCases;

using Microsoft.Extensions.DependencyInjection;
using Services;

public interface ICheckCameraConnected
{
    Task<bool> ExecuteAsync();
}

internal sealed class CheckCameraConnected : ICheckCameraConnected
{
    private readonly ICameraControl cameraControl;

    public CheckCameraConnected(
        SettingsProvider settingsProvider)
    {
        this.cameraControl = settingsProvider.GetConfiguredCameraControl();
    }

    public async Task<bool> ExecuteAsync()
    {
        return await this.cameraControl.IsOnlineAsync();
    }
}