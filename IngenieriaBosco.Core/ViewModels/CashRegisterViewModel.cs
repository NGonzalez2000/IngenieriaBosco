using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Core.Models.Singletons;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    public class CashRegisterViewModel : BaseViewModel
    {
        public CashRegisterViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
        }

        public override async void Load()
        {
            CashOperations = new( await DBCashOperation.SelectTop(100));
            CashRegister = new();
            CashRegister.ARG = (await DBCurrency.Select(Currency_Types.ARG)).First();
            CashRegister.USD = (await DBCurrency.Select(Currency_Types.USD)).First();

            OnPropertyChanged(nameof(CashOperations));
            OnPropertyChanged(nameof(CashRegister));
        }
        public ICommand Modify_Command => new RelayCommand(Modify_Execute);
        public int Quantity { get; set; }
        public CashRegisterModel? CashRegister { get; set; }
        public ObservableCollection<CashOperationModel>? CashOperations { get; set; }
        private async void Modify_Execute(object? param)
        {
            if (param is null) return;
            string currency = (string)param;

            CurrencyDialogModel dialogModel = new(currency);
            CashOperationModel? operation = await dialogModel.GetCashOperation();

            if (operation is null) return;

            try
            {
                await DBCashOperation.Insert(operation);
            }
            catch (Exception ex)
            {
                await AcceptCall($"Error al modificar {currency}\n\n" + ex.GetBaseException().Message,
                    DialogIdentifiers.CashRegister_Identifier);
                return;
            }

            bool isRemoval = decimal.Compare(operation.Amount, 0m) < 0;
            string op =  isRemoval ? "retirado" : "depositado";

            if (currency == Currency_Types.ARG)
                CashRegister!.ARG += operation.Amount;
            else
                CashRegister!.USD += operation.Amount;
            
            OnPropertyChanged(nameof(CashRegister));
            CashOperations!.Insert(0, operation);

            OnPropertyChanged(nameof(CashOperations));
            ShowSnackbarMessage($"Se han {op} $ {Math.Abs(operation.Amount)} con éxito");
        }

    }
}
