using Avalonia.Controls;

namespace PiPhotoBooth.Views;

using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ViewModels;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow(
        PhotoView photoView,
        GalleryView galleryView)
    {
        InitializeComponent();

        this.PhotoViewPlaceholder.Content = photoView;
        this.GalleryViewPlaceholder.Content = galleryView;
        
        this.WhenActivated(action =>
        {
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync));
            var mainWindowViewModel = this.DataContext as MainWindowViewModel;

            mainWindowViewModel?.InitializeIfNotAlreadyDone();
        });
        
    }
    
    private async Task DoShowDialogAsync(InteractionContext<SettingsWindowViewModel, object?> interaction)
    {
        var dialog = new SettingsWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<object?>(this);
        interaction.SetOutput(result);
    }
}