namespace PiPhotoBooth;

using Microsoft.Extensions.DependencyInjection;
using Model;
using ViewModels;
using Views;

public static class ServiceCollectionExtensions {
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindow>();
        collection.AddSingleton<MainWindowViewModel>();
        
        collection.AddTransient<PhotoView>();
        collection.AddTransient<PhotoViewModel>();
        
        collection.AddTransient<GalleryView>();
        collection.AddTransient<GalleryViewModel>();

        collection.AddTransient<ErrorsView>();
        collection.AddScoped<ErrorsViewModel>();
        
        collection.AddTransient<SettingsWindowViewModel>();
    }
}
