using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.DialogModels
{
    public class ProviderDialogModel : BaseDialogModel
    {
        public ProviderModel Provider { get; set; }
        public ICommand AddEmailCommand => new RelayCommand(_ => NewEmailExecute()); 
        public ProviderDialogModel(ProviderModel? provider = null)
        {
            Provider = new();
            if(provider != null)Provider = (ProviderModel)provider.ShallowCopy();
        }
        public async Task<ProviderModel?> NewProvider()
        {
            ProviderDialog dialog = new("Nuevo Proveedor")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Provider_Identifier, closingEventHandler: ClosingEventHandler_New);
            if (result) return Provider;
            return null;
        }
        public async Task<ProviderModel?> EditProvider()
        {
            ProviderDialog dialog = new("Editar Proveedor")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Provider_Identifier, closingEventHandler: ClosingEventHandler_Edit);
            if (result) return Provider;
            return null;
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Provider[nameof(Provider.Name)] == string.Empty &&
                Provider[nameof(Provider.CUIT)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Provider));
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Provider[nameof(Provider.Name)] == string.Empty &&
                Provider[nameof(Provider.CUIT)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Provider));
        }

        public void NewEmailExecute()
        {
            if (Provider.Emails is null) Provider.Emails = new();
            Provider.Emails.Add(new());
            OnPropertyChanged(nameof(Provider));
        }
    }
}
