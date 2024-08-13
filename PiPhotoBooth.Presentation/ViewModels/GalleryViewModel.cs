namespace PiPhotoBooth.ViewModels;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using Model;
using UseCases;

public class GalleryViewModel
{
    private readonly ILoadSavedPhotos loadSavedPhotos;

    public GalleryViewModel(ILoadSavedPhotos loadSavedPhotos)
    {
        this.loadSavedPhotos = loadSavedPhotos;
    }

    public ObservableCollection<ImageViewModel> Photos { get; } = new();
    
    public ImageViewModel? SelectedPhoto { get; set; }

    public async Task OnActivated()
    {
        this.Photos.Clear();

        var photos = await this.loadSavedPhotos.ExecuteAsync();

        foreach (var photo in photos)
        {
            var imageViewModel = new ImageViewModel(photo);
            await imageViewModel.LoadPhoto();
            this.Photos.Add(imageViewModel);
        }
    }
}