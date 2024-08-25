using Avalonia.Controls;

namespace PiPhotoBooth.Views;

using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Services;
using ViewModels;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    //private readonly ErrorHandler errorHandler;
    
    public MainWindow(
        PhotoView photoView,
        GalleryView galleryView,
        ErrorsView errorsView)
    {
        InitializeComponent();

        this.PhotoViewPlaceholder.Content = photoView;
        this.GalleryViewPlaceholder.Content = galleryView;
        this.ErrorsViewPlaceholder.Content = errorsView;
        
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