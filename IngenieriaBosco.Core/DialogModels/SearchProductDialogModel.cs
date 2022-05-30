using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class SearchProductDialogModel : BaseDialogModel
    {
        private ProviderModel? provider;
        private CategoryModel? selectedCategory;
        private BrandModel? selectedBrand;
        public CategoryModel? SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if(SetProperty(ref selectedCategory, value))
                {
                    SetBrands();
                }
            }
        }
        public BrandModel? SelectedBrand
        {
            get => selectedBrand;
            set
            {
                if(SetProperty(ref selectedBrand, value))
                {
                    LoadProducts();
                }
            }
        }
        public List<CategoryModel>? Categories { get; set; }
        public List<BrandModel>? Brands { get; set; }
        public ObservableCollection<ProductModel>? Products { get; set; }
        public ProductModel? SelectedProduct { get; set; }
        public int Amount { get; set; }

        public void SetProvider(ProviderModel prov)
            =>  provider = prov;

        public async Task<ProductModel?> GetProduct()
        {
            await SetCategories();
            SearchProductDialog dialog = new()
            {
                DataContext = this
            };
            bool response = await DialogHosting(dialog, DialogIdentifiers.ProductOrderWindow_Identifier,closingEventHandler: ClosingEventHandler_New);
            if (!response || SelectedProduct == null) return null;

            SelectedProduct.Category = SelectedCategory;
            SelectedProduct.Brand = SelectedBrand;
            SelectedProduct.Multiplier = Amount;
            return SelectedProduct;
        }

        private async Task SetCategories()
        {
            if(provider is null)
            {
                Categories = new(await DBCategory.SelectAll());
                return;
            }
            Categories = new(await DBCategory.SelectByProvider(provider));

        }
        private async void SetBrands()
        {
            if (SelectedCategory == null) return;
            if(provider is null)
            {
                Brands = new(await DBBrand.SelectByCategoryId(SelectedCategory));
                OnPropertyChanged(nameof(Brands));

                return;
            }
            Brands = new(await DBBrand.SelectByCategory_Provider(SelectedCategory.Id, provider.Id));
            OnPropertyChanged(nameof(Brands));
        }
        private async void LoadProducts()
        {
            if(SelectedCategory is null || SelectedBrand is null) return;
            Products = new(await DBProduct.SelectByBrand(SelectedBrand));
            OnPropertyChanged(nameof(Products));
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public async override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;
            if (Amount != 0) return;
            eventArgs.Cancel();

            AmountDialog dialog = new()
            {
                DataContext = this
            };

            bool response = await DialogHosting(dialog, DialogIdentifiers.SearchProduct_Identifier);


            if (response)
            {
                eventArgs.Session.Close(true);
            }
        }
    }
}
