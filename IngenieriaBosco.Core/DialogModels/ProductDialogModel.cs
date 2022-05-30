using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class ProductDialogModel : BaseDialogModel, IDataErrorInfo
    {
        public ProductModel Product { get; set; }
        private int selectedCategory;
        public int SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if(SetProperty(ref selectedCategory, value))
                {
                    Product.Category = value == -1 ? null : Categories![selectedCategory];
                    SetBrands();
                }
            }
        }

        private int selectedBrand;

        public int SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (SetProperty(ref selectedBrand, value))
                    Product.Brand = value == -1 ? null : Brands![selectedBrand];
            }
        }

        public ObservableCollection<CategoryModel>? Categories { get; set; }
        public ObservableCollection<BrandModel>? Brands { get; set; }

        

        public ProductDialogModel(ProductModel? product = null)
        {
            Product = new();

            if (product != null) Product = (ProductModel)product.ShallowCopy();
            selectedCategory = -1;
            selectedBrand = -1;
        }

        public async Task<ProductModel?> NewProduct()
        {

            await SetCategories();

            ProductDialog dialog = new("NUEVO PRODUCTO")
            {
                DataContext = this
            };

            bool response = await DialogHosting(dialog, DialogIdentifiers.Product_Identifier, closingEventHandler: ClosingEventHandler_New);

            if (!response) return null;

            if(Product.Brand is null) Product.Brand = new BrandModel() { Id = 1 };

            return Product;
        }
        public async Task<ProductModel?> EditProduct(string dialogName = DialogIdentifiers.Product_Identifier)
        {

            await SetCategories();

            if(Product.Category != null)
                selectedCategory = Categories!.IndexOf(Categories.FirstOrDefault(x => x.Id == Product.Category.Id, defaultValue:new ()));

            if (selectedCategory != -1)await SetBrands();
            if (Product.Brand != null && Product.Brand.Id != 1)
                selectedBrand = Brands!.IndexOf(Brands.FirstOrDefault(x => x.Id == Product.Brand.Id,defaultValue: new()));


            ProductDialog dialog = new("EDITAR PRODUCTO")
            {
                DataContext = this
            };


            bool response = await DialogHosting(dialog, dialogName, closingEventHandler: ClosingEventHandler_Edit);

            if (!response) return null;

            if (Product.Brand is null) Product.Brand = new BrandModel() { Id = 1 };

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
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(SelectedCategory)) return Product[nameof(ProductModel.Category)];
                return string.Empty;
            }
        }
        private async Task SetBrands()
        {
            try
            {
                Brands = selectedCategory is -1 ? new() : new(await DBBrand.SelectByCategoryId(Categories![selectedCategory]));
            }
            catch (Exception)
            {
                MessageBox.Show("No se han podido cargar las marcas");
                Brands = new();
            }
            OnPropertyChanged(nameof(Brands));
        }
        private async Task SetCategories()
        {
            try
            {
                Categories = new(await DBCategory.SelectAll());
            }
            catch (Exception)
            {
                MessageBox.Show("No se han podido cargar las categorías");
                Categories = new();
            }
            OnPropertyChanged(nameof(Categories));

        }
            
        
        
    }
}
