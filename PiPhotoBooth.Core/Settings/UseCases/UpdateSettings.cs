namespace PiPhotoBooth.Settings.UseCases;

using Model;

public interface IUpdateSettings
{
    Task ExecuteAsync(Settings settings);
}

internal sealed class UpdateSettings : IUpdateSettings
{
    private readonly IRepository repository;

    public UpdateSettings(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task ExecuteAsync(Settings settings)
    {
        await this.repository.UpdateSettings(settings);
    }
}