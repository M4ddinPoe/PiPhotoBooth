namespace PiPhotoBooth;

using Entities;
using LiteDB.Async;
using Mappings;
using MaybeMonad;
using Photo = Model.Photo;

public sealed class Repository : IRepository
{
    private readonly string connectionString;
    
    public Repository()
    {
        this.connectionString = Path.Combine(Environment.CurrentDirectory, "ppb.db");
    }
    
    public async Task<int> GetNextIndexAsync()
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<PhotoIndex>("photo_index");

        var currentIndex = await col.Query()
            .Where(x => x.Day == DateTime.Today)
            .FirstOrDefaultAsync();

        return currentIndex == null
            ? 1
            : currentIndex.LastNumber + 1;
    }

    public async Task UpdateIndexAsync(int index)
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<PhotoIndex>("photo_index");

        var count = await col.Query().CountAsync();
        var photoIndex = new PhotoIndex { Id = 1, Day = DateTime.Today, LastNumber = index };
        
        if (count == 0)
        {
            await col.InsertAsync(photoIndex);
        }
        else
        {
            await col.UpdateAsync(photoIndex);
        }
    }

    public async Task AddPhoto(Photo photo)
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<Entities.Photo>("photos");

        var photoEntity = photo.ToEntity();
        await col.InsertAsync(photoEntity);
    }

    public async Task<IEnumerable<Photo>> GetPhotos()
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<Entities.Photo>("photos");
        var entities = await col.Query().ToListAsync();

        return entities.Select(photo => photo.ToModel());
    }
    
    public async Task<Maybe<Photo>> GetLastPhoto()
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<Entities.Photo>("photos");
        var entity = await col.Query().OrderByDescending(photo => photo.Taken).FirstOrDefaultAsync();

        return entity?.ToModel() ?? Maybe<Photo>.Nothing;
    }
}
