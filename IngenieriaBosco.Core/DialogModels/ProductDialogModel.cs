using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class ProductDialogModel : BaseDialogModel
    {
        public ProductModel Product { get; set; }
        public ObservableCollection<CategoryModel>? Categories { get; set; }
        public ObservableCollection<BrandModel>? Brands { get; set; }

        

        public ProductDialogModel(ProductModel? product = null)
        {
            Product = new();
            if (product != null) Product = (ProductModel)product.ShallowCopy();

            SetCategories();
        }

        public async Task<ProductModel?> NewProduct()
        {
            ProductDialog dialog = new("NUEVO PRODUCTO",ComboBox_SelectionChanged)
            {
                DataContext = this
            };

            bool response = await DialogHosting(dialog, DialogIdentifiers.Product_Identifier, closingEventHandler: ClosingEventHandler_New);

            if (!response) return null;

            return Product;
        }
        
        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Product[nameof(Product.Code)] == string.Empty &&
                Product[nameof(Product.Description)] == string.Empty &&
                Product[nameof(Product.Category)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Product));
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;

            if (Product[nameof(Product.Code)] == string.Empty &&
                Product[nameof(Product.Description)] == string.Empty &&
                Product[nameof(Product.Category)] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Product));
        }


        private async void SetCategories()
            => Categories = new(await DBCategory.SelectAll());
        public async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Product.Category != null)
                Brands = new(await DBBrand.SelectByCategoryId(Product.Category));

            else
                Brands = new();

            OnPropertyChanged(nameof(Brands));
        }
        
    }
}
