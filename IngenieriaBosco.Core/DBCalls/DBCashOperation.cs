using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBCashOperation : DBAccess
    {
        internal static Task Insert(CashOperationModel com)
            => SaveData(storedProcedure: CashOperationSP.spCashOperation_Insert,
                new
                {
                    com.Amount,
                    com.Operation,
                    com.Currency,
                    com.Date
                });
        internal static Task<IEnumerable<CashOperationModel>> SelectTop(int quantity)
            => LoadData<CashOperationModel, dynamic>(storedProcedure: CashOperationSP.spCashOperation_SelectTop, new {Quantity = quantity});
    }
}
