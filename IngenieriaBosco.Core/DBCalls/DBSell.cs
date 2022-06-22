using IngenieriaBosco.Core.Models.Sells;
using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBSell : DBAccess
    {
        internal static Task Insert(SellModel sell)
            => SaveData(storedProcedure: SellSP.spSell_Insert, 
                new
                {
                    sell.FactN,
                    sell.PtoVta,
                    sell.FirstName,
                    sell.LastName,
                    sell.CUIL,
                    ResponsabilidadIVA = sell.RespIVA!.Descripcion,
                    sell.Currency,
                    sell.TotalPrice,
                    sell.IsPayed,
                    sell.IsRetailPrice,
                    sell.Date
                });
        internal static Task Update(SellModel sell)
            => SaveData(storedProcedure: SellSP.spSell_Update, new
            {
                sell.Id,
                sell.Currency,
                sell.TotalPrice,
                sell.IsPayed
            });
        internal static Task Delete(SellModel sell)
            => SaveData_Query("Delete from [dbo].[SellItems] where SellId = @Id\n delete from [dbo].[Sells] where [Id] = @Id", new
            {
                sell.Id
            });
        internal static Task<IEnumerable<SellModel>> SelectAll()
            => LoadData<SellModel,dynamic>(storedProcedure: SellSP.spSell_SelectAll, new { });
    }
}
