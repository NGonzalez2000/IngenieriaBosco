using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels.SellDialogs
{
    internal class ClientDialogModel : INotify
    {
        public List<ClientModel> Clients { get; set; }
        private ClientModel? tempClient;
        private ClientModel? selectedClient;
        public ClientModel? TempClient
        {
            get => tempClient;
            set => SetProperty(ref tempClient, value);
        }
        public ClientModel? SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }
        private bool isClient;

        public bool IsClient
        {
            get { return isClient; }
            set
            {
                if (SetProperty(ref isClient, value))
                {
                    if (isClient) SelectedClient = null;
                    else TempClient = new();
                }
            }
        }
        private bool canContinue;

        public bool CanContinue
        {
            get { return canContinue; }
            set { canContinue = value; }
        }

        public ClientDialogModel()
        {
            Clients = new();
            SetClients();
            IsClient = true;
        }
        public ClientModel? GetClient()
        {
            if (IsClient) return SelectedClient;
            return TempClient;
        }

        private async void SetClients()
        {
            Clients = new(await DBClient.SelectAll());
            OnPropertyChanged(nameof(Clients));
        }

        internal void SetClient(string firstName, string lastName, string cuil)
        {
            ClientModel client = new()
            {
                FirstName = firstName,
                LastName = lastName,
                CUIL = cuil
            };
            IsClient = false;
            TempClient = client;
        }
    }
}
