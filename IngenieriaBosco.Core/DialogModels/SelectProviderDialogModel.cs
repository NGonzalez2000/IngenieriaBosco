using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class SelectProviderDialogModel : BaseDialogModel
    {
        public List<ProviderModel> Providers { get; set; }
        public ProviderModel? SelectedProvider { get; set; }

        public SelectProviderDialogModel(IEnumerable<ProviderModel> providers)
        {
            Providers = new(providers);
        }

        public async Task<ProviderModel?> GetProvider(string dialogIdentifier)
        {
            SelectProviderDialog selectDialog = new()
            {
                DataContext = this
            };
            bool response = await DialogHosting(selectDialog, dialogIdentifier, closingEventHandler: ClosingEventHandler_New);
            if (response) return SelectedProvider;
            return null;
        }

        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (SelectedProvider != null) return;

            eventArgs.Cancel();

        }
    }
}
