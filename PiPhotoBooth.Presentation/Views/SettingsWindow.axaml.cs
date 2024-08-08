using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PiPhotoBooth.Views;

using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }
    
    private async void SelectPhotoDirectoryButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var directory = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Select Photo Directory",
            AllowMultiple = false,
        });

        if (directory.Count == 1)
        {
            this.PhotoDirectoryTextBox.Text = directory[0].Path.LocalPath;
        }
    }
}