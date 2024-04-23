namespace PiPhotoBooth;

using Microsoft.Extensions.DependencyInjection;
using Settings.UseCases;
using UseCases;

public static class ServiceCollectionExtensions 
{
    public static void AddCoreServices(this IServiceCollection collection) 
    {
        collection.AddTransient<ICheckCameraConnected, CheckCameraConnected>();
        collection.AddTransient<ILoadLastPhoto, LoadLastPhoto>();
        collection.AddTransient<IMakePhoto, MakePhoto>();
        
        collection.AddTransient<ICheckIsInitialized, CheckIsInitialized>();
        collection.AddTransient<ILoadSettings, LoadSettings>();
        collection.AddTransient<IUpdateSettings, UpdateSettings>();
    }
}
