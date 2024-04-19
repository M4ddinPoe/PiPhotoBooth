namespace PiPhotoBooth;

using ResultMonad;

public interface ICameraControl
{
    Task<bool> IsOnlineAsync();

    Task<ResultWithError<string>> TakePhotoAsync(string filename);
}
