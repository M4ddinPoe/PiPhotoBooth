namespace PiPhotoBoot;

using Microsoft.Extensions.DependencyInjection;
using PiPhotoBooth;

public static class ServiceCollectionExtensions 
{
    public static void AddFakeCameraService(this IServiceCollection collection) 
    {
        collection.AddTransient<ICameraControl, CameraControl>();
    }
}
