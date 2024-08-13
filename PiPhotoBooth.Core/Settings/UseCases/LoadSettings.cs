namespace PiPhotoBooth.Settings.UseCases;

using Model;

public interface ILoadSettings
{
    Task<Settings> ExecuteAsync();
}

internal sealed class LoadSettings : ILoadSettings
{
    private readonly IRepository repository;

    public LoadSettings(IRepository repository)
    {
        this.repository = repository;
    }

    public Task<Settings> ExecuteAsync()
    {
        return this.repository.GetSettings();
    }
}