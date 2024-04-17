namespace PiPhotoBooth.Repository.Entities;

internal sealed class PhotoIndex
{
    public int Id { get; set; }
    
    public DateTime Day { get; set; }
    
    public int LastNumber { get; set; }
}
