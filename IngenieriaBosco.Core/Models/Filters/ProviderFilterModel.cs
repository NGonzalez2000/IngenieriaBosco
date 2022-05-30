using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class ProviderFilterModel : FilterModel
    {
        public string? ProviderName { get; set; }
        public string? CUIT { get; set; }
        public override bool Filter(object o)
        {
            if(o == null)return false;
            ProviderModel provider = (ProviderModel)o;
            return Validate(ProviderName, provider.Name) &&
                ValidateCUIT(provider.CUIT);
        }

        private bool Validate(string? fst, string? scd)
        {
            if (string.IsNullOrEmpty(fst)) return true;
            if(string.IsNullOrEmpty(scd)) return false;
            return scd.Contains(fst);
        }
        private bool ValidateCUIT(string pCuit)
        {
            if (string.IsNullOrEmpty(CUIT)) return true;
            return pCuit.Contains(pCuit);
        }
    }
}
