namespace PiPhotoBoot.UseCases;

public interface ILoadLastPhoto
{
    Stream Execute();
}

public class LoadLastPhoto : ILoadLastPhoto
{
    public Stream Execute()
    {
        return File.OpenRead("/Users/maddin/Downloads/20231209_224431.jpg");
    }
}