using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class SearchProductDialogModel : BaseDialogModel
    {
        private ProviderModel? provider;
        private CategoryModel? selectedCategory;
        private string? code;
        private string? description;
        private BrandModel? selectedBrand;
        private ICollectionView? collectionView;
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
                    if(provider!= null)LoadProductsByBrand();
                    else
                    {
                        UpdateFilter();
                    }
                }
            }
        }
        public string? Code
        {
            get => code;
            set
            {
                if(SetProperty(ref code,value) && collectionView != null)
                    collectionView.Refresh();
            }
        }
        public string? Description
        {
            get => description;
            set
            {
                if (SetProperty(ref description, value) && collectionView != null)
                    collectionView.Refresh();
            }
        }
        

        public List<CategoryModel>? Categories { get; set; }
        public List<BrandModel>? Brands { get; set; }
        public ObservableCollection<ProductModel>? Products { get; set; }
        public ProductModel? SelectedProduct { get; set; }
        public int Amount { get; set; }

        public void SetProvider(ProviderModel prov)
            =>  provider = prov;

        public async Task<ProductModel?> GetProduct(string dialogIdentifier)
        {
            await SetCategories();
            SearchProductDialog dialog = new()
            {
                DataContext = this
            };
            bool response = await DialogHosting(dialog, dialogIdentifier,closingEventHandler: ClosingEventHandler_New);
            if (!response || SelectedProduct == null) return null;

            if(SelectedCategory != null)SelectedProduct.Category = SelectedCategory;
            if (SelectedBrand != null) SelectedProduct.Brand = SelectedBrand;
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
                Products = new(await DBProduct.SelectByCategory(SelectedCategory));
                foreach (ProductModel product in Products)
                    product.Brand = (await DBProduct.SelectBrand(product)).FirstOrDefault(defaultValue:null);
                OnPropertyChanged(nameof(Brands));
                OnPropertyChanged(nameof(Products));
                UpdateFilter();

                return;
            }
            Brands = new(await DBBrand.SelectByCategory_Provider(SelectedCategory.Id, provider.Id));
            OnPropertyChanged(nameof(Brands));
        }
        private async void LoadProductsByBrand()
        {
            if(SelectedCategory is null || SelectedBrand is null) return;
            Products = new(await DBProduct.SelectByBrand(SelectedBrand));
            OnPropertyChanged(nameof(Products));
            UpdateFilter();

        }

        private void UpdateFilter()
        {
            collectionView = CollectionViewSource.GetDefaultView(Products);
            collectionView.Filter = Filter;
            collectionView.Refresh();
        }
        private bool Filter(object o)
        {
            if (o is null) return false;
            ProductModel product = (ProductModel)o;
            return CheckBrand(product) &&
                    Validate(Code, product.Code) &&
                    Validate(Description, product.Description);
        }
        private bool CheckBrand(ProductModel p)
        {
            if (provider != null ) return SelectedBrand != null;
            if (SelectedBrand == null) return true;
            return p.Brand!.Id == SelectedBrand.Id;
        }
        private bool Validate(string? fst, string scd)
        {
            if(string.IsNullOrEmpty(fst)) return true;
            return scd.Contains(fst);
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
