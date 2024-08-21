namespace PiPhotoBooth.ViewModels;

using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;
using ResultMonad;
using UseCases;

public sealed class PhotoViewModel : ViewModelBase
{
    private const int PreviewTimeInMs = 10_000;
    
    private readonly IMakePhoto makePhoto;
    private readonly ILoadLastPhoto loadLastPhoto;
    private readonly IDeleteLastPhoto deleteLastPhoto;
    
    private bool isThreeSecondsTimerSelected;
    private bool isFiveSecondsTimerSelected;
    private bool isTenSecondsTimerSelected;

    private bool isPhotoButtonVisible;
    private bool isCountdownLabelVisible;
    private bool isCountdownProgressBarVisible;
    
    private bool isLoadPhotoProgressBarVisible;

    private string countdownLabelText;
    private int progressBarValue;

    private IImmutableSolidColorBrush photoTabBackground;
    
    private Bitmap? lastImage;
    private bool isLastImageVisible;

    private CancellationTokenSource previewCancellationTokenSource = new();

    public PhotoViewModel(
        IMakePhoto makePhoto,
        ILoadLastPhoto loadLastPhoto, IDeleteLastPhoto deleteLastPhoto)
    {
        this.makePhoto = makePhoto;
        this.loadLastPhoto = loadLastPhoto;
        this.deleteLastPhoto = deleteLastPhoto;

        this.IsThreeSecondsTimerSelected = true;
        this.IsPhotoButtonVisible = true;
        this.IsCountdownLabelVisible = false;
        this.IsCountdownProgressBarVisible = false;
        this.isLoadPhotoProgressBarVisible = false;

        this.CountdownLabelText = "0";
        this.ProgressBarValue = 100;
    }
    
    public bool IsThreeSecondsTimerSelected
    {
        get => isThreeSecondsTimerSelected;
        set => this.RaiseAndSetIfChanged(ref this.isThreeSecondsTimerSelected, value);
    }
    
    public bool IsFiveSecondsTimerSelected
    {
        get => isFiveSecondsTimerSelected;
        set => this.RaiseAndSetIfChanged(ref this.isFiveSecondsTimerSelected, value);
    }
    
    public bool IsTenSecondsTimerSelected
    {
        get => isTenSecondsTimerSelected;
        set => this.RaiseAndSetIfChanged(ref this.isTenSecondsTimerSelected, value);
    }
    
    public bool IsPhotoButtonVisible
    {
        get => isPhotoButtonVisible;
        private set => this.RaiseAndSetIfChanged(ref this.isPhotoButtonVisible, value);
    }
    
    public bool IsCountdownLabelVisible
    {
        get => isCountdownLabelVisible;
        private set => this.RaiseAndSetIfChanged(ref this.isCountdownLabelVisible, value);
    }
    
    public bool IsCountdownProgressBarVisible
    {
        get => isCountdownProgressBarVisible;
        private set => this.RaiseAndSetIfChanged(ref this.isCountdownProgressBarVisible, value);
    }
    
    public bool IsLoadPhotoProgressBarVisible
    {
        get => isLoadPhotoProgressBarVisible;
        private set => this.RaiseAndSetIfChanged(ref this.isLoadPhotoProgressBarVisible, value);
    }
    
    public string CountdownLabelText
    {
        get => countdownLabelText;
        private set => this.RaiseAndSetIfChanged(ref this.countdownLabelText, value);
    }
    
    public int ProgressBarValue
    {
        get => progressBarValue;
        private set => this.RaiseAndSetIfChanged(ref this.progressBarValue, value);
    }
    
    public IImmutableSolidColorBrush PhotoTabBackground
    {
        get => photoTabBackground;
        private set => this.RaiseAndSetIfChanged(ref this.photoTabBackground, value);
    }
    
    public Bitmap? LastImage
    {
        get => lastImage;
        private set => this.RaiseAndSetIfChanged(ref lastImage, value);
    }
    
    public bool IsLastImageVisible
    {
        get => isLastImageVisible;
        private set => this.RaiseAndSetIfChanged(ref this.isLastImageVisible, value);
    }
    
    public async void PhotoButtonActivated()
    {
        await this.TakeNewPhoto();
    }

    public async Task KeepPhoto()
    {
        await this.previewCancellationTokenSource.CancelAsync();
    }

    public async Task DiscardPhotoAsync()
    {
        try
        {
            await this.deleteLastPhoto.ExecuteAsync();   
            await this.previewCancellationTokenSource.CancelAsync();
        }
        catch (Exception e)
        {
            // todo: error handling
        }
    }
    
    private async Task TakeNewPhoto()
    {
        this.IsPhotoButtonVisible = false;
        
        await ShowCountdown();
        var result = await MakePhoto();
        
        if (result.IsSuccess)
        {
            await ShowPhoto();
        }
        
        this.IsPhotoButtonVisible = true;
        this.LastImage = null;
    }

    private async Task ShowCountdown()
    {
        this.IsCountdownLabelVisible = true;
        this.IsCountdownProgressBarVisible = true;

        for (int countdown = this.GetSelectedTimer(); countdown > 0; countdown--)
        {
            var percentage = 100 / this.GetSelectedTimer() * countdown;
            
            this.CountdownLabelText = countdown.ToString();
            this.ProgressBarValue = percentage;
            
            await Task.Delay(1000);
        }
        
        this.IsCountdownLabelVisible = false;
        this.IsCountdownProgressBarVisible = false;
    }

    private async Task<Result> MakePhoto()
    {
        this.PhotoTabBackground = Brushes.White;
        await Task.Delay(150);
        this.PhotoTabBackground = Brushes.Transparent;

        this.IsLoadPhotoProgressBarVisible = true;
        var result = await this.makePhoto.ExecuteAsync();

        if (result.IsFailure)
        {
            this.IsLoadPhotoProgressBarVisible = false;
         
            // todo: error handling
            // this.ErrorMessage = result.Error;
            // this.IsErrorMessageVisible = true;

            await Task.Delay(15000);
            
            // this.ErrorMessage = string.Empty;
            // this.IsErrorMessageVisible = false;
            
            return Result.Fail();
        }
        
        return Result.Ok();
    }

    private async Task ShowPhoto()
    {
        this.previewCancellationTokenSource = new CancellationTokenSource();
        var previewCancellationToken = previewCancellationTokenSource.Token;

        var maybeStream = await this.loadLastPhoto.ExecuteAsync();

        if (maybeStream.HasNoValue)
        {
            // todo: show error
            return;
        }
        
        await using var imageStream = maybeStream.Value;
        this.LastImage = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 1024));
        
        this.IsLoadPhotoProgressBarVisible = false;

        this.IsLastImageVisible = true;

        try
        {
            await Task.Delay(PreviewTimeInMs, previewCancellationToken);
        }
        catch (TaskCanceledException)
        {
        }
        
        this.IsLastImageVisible = false;
    }

    private int GetSelectedTimer()
    {
        if (this.IsThreeSecondsTimerSelected)
            return 3;

        if (this.isFiveSecondsTimerSelected)
            return 5;

        return 10;
    }
}