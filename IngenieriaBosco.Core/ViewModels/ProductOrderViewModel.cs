using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Core.Models.Filters;
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
        public ICommand RecivedOrder_Command => new RelayCommand(_ => RecivedOrder_Execute(), _ => RecivedOrder_Enable());
        public ICommand PayedOrder_Command => new RelayCommand(_ => PayedOrder_Execute(), _ => PayedOrder_Enable());
        public ICommand DeleteOrder_Command => new RelayCommand(_ => DeleteOrder_Execute());
        public ICommand FilterCommand => new RelayCommand(_ => FilterExecute());
        public ICommand SortCommand => new RelayCommand(_ => SortExecute());
        public ProductOrderFilterModel? ProductOrderFilter { get; set; }
        public ProductOrderSortModel? ProductOrderSort { get; set; }
        public GridListModel<ProductOrderModel>? Orders { get; set; }

        public override async void Load()
        {
            ProductOrderFilter = new();
            ProductOrderSort = new();
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
            OnPropertyChanged(nameof(ProductOrderFilter));
            OnPropertyChanged(nameof(ProductOrderSort));
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
        private async void RecivedOrder_Execute()
        {
            if (Orders == null || Orders.SelectedItem == null) return;
            List<ProductModel> products = new(await DBProductOrder.GetProducts(Orders.SelectedItem.Id));
            foreach (ProductModel product in products)
                await DBProduct.Recived(product.Id);

            try
            {
                await DBProductOrder.Recived(Orders.SelectedItem.Id);
            }
            catch (Exception ex)
            {
                await AcceptCall("Stock agregado pero ocurrió una excepcion al marcar el pedido como recibido, consulte al programado.\n\n" + ex.GetBaseException().Message);
                return;
            }
            Orders.SelectedItem.IsRecived = true;
            ShowSnackbarMessage("Stock agregado con éxito");
        }
        private async void PayedOrder_Execute()
        {
            if (Orders == null || Orders.SelectedItem == null) return;

            if(!decimal.Equals(Orders.SelectedItem.ARGPrice, decimal.Zero))
            {
                try
                {
                    CashOperationModel com = new();
                    com.SetCurrency(Currency_Types.ARG);
                    com.SetAmount(Orders.SelectedItem.ARGPrice);
                    com.SetOperation(Cash_Operation.OrderPayment);
                    await DBCashOperation.Insert(com);
                }
                catch (Exception ex)
                {
                    await AcceptCall("Error al realizar la operacion de pago.\n\n" + ex.GetBaseException().Message);
                    return;
                }
            }
            if (!decimal.Equals(Orders.SelectedItem.USDPrice, decimal.Zero))
            {
                try
                {
                    CashOperationModel com = new();
                    com.SetCurrency(Currency_Types.USD);
                    com.SetAmount(Orders.SelectedItem.USDPrice);
                    com.SetOperation(Cash_Operation.OrderPayment);
                    await DBCashOperation.Insert(com);
                }
                catch (Exception ex)
                {
                    await AcceptCall("Error al realizar la operacion de pago.\n\n" + ex.GetBaseException().Message);
                    return;
                }
            }

            try
            {
                await DBProductOrder.Payed(Orders.SelectedItem.Id);
            }
            catch (Exception ex)
            {
                await AcceptCall("Operación pagada pero ocurrio una excepcion al marcarla como tal, consulte al programador\n\n" + ex.GetBaseException().Message);
                return;
            }

            Orders.SelectedItem.IsPayed = true;
            ShowSnackbarMessage("Pago realizado con éxito");
        }
        private bool PayedOrder_Enable()
        {
            if (Orders == null) return false;
            if (Orders.SelectedItem == null) return false;
            return !Orders.SelectedItem.IsPayed;
        }
        private bool RecivedOrder_Enable()
        {
            if (Orders == null) return false;
            if (Orders.SelectedItem == null) return false;
            return !Orders.SelectedItem.IsRecived;
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
        private void FilterExecute()
        {
            if (Orders is null) return;

            Orders.FilterExecute(ProductOrderFilter!.Filter);

            OnPropertyChanged(nameof(Orders));
        }
        private void SortExecute()
            => Orders!.SortExecute(ProductOrderSort!.OrderBy());
    }
}
