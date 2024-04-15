using Avalonia.Controls;

namespace PiPhotoBooth.Views;

using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

public partial class MainWindow : Window
{
    private int selectedWaitTimeInSeconds = 3;

    public MainWindow()
    {
        InitializeComponent();
    }

    private async Task ShowCountdown()
    {
        this.PhotoButton.IsVisible = false;
        this.CountdownLabel.IsVisible = true;
        this.ProgressBar.IsVisible = true;

        for (int countdown = this.selectedWaitTimeInSeconds; countdown > 0; countdown--)
        {
            var percentage = 100 / this.selectedWaitTimeInSeconds * countdown;
            
            this.CountdownLabel.Text = countdown.ToString();
            this.ProgressBar.Value = percentage;

            await Task.Delay(1000);
        }

        this.PhotoButton.IsVisible = true;
        this.CountdownLabel.IsVisible = false;
        this.ProgressBar.IsVisible = false;
        
        this.PhotoTab.Background = Brushes.White;
        await Task.Delay(150);
        this.PhotoTab.Background = Brushes.Transparent;
    }

    private void PhotoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.ShowCountdown();
    }   

    private void PhotoButton_OnTapped(object? sender, TappedEventArgs e)
    {
        this.ShowCountdown();
    }

    private void ToggleButton_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        var radioButton = (RadioButton)e.Source;

        if (radioButton.IsChecked == true)
        {
            this.selectedWaitTimeInSeconds = int.Parse(radioButton.Tag.ToString());
        }
    }
}