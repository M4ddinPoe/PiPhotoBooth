namespace PiPhotoBooth.Model;

using System.Runtime.CompilerServices;

public sealed class Photo
{
    public Photo(string path)
    {
        Id = Guid.NewGuid();
        Taken = DateTime.Now;
        Path = path;
    }
    
    public Photo(Guid id, DateTime taken, string path)
    {
        Id = id;
        Taken = taken;
        Path = path;
    }

    public Guid Id { get; }
    
    public DateTime Taken { get; }
    
    public string Path { get; }
    
    public async Task<Stream> LoadAsync()
    {
        return File.OpenRead(this.Path);
    }
}
