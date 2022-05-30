using IngenieriaBosco.Core.Models.Enums;
using IngenieriaBosco.Core.Models.Excel;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.DialogModels
{
    internal class ExcelImportDialogModel : BaseDialogModel
    {
        public GridListModel<ExcelProduct_ImportModel> Products { get; set; }
        public List<CategoryModel>? Categories { get; set; }
        public List<BrandModel>? Brands { get; set; }
        
        private CategoryModel? selectedCategory;
        private BrandModel? selectedBrand;
        public CategoryModel? SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (SetProperty(ref selectedCategory, value))
                    CategoryChanged();
            }
        }
        public BrandModel? SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (SetProperty(ref selectedBrand, value))
                    BrandChanged();
            }
        }

        public ICommand Edit_Command => new RelayCommand(Edit_Execute);
        public ICommand Delete_Command => new RelayCommand(Delete_Execute);
        public ExcelImportDialogModel(IEnumerable<ExcelProduct_ImportModel> products)
        {
            Products = new(products);
        }
        public async Task<IEnumerable<ExcelResult_ImportModel>?> Import()
        {
            List<ExcelResult_ImportModel> result = new();
            Categories = new(await DBCategory.SelectAll());
            for (int i = 0; i < Products.Collection.Count; i++) Products.Collection[i].Validate_Product();

            ImportProductsDialog dialog = new()
            {
                DataContext = this
            };

            bool response = await DialogHosting(dialog, DialogIdentifiers.Excel_Identifier);

            if (!response) return null;

            foreach (ExcelProduct_ImportModel excelp in Products.Collection)
            {
                ExcelResult_ImportModel importResult = new();
                if(excelp.Operation == ExcelImport_Operations.New_op)
                {
                    try
                    {
                        await DBProduct.Insert(excelp.Product);
                        importResult.result = true;
                        importResult.Message = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        importResult.result = null;
                        importResult.Message = ex.GetBaseException().Message;
                    }
                }
                if(excelp.Operation == ExcelImport_Operations.Update_op)
                {
                    try
                    {
                        await DBProduct.Update(excelp.Product);
                        importResult.result = false;
                        importResult.Message = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        importResult.result = null;
                        importResult.Message = ex.GetBaseException().Message;
                    }
                }
                if(excelp.Operation == ExcelImport_Operations.Error_op)
                {
                    importResult.result = null;
                    importResult.Message = excelp.State;
                }
                result.Add(importResult);
            }

            return result;
        }
        
        private async void Edit_Execute(object? param)
        {
            if (param is null) return;
            if (param is ExcelProduct_ImportModel excelProduct)
            {
                ProductDialogModel dialogModel = new(excelProduct.Product);
                ProductModel? product = await dialogModel.EditProduct(DialogIdentifiers.ImportProduct_Identifier);

                if (product is null) return;

                ExcelProduct_ImportModel newp = new() { Product = (ProductModel)product.ShallowCopy() };

                newp.Validate_Product();

                Products.Edit(excelProduct, newp);
            }
        }
        private void Delete_Execute(object? param)
        {
            if (param is null) return;
            Products.Delete((ExcelProduct_ImportModel)param);
        }
        private async void CategoryChanged()
        {
            if (SelectedCategory is null) return;
            try
            {
                Brands = new(await DBBrand.SelectByCategoryId(SelectedCategory));
                OnPropertyChanged(nameof(Brands));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las marcas\n\n" + ex.GetBaseException().Message);
            }
            for(int i = 0; i < Products.Collection.Count; i++)
            {
                ExcelProduct_ImportModel excelProduct = (ExcelProduct_ImportModel)Products.Collection[i].ShallowCopy();
                excelProduct.Product.Category = SelectedCategory;
                excelProduct.Validate_Product();
                Products.Edit(Products.Collection[i], excelProduct);
            }
        }
        private void BrandChanged()
        {
            for (int i = 0; i < Products.Collection.Count; i++)
            {
                ExcelProduct_ImportModel excelProduct = (ExcelProduct_ImportModel)Products.Collection[i].ShallowCopy();
                excelProduct.Product.Brand = SelectedBrand;
                excelProduct.Validate_Product();
                Products.Edit(Products.Collection[i], excelProduct);
            }
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
