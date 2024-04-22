using Avalonia.Controls;

namespace PiPhotoBooth.Views;

using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ViewModels;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(action => 
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(InteractionContext<SettingsWindowViewModel, object?> interaction)
    {
        var dialog = new SettingsWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<object?>(this);
        interaction.SetOutput(result);
    }
}