namespace PiPhotoBooth.Settings.UseCases;

using Mediator;
using Model;
using Services.Messages;

public interface IUpdateSettings
{
    Task ExecuteAsync(Settings settings);
}

internal sealed class UpdateSettings : IUpdateSettings
{
    private readonly IMediator mediator;
    private readonly IRepository repository;

    public UpdateSettings(IMediator mediator, IRepository repository)
    {
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task ExecuteAsync(Settings settings)
    {
        await this.repository.UpdateSettings(settings);
        await this.mediator.Publish(new SettingsUpdatedNotification());
    }
}