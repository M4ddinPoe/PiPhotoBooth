namespace PiPhotoBooth.Repository;

public interface IRepository
{
    Task<int> GetNextIndexAsync();

    Task UpdateIndexAsync(int index);

    void Dispose();
}