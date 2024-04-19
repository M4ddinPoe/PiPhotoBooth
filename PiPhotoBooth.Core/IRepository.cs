namespace PiPhotoBooth;

using MaybeMonad;
using Model;

public interface IRepository
{
    Task<int> GetNextIndexAsync();

    Task UpdateIndexAsync(int index);

    Task AddPhoto(Photo photo);

    Task<IEnumerable<Photo>> GetPhotos();

    Task<Maybe<Photo>> GetLastPhoto();

    void Dispose();
}