namespace PiPhotoBooth.Settings.UseCases;

public class LoadSettings
{
    private readonly IRepository repository;

    public LoadSettings(IRepository repository)
    {
        this.repository = repository;
    }
}