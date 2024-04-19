namespace PiPhotoBooth.Model;

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
}
