using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PiPhotoBooth.Views;

using Avalonia.ReactiveUI;
using ViewModels;

public partial class PhotoView : ReactiveUserControl<PhotoViewModel>
{
    public PhotoView(PhotoViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}