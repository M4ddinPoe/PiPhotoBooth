namespace PiPhotoBooth.UseCases;

using Model;
using ResultMonad;
using Services;

public interface IMakePhoto
{
    Task<ResultWithError<string>> ExecuteAsync();
}

internal sealed class MakePhoto : IMakePhoto
{
    private readonly IRepository repository;
    private readonly ICameraControl cameraControl;
    private readonly SettingsProvider settingsProvider;

    public MakePhoto(IRepository repository, SettingsProvider settingsProvider)
    {
        this.repository = repository;
        this.cameraControl = settingsProvider.GetConfiguredCameraControl();
        this.settingsProvider = settingsProvider;
    }

    public async Task<ResultWithError<string>> ExecuteAsync()
    {
        // get filename
        var index = await this.repository.GetNextIndexAsync();
        var filename = $"{DateTime.Today :yyyyMMdd}-{index}.jpg";
        var path = Path.Combine(this.settingsProvider.Settings.DataDirectory, filename);

        var result = await this.cameraControl.TakePhotoAsync(path); 
        
        if (result.IsFailure)
        {
            var log = $"{Environment.NewLine}{DateTime.Now:s}: {result.Error}";
            await File.AppendAllTextAsync("/Users/maddin/piPhotoBoot.log", log);
            return result;
        }

        var photo = new Photo(path);

        await this.repository.UpdateIndexAsync(index);
        await this.repository.AddPhoto(photo);

        return ResultWithError.Ok<string>();
    }
}