namespace PiPhotoBooth.Model;

public sealed class Settings
{
    public static string CameraControlGPhotoKey = "CameraControl.GPhoto";
    
    public static string CameraControlFakerKey = "CameraControl.Faker";

    public static Settings Empty = new Settings(string.Empty, false);
    
    public Settings(string dataDirectory, bool isFakeCameraControlEnabled)
    {
        this.DataDirectory = dataDirectory;
        this.IsFakeCameraControlEnabled = isFakeCameraControlEnabled;
    }

    public string DataDirectory { get; }
    
    public bool IsFakeCameraControlEnabled { get; }

    public string CurrentCameraControlKey => this.IsFakeCameraControlEnabled
        ? CameraControlFakerKey
        : CameraControlGPhotoKey;
}