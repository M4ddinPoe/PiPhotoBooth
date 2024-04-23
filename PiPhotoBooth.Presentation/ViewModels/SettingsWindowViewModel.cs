namespace PiPhotoBooth.ViewModels;

using System;
using Model;
using ReactiveUI;
using Settings.UseCases;

public class SettingsWindowViewModel : ViewModelBase
{
    private readonly ILoadSettings loadSettings;
    private readonly IUpdateSettings updateSettings;

    private string _dataDirectory;
    private bool _isFakeCameraControlEnabled;
    
    public SettingsWindowViewModel(ILoadSettings loadSettings, IUpdateSettings updateSettings)
    {
        this.loadSettings = loadSettings;
        this.updateSettings = updateSettings;
        
        this.LoadSettingsAsync();
    }

    public string DataDirectory
    {
        get => this._dataDirectory;
        set => this.RaiseAndSetIfChanged(ref this._dataDirectory, value);
    }

    public bool IsFakeCameraControlEnabled
    {
        get => this._isFakeCameraControlEnabled;
        set => this.RaiseAndSetIfChanged(ref this._isFakeCameraControlEnabled, value);
    }

    public async void SaveAsync()
    {
        try
        {
            var settings = new Settings(this.DataDirectory, this.IsFakeCameraControlEnabled);
            await this.updateSettings.ExecuteAsync(settings);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    private async void LoadSettingsAsync()
    {
        try
        {
            var settings = await this.loadSettings.ExecuteAsync();

            this.DataDirectory = settings.DataDirectory;
            this.IsFakeCameraControlEnabled = settings.IsFakeCameraControlEnabled;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}