using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Sells
{
    internal class SellProductModel : IDataErrorInfo
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if(columnName == nameof(Code) && string.IsNullOrEmpty(Code)) return "Falta el código";
                if (columnName == nameof(Description) && string.IsNullOrEmpty(Description)) return "Falta la descripción";
                return string.Empty;
            }
        }

        public void SetCode(string code)
            =>Code = code;
        public void SetDescription(string description)
            =>Description = description;
        public void SetPrice(decimal price)
            =>Price = price;
        public void SetAmount(int amount)
            =>Amount = amount;
        public void SetCurrency(string currency)
            =>Currency = currency;
    }
}
