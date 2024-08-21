namespace PiPhotoBooth.UseCases;

public interface IDeleteLastPhoto
{
    Task ExecuteAsync();
}

internal class DeleteLastPhoto : IDeleteLastPhoto
{
    private readonly IRepository repository;

    public DeleteLastPhoto(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task ExecuteAsync()
    {
        var lastPhotoResult = await this.repository.DeleteLastPhoto();

        if (lastPhotoResult.HasNoValue)
        {
            return;
        }
        
        File.Delete(lastPhotoResult.Value.Path);
    }
}