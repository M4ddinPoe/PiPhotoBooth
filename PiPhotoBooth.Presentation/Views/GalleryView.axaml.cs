namespace PiPhotoBooth.Views;

using Avalonia.ReactiveUI;
using ReactiveUI;
using ViewModels;

public partial class GalleryView : ReactiveUserControl<GalleryViewModel>
{
    public GalleryView(GalleryViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;

        this.WhenActivated(action =>
        {
            (this.DataContext as GalleryViewModel)?.OnActivated();
        });
    }
}
