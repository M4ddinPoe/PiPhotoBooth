namespace PiPhotoBooth.Model;

public sealed class Settings
{
    public static string CameraControlGPhotoKey = "CameraControl.GPhoto";
    
    public static string CameraControlFakerKey = "CameraControl.Faker";

    public static Settings Empty = new Settings(string.Empty, false, false);
    
    public Settings(string dataDirectory, bool isFakeCameraControlEnabled, bool isFullScreenEnabled)
    {
        this.DataDirectory = dataDirectory;
        this.IsFakeCameraControlEnabled = isFakeCameraControlEnabled;
        this.IsFullScreenEnabled = isFullScreenEnabled;
    }

    public string DataDirectory { get; }
    
    public bool IsFakeCameraControlEnabled { get; }
    
    public bool IsFullScreenEnabled { get; }

    public string CurrentCameraControlKey => this.IsFakeCameraControlEnabled
        ? CameraControlFakerKey
        : CameraControlGPhotoKey;
}