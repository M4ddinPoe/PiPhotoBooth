namespace PiPhotoBooth.UseCases;

using MaybeMonad;

public interface ILoadLastPhoto
{
    Task<Maybe<Stream>> ExecuteAsync();
}

internal sealed class LoadLastPhoto : ILoadLastPhoto
{
    private readonly IRepository repository;

    public LoadLastPhoto(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Maybe<Stream>> ExecuteAsync()
    {
        var maybeLastPhoto = await this.repository.GetLastPhoto();

        return maybeLastPhoto.HasNoValue 
            ? Maybe<Stream>.Nothing 
            : File.OpenRead(maybeLastPhoto.Value.Path);
    }
}