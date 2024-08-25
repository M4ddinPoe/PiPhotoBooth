// namespace PiPhotoBooth.Services;
//
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Avalonia.Threading;
// using Material.Styles.Controls;
// using Material.Styles.Models;
// using Mediator;
// using Messages;
//
// internal class ErrorHandler : INotificationHandler<ErrorMessage>
// {
//     private SnackbarHost snackbarHost;
//
//     public void Register(SnackbarHost snackbarHost)
//     {
//         this.snackbarHost = snackbarHost;
//     }
//
//     public async ValueTask Handle(ErrorMessage notification, CancellationToken cancellationToken)
//     {
//         var snackbar = new SnackbarModel(notification.Message, TimeSpan.FromSeconds(5));
//         SnackbarHost.Post(snackbar, this.snackbarHost.HostName, DispatcherPriority.Normal);
//         this.snackbarHost.IsVisible = true;
//     }
// }