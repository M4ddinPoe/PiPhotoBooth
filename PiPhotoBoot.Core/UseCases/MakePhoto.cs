namespace PiPhotoBoot.UseCases;

public interface IMakePhoto
{
    Task ExecuteAsync();
}

public class MakePhoto : IMakePhoto
{
    public async Task ExecuteAsync()
    {
        // get current picture number/name
        // capture-image-and-download --keep-raw --filename /home/pi/ppb/ppb-20240416-0001 
        await Task.Delay(1000);
    }
}