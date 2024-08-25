namespace PiPhotoBooth.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mediator;
using Messages;
using UseCases;

public class GalleryViewModel
{
    private readonly ILoadSavedPhotos loadSavedPhotos;
    private readonly IMediator mediator;

    public GalleryViewModel(ILoadSavedPhotos loadSavedPhotos, IMediator mediator)
    {
        this.loadSavedPhotos = loadSavedPhotos;
        this.mediator = mediator;
    }

    public ObservableCollection<ImageViewModel> Photos { get; } = new();
    
    public ImageViewModel? SelectedPhoto { get; set; }

    public async Task OnActivated()
    {
        try
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
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
    }
}
