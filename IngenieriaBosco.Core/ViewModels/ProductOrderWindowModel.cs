using IngenieriaBosco.Core.DialogModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class ProductOrderWindowModel : BaseViewModel
    {
        public ProductOrderModel? ProductOrder { get; set; }
        private bool isNewMode;
        public ProductOrderWindowModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
            isNewMode = true;
        }
        public void SetProductOrder(ProductOrderModel pom)
        {
            ProductOrder = pom;
            isNewMode = false;
        }
        public async void SetProducts()
        {
            if (ProductOrder == null || isNewMode) return;
            try
            {
                ProductOrder.Products = new(await DBProductOrder.GetProducts(ProductOrder.Id));
            }
            catch(Exception ex)
            {
                await AcceptCall("Error al cargar los productos.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.ProductOrderWindow_Identifier);
            }
        }

        public async override void Load()
        {
           if(ProductOrder == null || isNewMode)
            {
                ProductOrder = new();
                try
                {
                    ProductOrder.Id = (await DBAccess.GetId("ProductOrders")) + 1;
                    OnPropertyChanged(nameof(ProductOrder));
                }
                catch (Exception ex)
                {
                    await AcceptCall("No se pudo cargar el número de pedido.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.ProductOrderWindow_Identifier);
                }
            }
        }

        public ICommand AddProduct_Command => new RelayCommand(_ => AddProduct_Execute());
        public ICommand RemoveProduct_Command => new RelayCommand(RemoveProduct_Execute);
        public ICommand GenerateOrder_Command => new RelayCommand(GenerateOrder_Execute, _ => ProductOrder != null && ProductOrder.Products != null && ProductOrder.Products.Count > 0);
        private async void AddProduct_Execute()
        {
            SearchProductDialogModel dialogModel = new();
            dialogModel.SetProvider(ProductOrder!.Provider!);
            ProductModel? product = await dialogModel.GetProduct(DialogIdentifiers.ProductOrderWindow_Identifier);
            if (product != null)
            {
                ProductOrder.InsertProduct(product);
                OnPropertyChanged(nameof(ProductOrder));
            }
        }
        private void RemoveProduct_Execute(object? param)
        {
            if (param == null || ProductOrder == null) return;
            ProductOrder.RemoveProduct((ProductModel)param);
            OnPropertyChanged(nameof(ProductOrder));
        }
        private async void GenerateOrder_Execute(object? param)
        {
            if (ProductOrder is null || ProductOrder.Provider is null || ProductOrder.Products is null) return;
            await DBProductOrder.Insert(ProductOrder);
            foreach (ProductModel product in ProductOrder.Products)
                await DBProductOrder.InsertProduct(product, ProductOrder.Id);
            if (param != null && param is Window w) w.Close();
            ShowSnackbarMessage("Pedido generado con éxito");
        }
        
    }
}
