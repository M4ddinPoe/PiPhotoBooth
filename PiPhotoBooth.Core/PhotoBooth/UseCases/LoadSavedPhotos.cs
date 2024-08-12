namespace PiPhotoBooth.UseCases;

using Model;

public interface ILoadSavedPhotos
{
    Task<IReadOnlyList<Photo>> ExecuteAsync();
}

internal sealed class LoadSavedPhotos : ILoadSavedPhotos
{
    private readonly IRepository repository;

    public LoadSavedPhotos(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IReadOnlyList<Photo>> ExecuteAsync()
    {
        var photos = await this.repository.GetPhotos();
        return photos.ToList();
    }
}