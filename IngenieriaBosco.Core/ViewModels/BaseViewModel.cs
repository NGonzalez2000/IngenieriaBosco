using IngenieriaBosco.Front.Dialogs.MessageDialog;
using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.ViewModels
{
    public abstract class BaseViewModel : INotify
    {
        protected readonly ISnackbarMessageQueue? snackbarMessageQueue;
        abstract public void Load();
        public BaseViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            this.snackbarMessageQueue = snackbarMessageQueue;
        }
        protected void ShowSnackbarMessage(string Message)
        {
            Task.Factory.StartNew(() => Thread.Sleep(500)).ContinueWith(t =>
            {
                snackbarMessageQueue?.Enqueue(Message);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        protected static async Task<bool> AcceptCancelCall(string message, string Dialog = "RootDialog")
        {
            if (!DialogHost.IsDialogOpen(Dialog))
            {
                var view = new AcceptCancelDialog(message);
                var result = await DialogHost.Show(view, Dialog);
                if (result != null && (bool)result) return true;
            }
            return false;

        }
        protected static async Task AcceptCall(string message, string dialogIdentifier = "RootDialog")
        {
            if (!DialogHost.IsDialogOpen(dialogIdentifier))
            {
                var view = new AcceptDialog(message);
                _ = await DialogHost.Show(view, dialogIdentifier);
            }

        }
    }
}
