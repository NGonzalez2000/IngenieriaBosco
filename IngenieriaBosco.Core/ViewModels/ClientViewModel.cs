using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Filters;
using IngenieriaBosco.Core.Models.Generics;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class ClientViewModel : BaseViewModel
    {
        public GridListModel<ClientModel>? ClientList { get; set; }
        public ICommand NewClientCommand => new RelayCommand(_ => NewClientExecute());
        public ICommand EditClientCommand => new RelayCommand(_ => EditClientExecute());
        public ICommand DeleteClientCommand => new RelayCommand(_ => DeleteClientExecute());
        public ClientFilterModel? ClientFilter { get; set; }
        public ClientSortModel? ClientSort { get; set; }
        public ICommand FilterCommand => new RelayCommand(_ => FilterExecute());
        public ICommand SortCommand => new RelayCommand(_ => SortExecute());
        public ClientViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
        }

        public override async void Load()
        {
            ClientFilter = new();
            ClientSort = new();
            ClientList = new( await DBClient.SelectAll());
            foreach (ClientModel client in ClientList.Collection)
                client.Emails = new(await DBClient.SelectEmails(client));

            OnPropertyChanged(nameof(ClientList));
        }

        private async void NewClientExecute()
        {
            ClientDialogModel dialogModel = new();
            ClientModel?client = await dialogModel.NewClient();
            if (client is null) return;

            try
            {
                await DBClient.Insert(client);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al crear el cliente\n\n" + ex.GetBaseException().Message,
                                    DialogIdentifiers.Client_Identifier);
                return;
            }

            try
            {
                client.Id = await DBAccess.GetId("Clients");
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al cargar el identificador del cliente\n\n" + ex.GetBaseException().Message
                                    + "\n\nLa página se actualizará",
                                    DialogIdentifiers.Client_Identifier);
                Load();
            }

            ProcessEmails(client);

            ClientList!.Insert(client);

            ShowSnackbarMessage("Cliente agregado con éxito");
        }
        private async void EditClientExecute()
        {
            if (ClientList!.SelectedItem is null) return;

            ClientDialogModel dialogModel = new(ClientList.SelectedItem);
            ClientModel? client = await dialogModel.EditClient();

            if(client is null) return;

            try
            {
                await DBClient.Update(client);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al actualizar el cliente\n\n" + ex.GetBaseException().Message,
                                    DialogIdentifiers.Client_Identifier);
                return;
            }

            ProcessEmails(client);

            ClientList.Edit(ClientList.SelectedItem, client);

            ShowSnackbarMessage("Cliente editado con éxito");
        }
        private async void DeleteClientExecute()
        {
            if(ClientList!.SelectedItem is null) return;

            bool response = await AcceptCancelCall($"Seguro que desea eliminar al cliente {ClientList.SelectedItem.FirstName} {ClientList.SelectedItem.LastName}",
                                                    DialogIdentifiers.Client_Identifier);
            if (!response) return;

            try
            {
                await DBClient.Delete(ClientList.SelectedItem);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al eliminar el cliente\n\n" + ex.GetBaseException().Message,
                    DialogIdentifiers.Client_Identifier);
                return;
            }

            ClientList.Delete(ClientList.SelectedItem);

            ShowSnackbarMessage("Cliente eliminado con éxito");
        }
        private static async void ProcessEmails(ClientModel client)
        {
            if (client.Emails is null) return;
            foreach (EmailModel email in client.Emails)
            {
                if (string.IsNullOrEmpty(email.Email))
                {
                    if (email.Id != -1)
                    {
                        try
                        {
                            await DBClient.DeleteEmail(email);
                        }
                        catch (Exception ex)
                        {
                            await AcceptCall("Ha ocurrido un error al eliminar un Email del proveedor\n\n" + ex.GetBaseException().Message,
                                              DialogIdentifiers.Client_Identifier);
                        }
                    }
                }
                else if (email.Id == -1)
                {
                    try
                    {
                        await DBClient.InsertEmail(client, email);
                    }
                    catch (Exception ex)
                    {
                        await AcceptCall($"Error al ingersar el Email {email.Email}\n\n" + ex.GetBaseException().Message,
                                           DialogIdentifiers.Client_Identifier);
                    }
                    try
                    {
                        email.Id = await DBAccess.GetId("ClientEmails");
                    }
                    catch (Exception ex)
                    {
                        await AcceptCall($"Error al cargar el identificador del Email {email.Email}\n\n" + ex.GetBaseException().Message
                                         + "\n\nPor favor reinicie la página al finalizar",
                                           DialogIdentifiers.Client_Identifier);
                    }
                }
                else
                {
                    try
                    {
                        await DBClient.UpdateEmail(email);
                    }
                    catch (Exception ex)
                    {
                        await AcceptCall($"Error al actualizar el Email {email.Email}\n\n" + ex.GetBaseException().Message,
                                            DialogIdentifiers.Client_Identifier);
                    }
                }
            }
            int i = 0;
            while (i < client.Emails.Count)
            {
                if (string.IsNullOrEmpty(client.Emails[i].Email)) client.Emails.RemoveAt(i);
                else i++;
            }
        }
        private void FilterExecute()
        {
            if (ClientList is null) return;

            ClientList.FilterExecute(ClientFilter!.Filter);

            OnPropertyChanged(nameof(ClientList));
        }
        private void SortExecute()
            => ClientList!.SortExecute(ClientSort!.OrderBy());
    }
}
