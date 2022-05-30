using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBCurrency : DBAccess
    {
        internal static Task<IEnumerable<decimal>> Select(string type)
            => LoadData<decimal, dynamic>(storedProcedure: CurrencySP.spCurrency_Select, new { Type = type });
    }
}
