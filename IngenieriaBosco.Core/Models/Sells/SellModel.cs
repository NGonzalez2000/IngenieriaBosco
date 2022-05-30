using IngenieriaBosco.Core.Resources.AFIP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IngenieriaBosco.Core.Models.Sells
{
    internal class SellModel : INotify
    {
        private ICollectionView? collectionView;
        private string code = string.Empty;
        private string description = string.Empty;
        private decimal totalPrice;
        public int Id { get; set; }
        public bool IsRetailPrice { get; set; }
        public string Code
        {
            get => code;
            set
            {
                if (SetProperty(ref code, value) && collectionView != null)
                    collectionView.Refresh();
            }
        }
        public string Description
        {
            get => description;
            set
            {
                if (SetProperty(ref description, value) && collectionView != null)
                    collectionView.Refresh();
            }
        }
        public ClientModel? Client { get; set; }
        public ResponsabilidadIVA? ResponsabilidadIVA { get; set; }
        public ObservableCollection<SellProductModel>? ProductList { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }
        public void InsertProduct(SellProductModel product)
        {
            if(ProductList == null || ProductList.Count == 0)
            {
                ProductList = new();
                Currency = product.Currency!;
                OnPropertyChanged(nameof(Currency));
                TotalPrice = decimal.Zero;
            }
            if (product.Currency != Currency) throw new Exception("No se pueden generar facturas con mas de un tipo de moneda.\n");
            if (CheckDupes(product.Code!)) throw new Exception($"Ya se ha agregado previamente el producto {product.Code}.");

            product.SubTotal = decimal.Multiply(product.Price, product.Amount);

            TotalPrice = decimal.Add(product.SubTotal, TotalPrice);

            ProductList.Add(product);
            OnPropertyChanged(nameof(ProductList));
            SetCollectionView();
        }

        public void RemoveProduct(SellProductModel product)
        {
            TotalPrice = decimal.Subtract(TotalPrice, product.SubTotal);

            ProductList!.Remove(product);
            OnPropertyChanged(nameof(ProductList));

            SetCollectionView();
        }
        private void SetCollectionView()
        {
            collectionView = CollectionViewSource.GetDefaultView(ProductList);
            collectionView.Filter = Filter;
            collectionView.Refresh();
        }
        public bool Filter(object o)
        {
            if (o is null) return false;
            SellProductModel product = (SellProductModel)o;
            return Validate(Code, product.Code) && Validate(Description, product.Description);
        }
        private static bool Validate(string fst, string scd)
        {
            ArgumentNullException.ThrowIfNull(scd);
            if (string.IsNullOrEmpty(fst)) return true;
            return scd.Contains(fst);
        }
        private bool CheckDupes(string code)
            => ProductList!.FirstOrDefault(x => x.Code == code, defaultValue: null) != null;
    }
}
