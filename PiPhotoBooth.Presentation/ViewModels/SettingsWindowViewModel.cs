namespace PiPhotoBooth.ViewModels;

using System;
using Model;
using ReactiveUI;
using Services;
using Settings.UseCases;

public class SettingsWindowViewModel : ViewModelBase
{
    private readonly SettingsProvider settingsProvider;

    private string _dataDirectory;
    private bool _isFakeCameraControlEnabled;
    
    public SettingsWindowViewModel(SettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;

        this.LoadSettings();
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
            await this.settingsProvider.UpdateSettings(settings);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    private void LoadSettings()
    {
        try
        {
            this.DataDirectory = this.settingsProvider.Settings.DataDirectory;
            this.IsFakeCameraControlEnabled = this.settingsProvider.Settings.IsFakeCameraControlEnabled;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}