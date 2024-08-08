namespace PiPhotoBooth.UseCases;

using Model;

internal sealed class LoadSavedPhotos
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