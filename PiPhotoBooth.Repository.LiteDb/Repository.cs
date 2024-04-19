namespace PiPhotoBooth;

using Entities;
using LiteDB.Async;
using Mappings;
using MaybeMonad;
using Photo = Model.Photo;

public sealed class Repository : IDisposable, IRepository
{
    private readonly LiteDatabaseAsync database;
    
    public Repository()
    {
        var connection = Path.Combine(Environment.CurrentDirectory, "ppb.db");
        database = new LiteDatabaseAsync(connection);
    }
    
    ~Repository()
    {
        this.Dispose();
    }
    
    public async Task<int> GetNextIndexAsync()
    {
        var col = database.GetCollection<PhotoIndex>("photo_index");
            
        var currentIndex = await col.Query()
            .Where(x => x.Day == DateTime.Today)
            .FirstOrDefaultAsync();

        return currentIndex == null 
            ? 1 
            : currentIndex.LastNumber+1;
    }

    public async Task UpdateIndexAsync(int index)
    {
        var col = database.GetCollection<PhotoIndex>("photo_index");

        var photoIndex = new PhotoIndex { Id = 1, Day = DateTime.Today, LastNumber = index };
        
        if (index == 1)
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
        var col = database.GetCollection<Entities.Photo>("photos");

        var photoEntity = photo.ToEntity();
        await col.InsertAsync(photoEntity);
    }

    public async Task<IEnumerable<Photo>> GetPhotos()
    {
        var col = database.GetCollection<Entities.Photo>("photos");
        var entities = await col.Query().ToListAsync();

        return entities.Select(photo => photo.ToModel());
    }
    
    public async Task<Maybe<Photo>> GetLastPhoto()
    {
        var col = database.GetCollection<Entities.Photo>("photos");
        var entity = await col.Query().OrderByDescending(photo => photo.Taken).FirstOrDefaultAsync();

        if (entity == null)
        {
            return Maybe<Photo>.Nothing;
        }
        
        return entity.ToModel();
    }

    public void Dispose()
    {
        database.Dispose();
    }
}
