namespace PiPhotoBoot;

using PiPhotoBooth;
using ResultMonad;

public class CameraControl : ICameraControl
{
    private const string ImageUrl = "https://picsum.photos/1920/1080";

    public Task<bool> IsOnlineAsync()
    {
        return Task.FromResult(true);
    }

    public async Task<ResultWithError<string>> TakePhotoAsync(string filename)
    {
        using (HttpClient client = new HttpClient())
        {
            // Send asynchronous GET request to the imageUrl
            HttpResponseMessage response = await client.GetAsync(ImageUrl);
            response.EnsureSuccessStatusCode();

            using (Stream contentStream = await response.Content.ReadAsStreamAsync(), 
                   fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Copy the contentStream to the fileStream
                await contentStream.CopyToAsync(fileStream);
            }
        }

        return ResultWithError.Ok<string>();
    }
}
