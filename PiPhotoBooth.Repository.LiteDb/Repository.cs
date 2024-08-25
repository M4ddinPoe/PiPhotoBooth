namespace PiPhotoBooth;

using Entities;
using LiteDB.Async;
using Mappings;
using MaybeMonad;
using Model;
using Services;
using Photo = Model.Photo;

public sealed class Repository : IRepository
{
    private readonly string connectionString;
    
    public Repository()
    {
        var directory = Path.Combine(@"C:\temp", "PiPhotoBooth");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        this.connectionString = Path.Combine(directory, "ppb.db");
    }

    public async Task<bool> IsInitialized()
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<SettingsEntity>("settings");
        
        var count = await col.CountAsync();
        return count > 0;
    }

    public async Task<Model.Settings> GetSettings()
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<SettingsEntity>("settings");
        
        var settings = await col.Query().FirstOrDefaultAsync();

        if (settings == null)
        {
            settings = new SettingsEntity();
            await col.InsertAsync(settings);
        }

        return settings.ToModel();
    }
    
    public async Task UpdateSettings(Model.Settings settings)
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<SettingsEntity>("settings");
        
        var count = await col.CountAsync();
        var settingsEntity = settings.ToEntity();

        if (count == 0)
        {
            await col.InsertAsync(settingsEntity);
        }
        else
        {
            await col.UpdateAsync(settingsEntity);
        }
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
    
    public async Task<Maybe<Photo>> DeleteLastPhoto()
    {
        using var database = new LiteDatabaseAsync(this.connectionString);
        var col = database.GetCollection<Entities.Photo>("photos");
        var entity = await col.Query().OrderByDescending(photo => photo.Taken).FirstOrDefaultAsync();

        col.DeleteAsync(entity.Id);
        return entity?.ToModel() ?? Maybe<Photo>.Nothing;
    }
}
