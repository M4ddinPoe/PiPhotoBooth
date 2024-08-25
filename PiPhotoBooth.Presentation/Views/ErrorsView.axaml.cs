using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PiPhotoBooth.Views;

using ViewModels;

public partial class ErrorsView : UserControl
{
    public ErrorsView(ErrorsViewModel viewModel)
    {
        this.DataContext = viewModel;
        InitializeComponent();
    }
}
