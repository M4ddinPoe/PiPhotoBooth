namespace PiPhotoBooth.Messages;

using Mediator;

public sealed class ErrorMessage : INotification
{
    public string Message { get; init; }
}