namespace PiPhotoBooth.ViewModels;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Settings.UseCases;
using UseCases;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ICheckCameraConnected checkCameraConnected;
    private readonly ICheckIsInitialized checkIsInitialized;

    private WindowState selectedWindowState = WindowState.Maximized;
    
    private bool isErrorMessageVisible;
    private string errorMessage;
    
    private IImmutableSolidColorBrush isCameraOnlineBrush;

    public MainWindowViewModel(
        ICheckCameraConnected checkCameraConnected, 
        ICheckIsInitialized checkIsInitialized,
        IServiceProvider services)
    {
        // this.loadLastPhoto = loadLastPhoto;
        // this.makePhoto = makePhoto;
        this.checkCameraConnected = checkCameraConnected;
        this.checkIsInitialized = checkIsInitialized;

        this.IsErrorMessageVisible = false;
        this.ErrorMessage = string.Empty;
        this.IsCameraOnlineBrush = Brushes.Red;

        var checkOnlineTimer = new Timer();
        checkOnlineTimer.Interval = 2000;
        checkOnlineTimer.Elapsed += CheckOnlineTimerOnElapsed;
        checkOnlineTimer.Start();

        ShowDialog = new Interaction<SettingsWindowViewModel, object?>();

        OpenSettingsCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                var store = services.GetRequiredService<SettingsWindowViewModel>();
                await ShowDialog.Handle(store);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
        
        this.CheckOnlineState();
    }

    public ICommand OpenSettingsCommand { get; }
    
    public Interaction<SettingsWindowViewModel, object?> ShowDialog { get; }
    
    public WindowState SelectedWindowState
    {
        get => this.selectedWindowState;
        set => this.RaiseAndSetIfChanged(ref this.selectedWindowState, value);
    }
    
    public bool IsErrorMessageVisible
    {
        get => this.isErrorMessageVisible;
        set => this.RaiseAndSetIfChanged(ref this.isErrorMessageVisible, value);
    }
    
    public string ErrorMessage
    {
        get => this.errorMessage;
        set => this.RaiseAndSetIfChanged(ref this.errorMessage, value);
    }

    public IImmutableSolidColorBrush IsCameraOnlineBrush
    {
        get => this.isCameraOnlineBrush;
        set => this.RaiseAndSetIfChanged(ref this.isCameraOnlineBrush, value);
    }

    public async void InitializeIfNotAlreadyDone()
    {
        try
        {
            var isInitialized = await this.checkIsInitialized.ExecuteAsync();

            if (!isInitialized)
            {
                this.OpenSettingsCommand.Execute(null);
            }
        }
        catch (Exception exception)
        {
            // todo: Error handling
            Console.WriteLine(exception);
        }
    }
    
    private async Task CheckOnlineState()
    {
        IsCameraOnlineBrush = await this.checkCameraConnected.ExecuteAsync()
            ? Brushes.Green
            : Brushes.Red;
    }
    
    private async void CheckOnlineTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        await this.CheckOnlineState();
    }
}