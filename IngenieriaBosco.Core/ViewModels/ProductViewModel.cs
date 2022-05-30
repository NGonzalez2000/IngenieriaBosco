using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Filters;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Front.Windows;
using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ICommand ImportProductCommand => new RelayCommand(_ => ImportProductExecute());
        public ICommand NewProductCommand => new RelayCommand(_ => NewProductExecute());
        public ICommand EditProductCommand => new RelayCommand(_ => EditProductExecute());
        public ICommand DeleteProductCommand => new RelayCommand(_ => DeleteProductExecute());
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
            OnPropertyChanged(nameof(ProductList));
            OnPropertyChanged(nameof(ProductFilter));
            OnPropertyChanged(nameof(ProductSort));
        }
        
        public void ImportProductExecute()
        {
            ExcelWindow excelWindow = new()
            {
                DataContext = new ExcelViewModel(snackbarMessageQueue!)
            };
            excelWindow.Show();
        }
        private async void NewProductExecute()
        {
            ProductDialogModel dialogModel = new();
            ProductModel? product = await dialogModel.NewProduct();

            if (product is null) return;

            try
            {
                await DBProduct.Insert(product);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al crear el producto\n\n" + ex.GetBaseException().Message,
                                    DialogIdentifiers.Product_Identifier);
                return;
            }

            try
            {
                product.Id = await DBAccess.GetId("Products");
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al cargar el identificador del producto\n\nLa página se actualizará" + ex.GetBaseException().Message,
                                    DialogIdentifiers.Product_Identifier);
            }
            if(ProductFilter!.Filter(product))
                ProductList!.Insert(product);

            ShowSnackbarMessage("Producto creado con éxito");
        }
        private async void EditProductExecute()
        {
            if (ProductList!.SelectedItem is null) return;

            ProductDialogModel dialogModel = new(ProductList.SelectedItem);
            ProductModel? product = await dialogModel.EditProduct();

            if(product is null) return;

            try
            {
                await DBProduct.Update(product);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al actualizar el producto\n\n" + ex.GetBaseException().Message,
                                    DialogIdentifiers.Product_Identifier);
                return;
            }

            ProductList.Edit(ProductList.SelectedItem, product);

            ShowSnackbarMessage("Producto editado con éxito");
        }
        private async void DeleteProductExecute()
        {
            if (ProductList!.SelectedItem is null) return;

            bool respone = await AcceptCancelCall($"Seguro que desea eliminar al producto {ProductList.SelectedItem.Code}?",
                DialogIdentifiers.Product_Identifier);

            if(!respone) return;

            try
            {
                await DBProduct.Delete(ProductList.SelectedItem);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al eliminar el producto seleccionado\n\n" + ex.GetBaseException().Message,
                    DialogIdentifiers.Product_Identifier);
            }

            ProductList.Delete(ProductList.SelectedItem);

            ShowSnackbarMessage("Pruducto eliminado con éxito");
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

            OnPropertyChanged(nameof(ProductList));
        }
        private void SortExecute()
            => ProductList!.SortExecute(ProductSort!.OrderBy());
    }
}
