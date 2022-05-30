using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Core.Models.Sells;
using IngenieriaBosco.Front.Dialogs.Sell;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels.SellDialogs
{
    internal class ServiceDialogModel : BaseDialogModel
    {
        public List<string> Currencies { get; set; } 
        public string SelectedCurrency { get; set; }
        public SellProductModel Service { get; set; }
        public ServiceDialogModel()
        {
            Service = new();
            Currencies = new List<string> { Currency_Types.ARG, Currency_Types.USD };
            SelectedCurrency = Currencies[0];
        }

        public async Task<SellProductModel?> GetService(string dialogIdentifier)
        {
            ServiceDialog dialog = new()
            {
                DataContext = this
            };
            bool response = await DialogHosting(dialog, dialogIdentifier, closingEventHandler: ClosingEventHandler_New);
            if (!response) return null;

            Service.SetCurrency(SelectedCurrency);
            Service.SetAmount(1);

            return Service;
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool o
                && o == false) return;
            if (string.IsNullOrEmpty(Service[nameof(SellProductModel.Code)]) ||
                string.IsNullOrEmpty(Service[nameof(SellProductModel.Description)])) return;

            eventArgs.Cancel();
            OnPropertyChanged(nameof(Service));
        }
    }
}
