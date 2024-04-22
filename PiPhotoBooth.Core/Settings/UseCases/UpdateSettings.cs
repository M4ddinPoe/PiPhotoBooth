namespace PiPhotoBooth.Settings.UseCases;

public class UpdateSettings
{
    private readonly IRepository repository;

    public UpdateSettings(IRepository repository)
    {
        this.repository = repository;
    }
}