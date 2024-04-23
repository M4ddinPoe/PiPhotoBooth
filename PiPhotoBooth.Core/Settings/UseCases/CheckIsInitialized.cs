namespace PiPhotoBooth.Settings.UseCases;

public interface ICheckIsInitialized
{
    Task<bool> ExecuteAsync();
}

internal sealed class CheckIsInitialized : ICheckIsInitialized
{
    private readonly IRepository repository;

    public CheckIsInitialized(IRepository repository)
    {
        this.repository = repository;
    }

    public Task<bool> ExecuteAsync()
    {
        return this.repository.IsInitialized();
    }
}