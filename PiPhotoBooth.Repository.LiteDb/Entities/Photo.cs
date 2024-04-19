namespace PiPhotoBooth.Entities;

internal sealed class Photo
{
    public Guid Id { get; init; }
    
    public DateTime Taken { get; init; }
    
    public string Path { get; init; }
}
