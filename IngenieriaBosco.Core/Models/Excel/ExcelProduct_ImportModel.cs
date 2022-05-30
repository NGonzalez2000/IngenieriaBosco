using IngenieriaBosco.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Excel
{
    internal class ExcelProduct_ImportModel : BaseModel
    {
        
        public bool ExceptionFlag { get; set; }
        public string Operation { get; set; }
        public string State { get; set; }
        public ProductModel Product { get; set; }
        public ExcelProduct_ImportModel()
        {
            ExceptionFlag = false;
            Operation = ExcelImport_Operations.Error_op;
            State = "Falta el Código";
            Product = new();
        }
        public async void SetCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                Product.Code = string.Empty;
                State = Product[nameof(ProductModel.Code)];
                Operation = ExcelImport_Operations.Error_op;
                return;
            }
            ProductModel? prod;
            try
            {
                prod = (await DBProduct.CheckCode(code)).FirstOrDefault(defaultValue:null);
                if (prod != null)
                {
                    Operation = ExcelImport_Operations.Update_op;
                    prod.Category = (await DBProduct.SelectCategory(prod)).FirstOrDefault(defaultValue: null);
                    if (prod.Category != null) prod.Brand = (await DBProduct.SelectBrand(prod)).FirstOrDefault(defaultValue: null);
                }
            }
            catch (Exception)
            {
                ExceptionFlag = true;
                prod = null;
            }

            if (prod == null)
            {
                Product.Code = code;
                Operation = ExcelImport_Operations.New_op;
            }
            else Product = (ProductModel)prod.ShallowCopy();
        }
        public async void SetCategory(string categoryName)
        {
            if (Product.Category != null && categoryName == Product.Category.Name) return;
            if (string.IsNullOrEmpty(categoryName))
            {
                Product.Category = null;
                State = Product[nameof(ProductModel.Category)];
                Operation = ExcelImport_Operations.Error_op;
                return;
            }
            CategoryModel? category;
            try
            {
                category = (await DBCategory.SelectByName(categoryName)).FirstOrDefault(defaultValue:null);
            }
            catch (Exception)
            {
                ExceptionFlag = true;
                category = null;
            }
            if (category != null) Product.Category = (CategoryModel)category.ShallowCopy();
            else
            {
                Operation = ExcelImport_Operations.Error_op;
                State = "Falta la Categoría";
            }
        }
        public async void SetBrand(string brandName)
        {
            if (Product.Category is null || string.IsNullOrEmpty(brandName)) return;
            BrandModel? brand;
            try
            {
                brand = (await DBBrand.CheckBrand(brandName)).FirstOrDefault(defaultValue:null);
            }
            catch (Exception)
            {
                ExceptionFlag = true;
                brand = null;
            }
            if(brand != null) Product.Brand = (BrandModel)brand.ShallowCopy();
        }
        public void Validate_Product()
        {
            string state = Product[nameof(ProductModel.Code)];
            if (SetState(state))
                return;
            state = Product[nameof(ProductModel.Description)];
            if (SetState(state))
                return;
            state = Product[nameof(ProductModel.Category)];
            if (SetState(state))
                return;
        }
        public bool SetState(string state)
        {
            State = state;
            if (string.IsNullOrEmpty(state))
            {
                Operation = Product.Id > 0 ? ExcelImport_Operations.Update_op : ExcelImport_Operations.New_op;
                return false;
            }
            Operation = ExcelImport_Operations.Error_op;
            return true;
        }
    }
}
