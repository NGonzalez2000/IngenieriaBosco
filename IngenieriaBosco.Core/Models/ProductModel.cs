using IngenieriaBosco.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models
{
    public class ProductModel : BaseModel,IDataErrorInfo
    {

        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal ListingPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalerPrice { get; set; }
        public int Stock { get; set; } = 0;
        public int WarningStock { get; set; } = 0;
        public int Multiplier { get; set; } = 0;
        public CategoryModel? Category { get; set; }
        public BrandModel? Brand { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Code) && string.IsNullOrEmpty(Code)) return "Falta el código";
                if (columnName == nameof(Description) && string.IsNullOrEmpty(Description)) return "Falta descripción";
                if (columnName == nameof(Category) && Category is null) return "Debe seleccionar una categoría";
                return string.Empty;
            }
        }
        public string Error => string.Empty;
    }
}
