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
        private List<ProviderModel>? providers;

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
        public ICommand NewProviderCommand => new RelayCommand(_ => NewProvider_Execute());
        public ICommand DeleteProviderCommand => new RelayCommand(_ => DeleteProvider_Execute());
        public CategoryViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {

        }
        public async override void Load()
        {
            providers = new(await DBProvider.SelectAll());

            CategoryList = new(await DBCategory.SelectAll());
            BrandList = new();
            ProviderList = new();

            CategoryList.OnSelectionChanged = OnCategorySelectionChanged;
            BrandList.OnSelectionChanged = OnBrandSelectionChanged;

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
                await AcceptCall("Error al insertar la Categoría\n\n."
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
                await AcceptCall("No se pudo cargar la Identidad de la categoría, la pagina se actualizará.\n\n"
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                Load();
            }

            CategoryList!.Insert(categoryModel);


            ShowSnackbarMessage("Categoría agregada con éxito");
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
                await AcceptCall("Error al editar la Categoría\n\n."
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
            if (CategoryList!.SelectedItem == null) return;
            BrandDialogModel dialogModel = new();
            BrandModel? brandModel = await dialogModel.NewBrand();

            if (brandModel == null) return;

            //insert brand
            try
            {
                await DBBrand.Insert(brandModel,CategoryList!.SelectedItem!.Id);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al insertar la Marca\n\n."
                                 + ex.GetBaseException().Message,
                                 DialogIdentifiers.Category_Identifier);
                return;
            }

            //get id
            try
            {
                brandModel.Id = await DBAccess.GetId("Brands");
            }
            catch (System.Exception ex)
            {
                await AcceptCall("No se pudo cargar la Identidad de la marca, la pagina se actualizará.\n\n"
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                Load();
            }
            BrandList!.Insert(brandModel);
            ShowSnackbarMessage("Marca agregada con éxito");
        }
        private async void EditBrand_Execute()
        {
            //generar script de limpieza de base de datos
            if(BrandList!.SelectedItem == null) return;

            BrandDialogModel dialogModel = new(BrandList.SelectedItem);
            BrandModel? brandModel = await dialogModel.EditBrand();

            if(brandModel == null) return;

            try
            {
                await DBBrand.Update(brandModel);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al editar la Marca\n\n."
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                return;
            }

            BrandList.Edit(BrandList.SelectedItem, brandModel);
            ShowSnackbarMessage("Marca editada con éxito");

        }
        private async void DeleteBrand_Execute()
        {
            if (BrandList == null || BrandList.SelectedItem == null) return;
            bool response = await AcceptCancelCall($"Seguro que desea eliminar a la marca {BrandList.SelectedItem.Name}.\n\n",
                                                   DialogIdentifiers.Category_Identifier);
            if (!response) return;

            try
            {
                await DBBrand.Delete(BrandList.SelectedItem);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al eliminar la Marca\n."
                                  + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                return;
            }

            BrandList.Delete(BrandList.SelectedItem);
            ShowSnackbarMessage("Marca eliminada con éxito");
        }

        private async void NewProvider_Execute()
        {
            if (BrandList!.SelectedItem is null) return;

            List<ProviderModel> _providers = new (providers!);

            foreach(ProviderModel provider in ProviderList!.Collection)
            {
                int indx = _providers.FindIndex(p => p.Id == provider.Id);
                _providers.RemoveAt(indx);
            }
                //_providers.Remove(provider);

            SelectProviderDialogModel selectProviderDialogModel = new(_providers);
            
            ProviderModel? providerModel = await selectProviderDialogModel.GetProvider(DialogIdentifiers.Category_Identifier);

            if (providerModel is null) return;

            try
            {
                await DBBrandProviders.Insert(BrandList!.SelectedItem, providerModel);
            }
            catch (System.Exception ex)
            {
                await AcceptCall($"Error al agregar el proveedor a la marca {BrandList.SelectedItem.Name}\n\n" +
                                   ex.GetBaseException().Message,
                                   DialogIdentifiers.Category_Identifier);
                return;
            }

            ProviderList.Insert(providerModel);

            ShowSnackbarMessage($"El proveedor de {BrandList.SelectedItem.Name} se ha agregado con éxito");
        }       
        private async void DeleteProvider_Execute()
        {
            if(ProviderList!.SelectedItem is null || BrandList!.SelectedItem is null) return;

            bool respone = await AcceptCancelCall($"Seguro que desea sacarle el proveedor {ProviderList.SelectedItem.Name} a la marca {BrandList.SelectedItem.Name}?",
                                                    DialogIdentifiers.Category_Identifier);

            if (!respone) return;

            try
            {
                await DBBrandProviders.Delete_Provider(BrandList.SelectedItem, ProviderList.SelectedItem);
            }
            catch (System.Exception ex)
            {
                await AcceptCall("Error al quitar el proveedor\n\n" + ex.GetBaseException().Message,
                                  DialogIdentifiers.Category_Identifier);
                return;
            }
            ProviderList.Delete(ProviderList.SelectedItem);

            ShowSnackbarMessage("Proveedor quitado con éxito");
        }

        private async void OnCategorySelectionChanged(object? c)
        {
            if (c == null) {
                BrandList!.Collection = new();
                BrandList.SelectedItem = null;
            }
            else if(c is CategoryModel selectedCategory)
            {
                BrandList!.Collection = new(await DBBrand.SelectByCategoryId(selectedCategory));
            }
            OnPropertyChanged(nameof(BrandList));
        }
        private async void OnBrandSelectionChanged(object? b)
        {
            if (b is null)
            {
                ProviderList!.Collection = new();
                OnPropertyChanged(nameof(ProviderList));
                return;
            }

            ProviderList!.Collection = new(await DBBrandProviders.SelectByBrand((BrandModel)b));

            OnPropertyChanged(nameof(ProviderList));
        }
    }
}
