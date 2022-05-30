using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels
{
    public class CurrencyDialogModel : BaseDialogModel
    {
        public int SelectedIndex { get; set; }
        public CashOperationModel CashOperation { get; set; }
        public List<string> Operations { get; set; }
        public CurrencyDialogModel(string Currency)
        {
            Operations = new () { "DEPOSITAR", "RETIRAR" };
            CashOperation = new() { Currency = Currency };
            SelectedIndex = 0;
        }
        
        public async Task<CashOperationModel?> GetCashOperation()
        {
            CurrencyDialog dialog = new()
            {
                DataContext = this
            };

            bool response = await DialogHosting(dialog, DialogIdentifiers.CashRegister_Identifier,closingEventHandler:ClosingEventHandler_New);

            if (!response) return null;

            if(SelectedIndex == 1)
                CashOperation.Amount = decimal.Negate(CashOperation.Amount);

            CashOperation.Operation = SelectedIndex == 0 ? Cash_Operation.Deposit : Cash_Operation.Removal;

            CashOperation.Date = DateOnly.FromDateTime(DateTime.Now).ToShortDateString();

            return CashOperation;
        }

        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (CashOperation[nameof(CashOperation.Amount)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(CashOperation.Amount));
        }
    }
}
