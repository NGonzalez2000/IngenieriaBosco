using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class ProductOrderSP
    {
        internal const string spProductOrder_SelectAll = "[dbo].[spProductOrder_SelectAll]";
        internal const string spProductOrder_GetProvider = "[dbo].[spProductOrder_GetProvider]";
        internal const string spProductOrder_GetProducts = "[dbo].[spProductOrder_GetProducts]";
        internal const string spProductOrder_Insert = "[dbo].[spProductOrder_Insert]";
        internal const string spProductOrder_Delete = "[dbo].[spProductOrder_Delete]";
        internal const string spProductOrder_InsertProduct = "[dbo].[spProductOrder_InsertProduct]";
    }
}
