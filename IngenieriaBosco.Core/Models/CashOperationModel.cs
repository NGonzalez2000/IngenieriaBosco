using IngenieriaBosco.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models
{
    public class CashOperationModel : BaseModel, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Date { get; set; }

        public CashOperationModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Now).ToShortDateString();
            Operation = string.Empty;
            Currency = string.Empty;
        }
        public void SetAmount(decimal val)
            => Amount = val;
        public void SetOperation(string operation)
            => Operation = operation;
        public void SetCurrency(string currency)
            => Currency = currency;

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Amount))
                {
                    if (decimal.Compare(Amount, 0m) <= 0) return "El valor debe ser mayor a $ 0.00";
                }
                return string.Empty;
            }
        }
    }
}
