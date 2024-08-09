namespace PiPhotoBooth;

using Microsoft.Extensions.DependencyInjection;
using ViewModels;

public static class ServiceCollectionExtensions {
    public static void AddCommonServices(this IServiceCollection collection) {
        // collection.AddSingleton<IRepository, Repository>();
        // collection.AddTransient<BusinessService>();
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<SettingsWindowViewModel>();
        //collection.AddTransient<GalleryViewModel>();
    }
}
