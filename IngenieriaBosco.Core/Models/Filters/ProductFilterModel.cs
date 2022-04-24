using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.Models.Filters
{
    public class ProductFilterModel : FilterModel
    {
        private string? selectedCategory;
        public string? ProductDescription { get; set; }
        public string? ProductCode { get; set; }
        public string? SelectedCategory 
        {
            get => selectedCategory;
            set
            {
                SetProperty(ref selectedCategory, value);
                Brands = new ();
            }
        }
        public string? SelectedBrand { get; set; }
        public List<BrandModel> Brands { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public ProductFilterModel()
        {
            Brands = new();
            Categories = new();
        }
        public override bool Filter(object o)
        {
            if (o == null) return false;
            return o is ProductModel p
                && Validate(ProductDescription, p.Description)
                && Validate(ProductCode, p.Code)
                && Validate(SelectedCategory, p.CategoryName)
                && Validate(SelectedBrand, p.BrandName);
        }

        private static bool Validate(string? fst, string? scd)
        {
            if(fst == null || scd == null) return true;
            if(fst == string.Empty) return true;
            return scd.Contains(fst);
        }
    }
}
