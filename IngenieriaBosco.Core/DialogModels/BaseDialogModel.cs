using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IngenieriaBosco.Core.DialogModels
{
    public class BaseDialogModel : INotify
    {
        protected static async Task<bool> DialogHosting(UserControl view, string Dialog = "RootDialog",
                                                            DialogOpenedEventHandler? openedEventHandler = null,
                                                            DialogClosingEventHandler? closingEventHandler = null)
        {
            if (DialogHost.IsDialogOpen(Dialog) || view == null) return false;
            object? result = await DialogHost.Show(view, Dialog, openedEventHandler, closingEventHandler);
            if (result == null || (bool)result == true) return true;
            return false;
        }
    }
}
