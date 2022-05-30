using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models
{
    internal class ProductOrderModel
    {
        public int Id { get; set; }
        public ObservableCollection<ProductModel>? Products { get; set; }
        public ProviderModel? Provider { get; set; }
        public string Date { get; set; }
        public DateOnly SortDate => DateOnly.Parse(Date);
        public decimal USDPrice { get; set; }
        public decimal ARGPrice { get; set; }
        public bool IsPayed { get; set; }
        public bool IsRecived { get; set; }
        public ProductOrderModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Now).ToShortDateString();
        }

        public void InsertProduct(ProductModel product)
        {
            if (Products == null)
                Products = new();

            if (product.Brand == null || !product.Brand.IsDolarValue)
                ARGPrice = decimal.Add(ARGPrice, decimal.Multiply(product.ListingPrice, product.Multiplier));
            else
                USDPrice = decimal.Add(USDPrice, decimal.Multiply(product.ListingPrice, product.Multiplier));

            Products.Add(product);
        }

        public void RemoveProduct(ProductModel product)
        {
            if (product.Brand == null || !product.Brand.IsDolarValue)
                ARGPrice = decimal.Add(ARGPrice, decimal.Negate(decimal.Multiply(product.ListingPrice, product.Multiplier)));
            else
                USDPrice = decimal.Add(USDPrice, decimal.Negate(decimal.Multiply(product.ListingPrice, product.Multiplier)));

            Products!.Remove(product);
        }
    }
}
