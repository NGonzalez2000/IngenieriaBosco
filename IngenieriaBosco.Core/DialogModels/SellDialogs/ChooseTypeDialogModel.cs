using IngenieriaBosco.Front.Dialogs.Sell;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels.SellDialogs
{
    internal class ChooseTypeDialogModel : BaseDialogModel
    {
        public async Task<bool> SelectType()
        {
            ChooseTypeDialog dialog = new();
            return await DialogHosting(dialog, DialogIdentifiers.SellWindow_Identifier);
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
