using IngenieriaBosco.Core.Models.Generics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Sells
{
    internal class SellOrderModel : BaseModel
    {
        private bool isRetailPrice;

        public SellOrderModel()
        {
            Products = new();
            isRetailPrice = true;
        }


        public DateOnly Date  { get; set; }
        public bool IsRetailPrice
        {
            get => isRetailPrice;
            set 
            {
                if (SetProperty(ref isRetailPrice, value))
                    Recalculate();
            }
        }
        public ClientModel? Client { get; set; }
        public ObservableCollection<SellItemModel> Products { get; set; }
        public decimal TotalAR { get; set; }
        public decimal TotalUSD { get; set; }

        public void InsertProduct(ProductModel product)
        {
            Products.Add(new(product));          
        }
        public void QuantityChanged(ProductModel product, int quantity)
        {
            int indx = Products.IndexOf(Products.FirstOrDefault(x => x.Product.Id == product.Id, defaultValue:new(new())));
            if (indx == -1) return;

            AddPrice(product, quantity - Products[indx].Quantity);
            
        }
        private void AddPrice(ProductModel product, int quantity)
        {
            if (product.Brand!.IsDolarValue)
            {
                if (IsRetailPrice) TotalUSD += product.RetailPrice * quantity;
                else TotalUSD += product.WholesalerPrice * quantity;
            }
            else
            {
                if (IsRetailPrice) TotalAR += product.RetailPrice * quantity;
                else TotalAR += product.WholesalerPrice * quantity;
            }
            OnPropertyChanged(nameof(TotalUSD));
            OnPropertyChanged(nameof(TotalAR));
        }
        private void Recalculate()
        {
            TotalUSD = 0m;
            TotalAR = 0m;

            foreach (SellItemModel item in Products)
                AddPrice(item.Product, item.Quantity);
        }
    }
}
