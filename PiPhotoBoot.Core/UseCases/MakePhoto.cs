namespace PiPhotoBoot.UseCases;

using PiPhotoBooth.Repository;
using ResultMonad;

public interface IMakePhoto
{
    Task<ResultWithError<string>> ExecuteAsync();
}

internal sealed class MakePhoto : IMakePhoto
{
    private readonly IRepository repository;
    private readonly GPhoto2 gPhoto2 = new();

    public MakePhoto(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ResultWithError<string>> ExecuteAsync()
    {
        // get filename
        var index = await this.repository.GetNextIndexAsync();
        var filename = $"{DateTime.Today :yyyyMMdd}-{index}";
        var result = await this.gPhoto2.CaptureImageAndDownloadAsync(filename);
        
        if (result.IsFailure)
        {
            var log = $"{Environment.NewLine}{DateTime.Now:s}: {result.Error}";
            await File.AppendAllTextAsync("/Users/maddin/piPhotoBoot.log", log);
            return result;
        }

        await this.repository.UpdateIndexAsync(index);
        return ResultWithError.Ok<string>();
    }
}