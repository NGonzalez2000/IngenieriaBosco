using IngenieriaBosco.Core.Models.Sells;
using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBSellItem : DBAccess
    {
        internal static Task Insert(SellProductModel spm, int SellId)
            => SaveData(storedProcedure: SellItemSP.spSellItem_Insert,
                new 
                {
                    SellId,
                    spm.Code,
                    spm.Description,
                    spm.Amount,
                    spm.Price,
                    spm.SubTotal,
                    spm.Currency
                });
        internal static Task Update(SellProductModel spm)
            => SaveData_Query("update [dbo].[SellItems] set Currency = @Currency, Amount = @Amount, Price = @Price, SubTotal = @SubTotal where Id = @Id",
                new
                {
                    Currency = spm.Currency,
                    Amount = spm.Amount,
                    Price = spm.Price,
                    SubTotal = spm.SubTotal,
                    Id = spm.Id
                });
        internal static Task Delete(SellProductModel spm)
            => SaveData_Query("Delete from [dbo].[SellItems] where Id = @Id", new { spm.Id });
        internal static Task<IEnumerable<SellProductModel>> SelectBySellId(int Id)
            => LoadData<SellProductModel, dynamic>(storedProcedure: SellItemSP.spSellItem_SelectBySellId, new { SellId = Id });
    }
}
