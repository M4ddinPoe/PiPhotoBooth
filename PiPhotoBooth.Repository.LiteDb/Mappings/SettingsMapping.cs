namespace PiPhotoBooth.Mappings;

using Entities;
using Model;

internal static class SettingsMapping
{
    public static Settings ToModel(this SettingsEntity entity)
    {
        return new Settings(entity.DataDirectory, entity.IsFakeCameraControlEnabled, entity.IsFullscreenEnable ?? false);
    }

    public static SettingsEntity ToEntity(this Settings model)
    {
        return new SettingsEntity
        {
            Id = 1,
            DataDirectory = model.DataDirectory,
            IsFakeCameraControlEnabled = model.IsFakeCameraControlEnabled,
            IsFullscreenEnable = model.IsFullScreenEnabled,
        };
    }
}