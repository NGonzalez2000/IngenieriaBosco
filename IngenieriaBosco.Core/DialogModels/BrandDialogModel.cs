using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class BrandDialogModel : BaseDialogModel
    {
        public BrandModel Brand { get; set; }
        public BrandDialogModel(BrandModel? brand = null)
        {
            Brand = new();
            if (brand != null) Brand = (BrandModel)brand.ShallowCopy();
        }
        public async Task<BrandModel?> NewBrand()
        {
            BrandDialog dialog = new("Nueva Marca")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Category_Identifier, closingEventHandler: ClosingEventHandler_New);
            if (result) return Brand;
            return null;
        }
        public async Task<BrandModel?> EditBrand()
        {
            BrandDialog dialog = new("Editar Marca")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Category_Identifier, closingEventHandler: ClosingEventHandler_Edit);
            if (result) return Brand;
            return null;
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if(Brand["Name"] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Brand));
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Brand["Name"] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Brand));
        }

    }
}
