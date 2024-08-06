namespace PiPhotoBooth.UseCases;

using Mediator;
using Services;
using Services.Messages;

public interface ICheckCameraConnected
{
    Task<bool> ExecuteAsync();
}

internal sealed class CheckCameraConnected : ICheckCameraConnected, INotificationHandler<SettingsUpdatedNotification>
{
    private readonly SettingsProvider settingsProvider;
    private ICameraControl cameraControl;

    public CheckCameraConnected(
        SettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
        this.cameraControl = settingsProvider.GetConfiguredCameraControl();
    }

    public async Task<bool> ExecuteAsync()
    {
        return await this.cameraControl.IsOnlineAsync();
    }

    public ValueTask Handle(SettingsUpdatedNotification notification, CancellationToken cancellationToken)
    {
        this.cameraControl = this.settingsProvider.GetConfiguredCameraControl();
        return new ValueTask();
    }
}