using IngenieriaBosco.Core.Models.Sells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class SellFilterModel : FilterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CUIL { get; set; }
        public string FactN { get; set; }
        public SellFilterModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            CUIL = string.Empty;
            FactN = string.Empty;
        }
        public override bool Filter(object o)
        {
            if(o == null) return false;
            SellModel sell = (SellModel)o;
            return Validate(FirstName, sell.FirstName) &&
                    Validate(LastName, sell.LastName) &&
                    ValidateCuil(sell.CUIL) &&
                    Validate(FactN, sell.FactN);
        }
        private bool ValidateCuil(string cCUIL)
        {
            if (string.IsNullOrEmpty(CUIL)) return true;
            return cCUIL.Contains(CUIL.TrimStart('0'));
        }
        private bool Validate(string? fst, string scd)
        {
            if (string.IsNullOrEmpty(fst)) return true;
            return scd.Contains(fst);
        }
    }
}
