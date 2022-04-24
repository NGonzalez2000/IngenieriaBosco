using IngenieriaBosco.Core.DBCalls;
using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Generics;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private string? categoryName;
        private string? brandName;
        private string? providerName;

        public string? CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                CategoryList!.FilterExecute(x => (x is CategoryModel c && c.Name!.Contains(categoryName!)) || categoryName == string.Empty);
            }
        }
        public string? BrandName
        {
            get { return brandName; }
            set
            {
                brandName = value;
                BrandList!.FilterExecute(x => (x is BrandModel b && b.Name!.Contains(brandName!)) || brandName == string.Empty);
            }
        }
        public string? ProviderName
        {
            get { return providerName; }
            set
            {
                providerName = value;
                ProviderList!.FilterExecute(x => (x is ProviderModel p && p.Name!.Contains(providerName!)) || providerName == string.Empty);
            }
        }

        public GridListModel<CategoryModel>? CategoryList { get; set; }
        public GridListModel<BrandModel>? BrandList { get; set;}
        public GridListModel<ProviderModel>? ProviderList { get; set; }

        public ICommand NewCategoryCommand => new RelayCommand(_ => NewCategory_Execute());
        public ICommand EditCategoryCommand => new RelayCommand(_ => EditCategory_Execute());
        public ICommand DeleteCategoryCommand => new RelayCommand(_ => DeleteCategory_Execute());
        public ICommand NewBrandCommand => new RelayCommand(_ => NewBrand_Execute());
        public ICommand EditBrandCommand => new RelayCommand(_ => EditBrand_Execute());
        public ICommand DeleteBrandCommand => new RelayCommand(_ => DeleteBrand_Execute());
        public CategoryViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {

        }
        public async override void Load()
        {
            CategoryList = new(await DBCategory.SelectAll());
            BrandList = new();
            ProviderList = new();

            OnPropertyChanged(nameof(CategoryList));
            OnPropertyChanged(nameof(BrandList));
            OnPropertyChanged(nameof(ProviderList));

            categoryName = string.Empty;
            brandName = string.Empty;
            providerName = string.Empty;

            
        }
        private async void NewCategory_Execute()
        {
            CategoryDialogModel dialogModel = new();
            CategoryModel? categoryModel = await dialogModel.NewCategory();

            if (categoryModel == null) return;

            try
            {
                await DBCategory.Insert(categoryModel);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al insertar la Categoría\n."
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                return;
            }

            try
            {
                categoryModel.Id = await DBAccess.GetId("Categories");
            }
            catch (System.Exception ex)
            {
                await AcceptCall("No se pudo cargar la Identidad de la categoría, la pagina se actualizará.\n"
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                Load();
            }

            CategoryList!.Insert(categoryModel);


            ShowSnackbarMessage($"Categoría agregada con éxito");
        }
        private async void EditCategory_Execute()
        {
            if (CategoryList!.SelectedItem == null) return;
            CategoryDialogModel dialogModel = new(CategoryList.SelectedItem);
            CategoryModel? categoryModel = await dialogModel.EditCategory();

            if (categoryModel == null) return;

            try
            {
                await DBCategory.Update(categoryModel);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al editar la Categoría\n."
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                return;
            }
            
            CategoryList.Edit(CategoryList.SelectedItem, categoryModel);
            ShowSnackbarMessage("Categoría editada con éxito");
        }
        private async void DeleteCategory_Execute()
        {
            if(CategoryList == null || CategoryList.SelectedItem == null) return;
            bool response = await AcceptCancelCall($"Seguro que desea eliminar a la categoría {CategoryList.SelectedItem.Name}.\n" +
                                                   $"Todas las marcas y productos asociadas a dicha categoría tambien se eliminaran.",
                                                   DialogIdentifiers.Category_Identifier);
            if (!response) return;

            try
            {
                await DBCategory.Delete(CategoryList.SelectedItem);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al eliminar la Categoría\n."
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                return;
            }
            
            CategoryList.Delete(CategoryList.SelectedItem);
            ShowSnackbarMessage("Categoría eliminada con éxito");
        }
        private async void NewBrand_Execute()
        {

        }
        private async void EditBrand_Execute()
        {

        }
        private async void DeleteBrand_Execute()
        { 
        }
    }
}
