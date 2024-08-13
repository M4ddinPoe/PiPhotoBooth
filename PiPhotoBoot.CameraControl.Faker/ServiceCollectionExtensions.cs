namespace PiPhotoBoot;

using Microsoft.Extensions.DependencyInjection;
using PiPhotoBooth;
using PiPhotoBooth.Model;

public static class ServiceCollectionExtensions 
{
    public static void AddFakeCameraService(this IServiceCollection collection) 
    {
        collection.AddKeyedTransient<ICameraControl, CameraControl>(Settings.CameraControlFakerKey);
    }
}
