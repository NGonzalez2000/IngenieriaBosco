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
    internal class ClientDialogModel : BaseDialogModel
    {
        public ClientModel Client { get; set; }
        public ICommand AddEmailCommand => new RelayCommand(_ => NewEmailExecute());
        public ClientDialogModel(ClientModel? client = null)
        {
            Client = new();
            if (client != null) Client = (ClientModel)client.ShallowCopy();
        }
        public async Task<ClientModel?> NewClient()
        {
            ClientDialog dialog = new("Nuevo Cliente")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Client_Identifier, closingEventHandler: ClosingEventHandler_New);
            if (result) return Client;
            return null;
        }
        public async Task<ClientModel?> EditClient()
        {
            ClientDialog dialog = new("Editar Cliente")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Client_Identifier, closingEventHandler: ClosingEventHandler_Edit);
            if (result) return Client;
            return null;
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Client[nameof(Client.FirstName)] == string.Empty &&
                Client[nameof(Client.FirstName)] == string.Empty &&
                Client[nameof(Client.CUIL)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Client));
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Client[nameof(Client.FirstName)] == string.Empty &&
                Client[nameof(Client.LastName)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Client));
        }

        public void NewEmailExecute()
        {
            if (Client.Emails is null) Client.Emails = new();
            Client.Emails.Add(new());
            OnPropertyChanged(nameof(Client));
        }

        
    }
}
