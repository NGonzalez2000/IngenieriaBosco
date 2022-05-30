using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class ClientFilterModel : FilterModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CUIL { get; set; }
        public override bool Filter(object o)
        {
            if (o == null) return false;
            ClientModel client = (ClientModel)o;
            return ValidateCuil(client.CUIL) &&
                Validate(FirstName, client.FirstName) &&
                Validate(FirstName, client.FirstName);
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
