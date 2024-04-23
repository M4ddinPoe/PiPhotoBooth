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

    public Task ExecuteAsync(Settings settings)
    {
        return this.repository.UpdateSettings(settings);
    }
}