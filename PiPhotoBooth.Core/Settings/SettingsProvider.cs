namespace PiPhotoBooth.Services;

using Model;
using Settings.UseCases;

public sealed class SettingsProvider
{
    private readonly ICheckIsInitialized checkIsInitialized;
    private readonly ILoadSettings loadSettings;
    private readonly IUpdateSettings updateSettings;

    public SettingsProvider(
        ICheckIsInitialized checkIsInitialized, ILoadSettings loadSettings, IUpdateSettings updateSettings)
    {
        this.checkIsInitialized = checkIsInitialized;
        this.loadSettings = loadSettings;
        this.updateSettings = updateSettings;
    }
    
    public bool IsInitalized { get; private set; }
    
    public Settings Settings { get; private set; }

    public async Task Init()
    {
        this.IsInitalized = await this.checkIsInitialized.ExecuteAsync();
    }
    
    public async Task UpdateSettings(Settings settings)
    {
        await this.updateSettings.ExecuteAsync(settings);
        this.Settings = settings;
        this.IsInitalized = true;
    }
}