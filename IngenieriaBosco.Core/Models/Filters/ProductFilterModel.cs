﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.Models.Filters
{
    public class ProductFilterModel : FilterModel
    {
        private CategoryModel? selectedCategory;
        public string? ProductDescription { get; set; }
        public string? ProductCode { get; set; }
        public CategoryModel? SelectedCategory 
        {
            get => selectedCategory;
            set
            {
                if(SetProperty(ref selectedCategory, value))
                    SetBrands(selectedCategory);
            }
        }
        public BrandModel? SelectedBrand { get; set; }
        public List<BrandModel> Brands { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public ProductFilterModel()
        {
            Brands = new();
            Categories = new();
            SetCategories();
        }
        public override bool Filter(object o)
        {
            if (o is null || SelectedCategory is null) return false;
            return o is ProductModel p
                && Validate(ProductDescription, p.Description)
                && Validate(ProductCode, p.Code)
                && Validate(SelectedCategory.Name, p.Category.Name)
                && (SelectedBrand != null && Validate(SelectedBrand.Name, p.Brand.Name));
        }

        private static bool Validate(string? fst, string? scd)
        {
            if(fst == null || scd == null) return true;
            if(fst == string.Empty) return true;
            return scd.Contains(fst);
        }
        private async void SetBrands(CategoryModel? category)
        {
            if (category is null) return;
            Brands = new(await DBBrand.SelectByCategoryId(category));
            OnPropertyChanged(nameof(Brands));
        }
        private async void SetCategories()
        {
            Categories = new(await DBCategory.SelectAll());
            OnPropertyChanged(nameof(Categories));
        }
    }
}
