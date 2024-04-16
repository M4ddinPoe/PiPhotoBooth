namespace PiPhotoBooth.Repository;

using Entities;
using LiteDB.Async;

public sealed class Repository : IDisposable, IRepository
{
    private readonly LiteDatabaseAsync database;
    
    public Repository()
    {
        var connection = Path.Combine(Environment.CurrentDirectory, "ppb.db");
        database = new LiteDatabaseAsync(connection);
    }
    
    public async Task<int> GetNextIndexAsync()
    {
        var col = database.GetCollection<PhotoIndex>("photo_index");
            
        var currentIndex = await col.Query()
            .Where(x => x.Day == DateTime.Today)
            .FirstOrDefaultAsync();

        return currentIndex == null 
            ? 1 
            : currentIndex.LastNumber++;
    }

    public async Task UpdateIndexAsync(int index)
    {
        var col = database.GetCollection<PhotoIndex>("photo_index");

        var photoIndex = new PhotoIndex { Day = DateTime.Today, LastNumber = index };
        
        if (index == 1)
        {
            await col.InsertAsync(photoIndex);
        }
        else
        {
            await col.UpdateAsync(photoIndex);
        }
    }

    public void Dispose()
    {
        database.Dispose();
    }
}
