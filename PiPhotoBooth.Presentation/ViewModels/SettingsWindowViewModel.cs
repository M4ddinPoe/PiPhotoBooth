namespace PiPhotoBooth.ViewModels;

using System;
using System.Threading.Tasks;
using Mediator;
using Messages;
using Model;
using ReactiveUI;
using Services;

public class SettingsWindowViewModel : ViewModelBase
{
    private readonly SettingsProvider settingsProvider;
    private readonly IMediator mediator;

    private string dataDirectory;
    private bool isFakeCameraControlEnabled;
    
    public SettingsWindowViewModel(SettingsProvider settingsProvider, IMediator mediator)
    {
        this.settingsProvider = settingsProvider;
        this.mediator = mediator;

        this.LoadSettings();
    }

    public string DataDirectory
    {
        get => this.dataDirectory;
        set => this.RaiseAndSetIfChanged(ref this.dataDirectory, value);
    }

    public bool IsFakeCameraControlEnabled
    {
        get => this.isFakeCameraControlEnabled;
        set => this.RaiseAndSetIfChanged(ref this.isFakeCameraControlEnabled, value);
    }

    public async void SaveAsync()
    {
        try
        {
            var settings = new Settings(this.DataDirectory, this.IsFakeCameraControlEnabled);
            await this.settingsProvider.UpdateSettings(settings);
        }
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
    }
    
    private async Task LoadSettings()
    {
        try
        {
            this.DataDirectory = this.settingsProvider.Settings.DataDirectory;
            this.IsFakeCameraControlEnabled = this.settingsProvider.Settings.IsFakeCameraControlEnabled;
        }
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
    }
}
