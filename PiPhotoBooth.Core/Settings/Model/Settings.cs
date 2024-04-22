namespace PiPhotoBooth.Model;

public sealed class Settings
{
    public Settings(string dataDirectory, bool isFakeCameraControlEnabled)
    {
        this.DataDirectory = dataDirectory;
        this.IsFakeCameraControlEnabled = isFakeCameraControlEnabled;
    }

    public string DataDirectory { get; }
    
    public bool IsFakeCameraControlEnabled { get; }
}