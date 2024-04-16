namespace PiPhotoBoot.UseCases;

public interface ILoadLastPhoto
{
    Stream Execute();
}

public class LoadLastPhoto : ILoadLastPhoto
{
    public Stream Execute()
    {
        string path = "/Users/maddin/Downloads/20231209_224431.jpg";
        //string path = @"C:\Users\martin.poepel\OneDrive - advastore.com\Bilder\Wallpaper\16-9\01-Ensiferum-0579.jpg";
        return File.OpenRead(path);
    }
}