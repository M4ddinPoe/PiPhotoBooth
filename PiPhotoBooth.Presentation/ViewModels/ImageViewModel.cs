namespace PiPhotoBooth.ViewModels;

using System;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Model;
using ReactiveUI;

public class ImageViewModel : ViewModelBase
{
    private readonly Photo photo;
    private Bitmap? image;

    public ImageViewModel(Photo photo)
    {
        this.photo = photo;
    }

    public DateTime Taken => this.photo.Taken;
    
    public Bitmap? Image
    {
        get => this.image;
        private set => this.RaiseAndSetIfChanged(ref this.image, value);
    }
    
    public async Task LoadPhoto()
    {
        await using (var imageStream =await photo.LoadAsync())
        {
            Image = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
    }
}