using MaterialDesignThemes.Wpf;

namespace IngenieriaBosco.Core.ViewModels
{
    public abstract class BaseViewModel
    {
        protected readonly ISnackbarMessageQueue? snackbarMessageQueue;
        public BaseViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            this.snackbarMessageQueue = snackbarMessageQueue;
        }
        abstract public void Load();
    }
}
