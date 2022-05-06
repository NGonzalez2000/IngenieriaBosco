using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Filters;
using IngenieriaBosco.Core.Models.Generics;
using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ICommand NewProductCommand => new RelayCommand(_ => NewProductExecute());
        public GridListModel<ProductModel>? ProductList { get; set; }
        public ProductFilterModel? ProductFilter { get; set; } 
        public ProductSortModel? ProductSort { get; set; }
        public ICommand FilterCommand => new RelayCommand(_ => FilterExecute());
        public ICommand SortCommand => new RelayCommand(_ => SortExecute());
        

        public ProductViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {

        }
        public override void Load()
        {
            ProductList = new();
            ProductFilter = new ();
            ProductSort = new();
        }
        private async void NewProductExecute()
        {
            ProductDialogModel dialogModel = new();
            ProductModel? product = await dialogModel.NewProduct();
        }
        private async void FilterExecute()
        {
            if (ProductList is null || ProductFilter!.SelectedCategory is null) return;


            ProductList.Collection = new(await DBProduct.SelectByCategory(ProductFilter.SelectedCategory));


            foreach(ProductModel product in ProductList.Collection)
            {
                product.Category = ProductFilter.SelectedCategory;
                product.Brand = (await DBProduct.SelectBrand(product)).First();
            }
            ProductList.FilterExecute(ProductFilter!.Filter);
        }
        private void SortExecute()
            => ProductList!.SortExecute(ProductSort!.OrderBy());
    }
}
