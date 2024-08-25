namespace PiPhotoBooth.ViewModels;

using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Mediator;
using Messages;
using ReactiveUI;
using ResultMonad;
using UseCases;

public sealed class PhotoViewModel : ViewModelBase
{
    private const int PreviewTimeInMs = 10_000;
    
    private readonly IMakePhoto makePhoto;
    private readonly ILoadLastPhoto loadLastPhoto;
    private readonly IDeleteLastPhoto deleteLastPhoto;
    private readonly IMediator mediator;
    
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
        ILoadLastPhoto loadLastPhoto, 
        IDeleteLastPhoto deleteLastPhoto,
        IMediator mediator)
    {
        this.makePhoto = makePhoto;
        this.loadLastPhoto = loadLastPhoto;
        this.deleteLastPhoto = deleteLastPhoto;
        this.mediator = mediator;

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
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
    }
    
    private async Task TakeNewPhoto()
    {
        try
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
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
    }

    private async Task ShowCountdown()
    {
        try
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
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
    }

    private async Task<Result> MakePhoto()
    {
        try
        {
            this.PhotoTabBackground = Brushes.White;
            await Task.Delay(150);
            this.PhotoTabBackground = Brushes.Transparent;

            this.IsLoadPhotoProgressBarVisible = true;
            var result = await this.makePhoto.ExecuteAsync();

            if (result.IsFailure)
            {
                this.IsLoadPhotoProgressBarVisible = false;
                
                var message = new ErrorMessage { Message = result.Error };
                await mediator.Publish(message);
            
                return Result.Fail();
            }
        
            return Result.Ok();
        }
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
            
            return Result.Fail();
        }
    }

    private async Task ShowPhoto()
    {
        try
        {
            this.previewCancellationTokenSource = new CancellationTokenSource();
            var previewCancellationToken = previewCancellationTokenSource.Token;

            var maybeStream = await this.loadLastPhoto.ExecuteAsync();

            if (maybeStream.HasNoValue)
            {
                var message = new ErrorMessage { Message = "Last photo was not found" };
                await mediator.Publish(message);
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
        catch (Exception exception)
        {
            var message = new ErrorMessage { Message = $"{exception.GetType()}: {exception.Message}" };
            await mediator.Publish(message);
        }
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