using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.DialogModels.SellDialogs;
using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Core.Models.Sells;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class SellWindowModel : BaseViewModel
    {
        private bool isRetailPrice;
        public bool IsNewMode { get; set; }
        public RespIVADialogModel RespIVADialogModel { get; set; }
        public DialogModels.SellDialogs.ClientDialogModel ClientDialogModel { get; set; }
        public SellModel SellModel { get; set; }
        
        public ICommand AddProduct_Command => new RelayCommand(_ => AddProduct_Execute());
        public ICommand AddService_Command => new RelayCommand(_ => AddService_Execute());
        public ICommand Delete_Command => new RelayCommand(Delete_Execute);
        public bool IsRetailPrice
        {
            get => isRetailPrice;
            set
            {
                if (SetProperty(ref isRetailPrice, value))
                    SellModel = new();
            }
        }
        public SellWindowModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
            RespIVADialogModel = new ();
            ClientDialogModel = new();
            SellModel = new();
        }

        public override void Load()
        {
            
        }
        private async void AddProduct_Execute()
        {
            SearchProductDialogModel dialogModel = new();
            ProductModel? product = await dialogModel.GetProduct(DialogIdentifiers.SellWindow_Identifier);
            if (product == null) return;

            SellProductModel sellProduct = new();

            sellProduct.SetCode(product.Code);
            sellProduct.SetDescription(product.Description);
            sellProduct.SetCurrency(product.Brand!.IsDolarValue ? Currency_Types.USD : Currency_Types.ARG);
            sellProduct.SetPrice(isRetailPrice? product.RetailPrice : product.WholesalerPrice);
            sellProduct.SetAmount(product.Multiplier);

            try
            {
                SellModel.InsertProduct(sellProduct);
            }
            catch (Exception ex)
            {
                await AcceptCall(ex.Message, DialogIdentifiers.SellWindow_Identifier);
            }

        }
        private async void AddService_Execute()
        {
            ServiceDialogModel dialogModel = new();
            SellProductModel? service = await dialogModel.GetService(DialogIdentifiers.SellWindow_Identifier);

            if (service is null) return;

            try
            {
                SellModel.InsertProduct(service);
            }
            catch (Exception ex)
            {
                await AcceptCall(ex.Message, DialogIdentifiers.SellWindow_Identifier);
            }

        }
        private void Delete_Execute(object? o)
        {
            if (o is null) return;
            SellModel.RemoveProduct((SellProductModel)o);

            
        }
    }
}
