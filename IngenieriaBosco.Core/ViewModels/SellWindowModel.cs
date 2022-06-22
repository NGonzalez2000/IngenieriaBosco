using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.DialogModels.SellDialogs;
using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Core.Models.Sells;
using IngenieriaBosco.Front.Dialogs.Sell;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class SellWindowModel : BaseViewModel
    {
        private bool isRetailPrice;
        public bool IsNewMode { get; set; }
        public bool IsEditable { get; set; }
        public RespIVADialogModel RespIVADialogModel { get; set; }
        public DialogModels.SellDialogs.ClientDialogModel ClientDialogModel { get; set; }
        public SellModel SellModel { get; set; }
        
        public ICommand AddProduct_Command => new RelayCommand(_ => AddProduct_Execute(), _ => !SellModel.IsPayed);
        public ICommand AddService_Command => new RelayCommand(_ => AddService_Execute(), _ => !SellModel.IsPayed);
        public ICommand IsPayed_Command => new RelayCommand(_ => IsPayed_Execute(), _ => !SellModel.IsPayed);
        public ICommand End_Command => new RelayCommand(End_Execute, _ => IsEditable);
        public ICommand Edit_Command => new RelayCommand(Edit_Execute, _ => !SellModel.IsPayed);
        public ICommand Delete_Command => new RelayCommand(Delete_Execute, _ => !SellModel.IsPayed);
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
            IsEditable = true;
        }

        public override void Load()
        {
            
        }
        public void SetSell(SellModel sellModel)
        {
            SellModel = sellModel;
            if (sellModel.RespIVA == null) return;
            RespIVADialogModel.SetResp(sellModel.RespIVA.Descripcion);
            ClientDialogModel.SetClient(sellModel.FirstName, sellModel.LastName, sellModel.CUIL);
            SellModel.LoadProducts();
            IsEditable = !SellModel.IsPayed;
            OnPropertyChanged(nameof(IsEditable));

        }
        private async void IsPayed_Execute()
        {
            bool response = await AcceptCancelCall("Se marcará la venta como pagada.\n\n Esto no se podra cambiar",DialogIdentifiers.SellWindow_Identifier);
            if(response)SellModel.IsPayed = true;
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
        private async void Edit_Execute(object? o)
        {
            if (o is null) return;
            ChooseTypeDialogModel typeDialogModel = new();
            SellProductModel temp = (SellProductModel)o;
            SellProductModel? spm;
            if(!await typeDialogModel.SelectType())
            {
                ServiceDialogModel dialogModel = new();
                dialogModel.Service = new(temp);
                dialogModel.SelectedCurrency = temp.Currency!;

                spm = await dialogModel.GetService(DialogIdentifiers.SellWindow_Identifier);
            }
            else
            {
                DialogModels.SellDialogs.ProductDialogModel productDialogModel = new(temp);
                spm = await productDialogModel.GetService(DialogIdentifiers.SellWindow_Identifier);
            }
            if(spm is null) return;
            SellModel.RemoveProduct((SellProductModel)o);
            SellModel.InsertProduct(spm);
        }
        private async void End_Execute(object? o)
        {
            if (o is null) return;
            if (SellModel.IsPayed)
            {
                CashOperationModel com = new();
                com.SetCurrency(SellModel.Currency);
                com.SetOperation(Cash_Operation.SellPayment);
                com.SetAmount(SellModel.TotalPrice);
                try
                {
                    await DBCashOperation.Insert(com);
                } 
                catch (Exception ex)
                {
                    await AcceptCall("Error al registrar el pago.\n\n" + ex.GetBaseException().Message);
                }
                
            }
            if (IsNewMode)
            {
                ClientModel? temp = ClientDialogModel.GetClient();
                if(temp is null)
                {
                    await AcceptCall("Debe seleccionar un cliente.", DialogIdentifiers.SellWindow_Identifier);
                    return;
                }
                SellModel.FirstName = temp.FirstName;
                SellModel.LastName = temp.LastName;
                SellModel.CUIL = temp.CUIL;

                SellModel.RespIVA = RespIVADialogModel.SelectedRespIVA;
                try
                {
                    await DBSell.Insert(SellModel);
                    SellModel.Id = await DBAccess.GetId("Sells");
                }
                catch (Exception ex)
                {
                    await AcceptCall("Error al guardar la venta en la Base de datos.\n\n" + ex.GetBaseException().Message,
                        DialogIdentifiers.SellWindow_Identifier);
                    return;
                }

                try
                {
                    foreach (SellProductModel spm in SellModel.ProductList!)
                    {
                        await DBSellItem.Insert(spm, SellModel.Id);
                        spm.Id = await DBAccess.GetId("SellItems");
                    }
                }
                catch (Exception ex)
                {
                    await AcceptCall("Error al guardar los products de la venta en la Base de datos.\n\n" + ex.GetBaseException().Message,
                        DialogIdentifiers.SellWindow_Identifier);
                    return;
                }

                await AcceptCall("Venta generada con éxito.", DialogIdentifiers.SellWindow_Identifier);

            }
            else
            {
                try
                {
                    await DBSell.Update(SellModel);
                }
                catch (Exception ex)
                {
                    await AcceptCall("Error al actualizar la venta.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.SellWindow_Identifier);
                    return;
                }
                try
                {
                    foreach(SellProductModel spm in SellModel.ProductList!)
                    {
                        if (spm.Id == 0)
                        {
                            await DBSellItem.Insert(spm, SellModel.Id);
                            spm.Id = await DBAccess.GetId("SellItems");
                        }
                        else
                        {
                            await DBSellItem.Update(spm);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await AcceptCall("Error al actualizar los productos de la venta.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.SellWindow_Identifier);
                    return;
                }
                await AcceptCall("Venta editada con éxito.", DialogIdentifiers.SellWindow_Identifier);
            }
            
            
            ((Window)o).Close();
        }
        private async void Delete_Execute(object? o)
        {
            if (o is null) return;
            SellProductModel tmp = (SellProductModel)o;
            try
            {
                if(tmp.Id != 0)
                    await DBSellItem.Delete(tmp);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al quitar el product.\n\n" + ex.GetBaseException().Message, DialogIdentifiers.SellWindow_Identifier);
                return;
            }
            SellModel.RemoveProduct((SellProductModel)o); 
        }
    }
}
