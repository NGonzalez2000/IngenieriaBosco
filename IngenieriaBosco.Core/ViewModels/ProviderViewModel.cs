using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Filters;
using IngenieriaBosco.Core.Models.Generics;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class ProviderViewModel : BaseViewModel
    {
        public GridListModel<ProviderModel>? ProviderList { get; set; }
        public ProviderViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
        }
        public override async void Load()
        {
            ProviderFilter = new();
            ProviderSort = new();
            ProviderList = new(await DBProvider.SelectAll());
            foreach(ProviderModel provider in ProviderList.Collection)
                provider.Emails = new(await DBProvider.SelectEmails(provider));

            OnPropertyChanged(nameof(ProviderFilter));
            OnPropertyChanged(nameof(ProviderSort));
            OnPropertyChanged(nameof(ProviderList));
        }
        public ICommand NewProviderCommand => new RelayCommand(_ => NewProviderAction());
        public ICommand EditProviderCommand => new RelayCommand(_ => EditProviderAction());
        public ICommand DeleteProviderCommand => new RelayCommand(_ => DeleteProviderAction());
        public ICommand FilterCommand => new RelayCommand(_ => FilterExecute());
        public ICommand SortCommand => new RelayCommand(_ => SortExecute());
        public ProviderFilterModel? ProviderFilter { get; set; }
        public ProviderSortModel? ProviderSort { get; set; }
        private async void NewProviderAction()
        {
            ProviderDialogModel dialogModel = new();
            ProviderModel? providerModel = await dialogModel.NewProvider();

            if (providerModel == null) return;

            try
            {
                await DBProvider.Insert(providerModel);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al crear el proveedor\n\n" + ex.GetBaseException().Message,
                                  DialogIdentifiers.Provider_Identifier);
                return;
            }

            try
            {
                providerModel.Id = await DBAccess.GetId("Providers");
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al cargar el identificador del proveedor\n\nLa pagina se actualizará\n\n" + ex.GetBaseException().Message,
                                  DialogIdentifiers.Provider_Identifier);
                Load();
            }
            ProcessEmails(providerModel);

            ProviderList!.Insert(providerModel);
            ShowSnackbarMessage("Proveedor creado con éxito");
        }
        private async void EditProviderAction()
        {
            if(ProviderList!.SelectedItem == null) return;
            ProviderDialogModel dialogModel = new(ProviderList.SelectedItem);
            ProviderModel? providerModel = await dialogModel.EditProvider();

            if (providerModel == null) return;

            try
            {
                await DBProvider.Update(providerModel);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al editar el proveedor\n\n" + ex.GetBaseException().Message,
                                  DialogIdentifiers.Provider_Identifier);
                return;
            }
            ProcessEmails(providerModel);
            ProviderList.Edit(ProviderList.SelectedItem, providerModel);

            ShowSnackbarMessage("Proveedor editado con éxito");
        }
        private async void DeleteProviderAction()
        {
            if (ProviderList!.SelectedItem == null) return;
            bool response = await AcceptCancelCall($"Seguro que desea eliminar al proveedor {ProviderList.SelectedItem.Name}.",
                                                   DialogIdentifiers.Provider_Identifier);
            if (!response) return;

            try
            {
                await DBProvider.Delete(ProviderList.SelectedItem);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al eliminar el Proveedor\n."
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Provider_Identifier);
                return;
            }

            ProviderList.Delete(ProviderList.SelectedItem);
            ShowSnackbarMessage("Proveedor eliminado con éxito");
        }
        private static async void ProcessEmails(ProviderModel provider)
        {
            if (provider.Emails is null) return;

            foreach(EmailModel email in provider.Emails)
            {
                if (string.IsNullOrEmpty(email.Email))
                {
                    if (email.Id != -1)
                    {
                        try
                        {
                            await DBProvider.DeleteEmail(email);
                        }
                        catch (Exception ex)
                        {
                            await AcceptCall("Ha ocurrido un error al eliminar un Email del proveedor\n\n" + ex.GetBaseException().Message,
                                              DialogIdentifiers.Provider_Identifier);
                        }
                    }
                }
                else if (email.Id == -1)
                {
                    try
                    {
                        await DBProvider.InsertEmail(provider, email);
                    }
                    catch (Exception ex)
                    {
                        await AcceptCall($"Error al ingersar el Email {email.Email}\n\n" + ex.GetBaseException().Message,
                                           DialogIdentifiers.Provider_Identifier);
                    }
                    try
                    {
                        email.Id = await DBAccess.GetId("ProviderEmails");
                    }
                    catch (Exception ex)
                    {
                        await AcceptCall($"Error al cargar el identificador del Email {email.Email}\n\n" + ex.GetBaseException().Message
                                         + "\n\nPor favor reinicie la página al finalizar",
                                           DialogIdentifiers.Provider_Identifier);
                    }
                }
                else
                {
                    try
                    {
                        await DBProvider.UpdateEmail(email);
                    }
                    catch (Exception ex)
                    {
                        await AcceptCall($"Error al actualizar el Email {email.Email}\n\n" + ex.GetBaseException().Message,
                                            DialogIdentifiers.Provider_Identifier);
                    }
                }
                
            }

            int i = 0;
            while(i < provider.Emails.Count)
            {
                if (string.IsNullOrEmpty(provider.Emails[i].Email)) provider.Emails.RemoveAt(i);
                else i++;
            }
        }
        private void FilterExecute()
        {
            if (ProviderList is null) return;

            ProviderList.FilterExecute(ProviderFilter!.Filter);

            OnPropertyChanged(nameof(ProviderList));
        }
        private void SortExecute()
            => ProviderList!.SortExecute(ProviderSort!.OrderBy());
    }
}
