using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class ProductOrderFilterModel : FilterModel
    {
        public int Number { get; set; }
        public string? ProviderName { get; set; }
        public override bool Filter(object o)
        {
            if (o is null) return false;
            ProductOrderModel pom = (ProductOrderModel)o;
            return Validate(ProviderName, pom.Provider!.Name) &&
                    ValidateNum(Number, pom.Id);
        }
        private bool Validate(string? fst, string? scd)
        {
            if (string.IsNullOrEmpty(fst)) return true;
            if (string.IsNullOrEmpty(scd)) return false;
            return scd.Contains(fst);
        }
        private bool ValidateNum(int fst, int scd)
        {
            if (fst == 0) return false;
            return fst == scd;
        }
    }
}
