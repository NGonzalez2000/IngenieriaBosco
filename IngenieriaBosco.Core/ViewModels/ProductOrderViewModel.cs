using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Front.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class ProductOrderViewModel : BaseViewModel
    {
        public ProductOrderViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
        }
        public ICommand NewOrder_Command => new RelayCommand(_ => NewOrder_Execute());
        public ICommand DeleteOrder_Command => new RelayCommand(_ => DeleteOrder_Execute());
        public GridListModel<ProductOrderModel>? Orders { get; set; }

        public override async void Load()
        {
            try
            {
                Orders = new()
                {
                    Collection = new(await DBProductOrder.SelectAll())
                };
                foreach (ProductOrderModel orderModel in Orders.Collection)
                    orderModel.Provider = (await DBProductOrder.GetProvider(orderModel.Id)).First();
            }
            catch(Exception ex)
            {
                await AcceptCall("Error al cargar los pedidos.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.ProductOrder_Identifier);
                Orders = new();
                return;
            }
            OnPropertyChanged(nameof(Orders));
        }
        private async void NewOrder_Execute()
        {
            SelectProviderDialogModel dialogModel = new(await (DBProvider.SelectAll()));
            ProviderModel? provider = await dialogModel.GetProvider(DialogIdentifiers.ProductOrder_Identifier);
            if (provider == null) return;


            ProductOrderWindowModel dataContext = new(snackbarMessageQueue!);
            dataContext.Load();
            dataContext.ProductOrder!.Provider = provider;
            ProductOrderWindow productsWindow = new()
            {
                DataContext = dataContext
            };

            productsWindow.Show();
        }
        private async void DeleteOrder_Execute()
        {
            if (Orders!.SelectedItem == null) return;
            try
            {
                await DBProductOrder.Delete(Orders.SelectedItem.Id);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al eliminar el pedido.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.ProductOrder_Identifier);
                return;
            }

            Orders.Delete(Orders.SelectedItem);
            ShowSnackbarMessage("Pedido eliminado con éxito");
        }
    }
}
