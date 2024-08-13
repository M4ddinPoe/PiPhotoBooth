namespace PiPhotoBooth.CameraControl.GPhoto;

using Microsoft.Extensions.DependencyInjection;
using Model;

public static class ServiceCollectionExtensions 
{
    public static void AddGPhoto2CameraService(this IServiceCollection collection) 
    {
        collection.AddKeyedTransient<ICameraControl, CameraControl>(Settings.CameraControlGPhotoKey);
    }
}
