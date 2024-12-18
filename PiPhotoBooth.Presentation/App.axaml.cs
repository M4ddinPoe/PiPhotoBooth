namespace PiPhotoBooth;

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CameraControl.GPhoto;
using PiPhotoBoot;
using PiPhotoBooth.ViewModels;
using PiPhotoBooth.Views;
using Services;

public partial class App : Application
{
    private ServiceProvider serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        
        //var configuration = config.GetRequiredSection("PhotoBooth").Get<Configuration.Configuration>();

        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.AddCommonServices();
        collection.AddCoreServices();
        collection.AddRepositoryServices();
        collection.AddFakeCameraService();
        collection.AddGPhoto2CameraService();
        collection.AddMediator();
        //collection.AddSingleton(configuration);

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        this.serviceProvider = collection.BuildServiceProvider();
        var v = serviceProvider.GetRequiredService<MainWindow>();
        var vm = serviceProvider.GetRequiredService<MainWindowViewModel>();
        
        v.DataContext = vm;
        
        var settingsProvider = this.serviceProvider.GetService<SettingsProvider>();
        settingsProvider.Init();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;
            
            desktop.MainWindow = v;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = v;
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        if (serviceProvider is IDisposable)
        { 
            serviceProvider.Dispose();
        }
    }
}
