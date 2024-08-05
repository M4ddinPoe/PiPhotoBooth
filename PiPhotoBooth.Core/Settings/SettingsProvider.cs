namespace PiPhotoBooth.Services;

using Microsoft.Extensions.DependencyInjection;
using Model;
using Settings.UseCases;

public sealed class SettingsProvider
{
    private readonly IServiceProvider serviceProvider;
    private readonly ICheckIsInitialized checkIsInitialized;
    private readonly ILoadSettings loadSettings;
    private readonly IUpdateSettings updateSettings;

    public SettingsProvider(
        IServiceProvider serviceProvider,
        ICheckIsInitialized checkIsInitialized, 
        ILoadSettings loadSettings, 
        IUpdateSettings updateSettings)
    {
        this.serviceProvider = serviceProvider;
        this.checkIsInitialized = checkIsInitialized;
        this.loadSettings = loadSettings;
        this.updateSettings = updateSettings;
    }

    public Settings Settings { get; private set; } = Settings.Empty;
    
    public bool IsInitalized { get; private set; }

    public async Task Init()
    {
        this.IsInitalized = await this.checkIsInitialized.ExecuteAsync();

        if (this.IsInitalized)
        {
            this.Settings = await this.loadSettings.ExecuteAsync();
        }
    }
    
    public async Task UpdateSettings(Settings settings)
    {
        await this.updateSettings.ExecuteAsync(settings);
        this.Settings = settings;
        this.IsInitalized = true;
    }

    public ICameraControl GetConfiguredCameraControl()
    {
        return this.serviceProvider.GetRequiredKeyedService<ICameraControl>(this.Settings.CurrentCameraControlKey);
    }
}