namespace PiPhotoBooth.ViewModels;

using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Messages;

public class ErrorsViewModel : ViewModelBase, INotificationHandler<ErrorMessage>
{
    public ObservableCollection<ErrorMessage> ErrorMessages { get; } = new();
    
    public async ValueTask Handle(ErrorMessage notification, CancellationToken cancellationToken)
    {
        this.ErrorMessages.Add(notification);
        await Task.Delay(5000);
        this.ErrorMessages.Remove(notification);
    }
}
