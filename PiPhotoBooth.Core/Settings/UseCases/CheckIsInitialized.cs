namespace PiPhotoBooth.Settings.UseCases;

public class CheckIsInitialized
{
    private readonly IRepository repository;

    public CheckIsInitialized(IRepository repository)
    {
        this.repository = repository;
    }
}