namespace PiPhotoBooth.UseCases;

using Mediator;
using Services;
using Services.Messages;

public interface ICheckCameraConnected
{
    Task<bool> ExecuteAsync();
}

public sealed class CheckCameraConnected : ICheckCameraConnected
{
    private readonly SettingsProvider settingsProvider;

    public CheckCameraConnected(
        SettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
    }

    public async Task<bool> ExecuteAsync()
    {
        var cameraControl = settingsProvider.GetConfiguredCameraControl();
        return await cameraControl.IsOnlineAsync();
    }
}