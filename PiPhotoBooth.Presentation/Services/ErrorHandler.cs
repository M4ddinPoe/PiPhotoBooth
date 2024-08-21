namespace PiPhotoBooth.Services;

using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using Material.Styles.Controls;
using Material.Styles.Models;
using Mediator;
using Messages;

internal class ErrorHandler: INotificationHandler<ErrorMessage>
{
    private SnackbarHost snackbarHost;

    public void Register(SnackbarHost snackbarHost)
    {
        this.snackbarHost = snackbarHost;
    }

    public ValueTask Handle(ErrorMessage notification, CancellationToken cancellationToken)
    {
        var snackbar = new SnackbarModel(notification.Message);
        SnackbarHost.Post(snackbar, this.snackbarHost.HostName, DispatcherPriority.Normal);

        return default;
    }
}