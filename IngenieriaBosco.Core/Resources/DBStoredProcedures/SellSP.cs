using IngenieriaBosco.Core.Models.Sells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class SellSP 
    {
        internal const string spSell_Insert = "[dbo].[spSell_Insert]";
        internal const string spSell_Update = "[dbo].[spSell_Update]";
        internal const string spSell_Delete = "[dbo].[spSell_Delete]";
        internal const string spSell_SelectAll = "[dbo].[spSell_SelectAll]";
    }
}
