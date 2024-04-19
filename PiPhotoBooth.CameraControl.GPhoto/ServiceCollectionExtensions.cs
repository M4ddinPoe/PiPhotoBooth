namespace PiPhotoBooth.CameraControl.GPhoto;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions 
{
    public static void AddGPhoto2CameraService(this IServiceCollection collection) 
    {
        collection.AddTransient<ICameraControl, PiPhotoBooth.CameraControl.GPhoto.CameraControl>();
    }
}
