using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Sells
{
    internal class SellItemModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public SellItemModel(ProductModel product)
        {
            Product = (ProductModel)product.ShallowCopy();
        }
    }
}
