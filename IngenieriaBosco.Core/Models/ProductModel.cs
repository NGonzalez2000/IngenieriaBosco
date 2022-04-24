using IngenieriaBosco.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public decimal ListingPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalerPrice { get; set; }
        public int Stock { get; set; }
        public int WarningStock { get; set; }
    }
}
