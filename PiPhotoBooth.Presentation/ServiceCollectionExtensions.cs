namespace PiPhotoBooth;

using Microsoft.Extensions.DependencyInjection;
using ViewModels;
using Views;

public static class ServiceCollectionExtensions {
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindow>();
        collection.AddTransient<GalleryView>();
        // collection.AddSingleton<IRepository, Repository>();
        // collection.AddTransient<BusinessService>();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<SettingsWindowViewModel>();
        collection.AddTransient<GalleryViewModel>();
    }
}
