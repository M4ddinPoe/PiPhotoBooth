using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PiPhotoBooth.ViewModels;
using PiPhotoBooth.Views;

namespace PiPhotoBooth;

using System;
using Microsoft.Extensions.DependencyInjection;

public partial class App : Application
{
    private ServiceProvider serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.AddCommonServices();
        collection.AddCoreServices();
        collection.AddRepositoryServices();

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        this.serviceProvider = collection.BuildServiceProvider();
        var vm = serviceProvider.GetRequiredService<MainWindowViewModel>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm,
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = vm
            };
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