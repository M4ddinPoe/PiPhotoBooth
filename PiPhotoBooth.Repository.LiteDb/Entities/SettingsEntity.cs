namespace PiPhotoBooth.Entities;

internal sealed class SettingsEntity
{
    public int Id { get; init; }
    
    public string DataDirectory { get; init; }
    
    public bool IsFakeCameraControlEnabled { get; init; }
    
    public bool? IsFullscreenEnable { get; init; }
}