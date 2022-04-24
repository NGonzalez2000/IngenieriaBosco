using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class CategorySP
    {
        internal const string spCategory_Insert = "[dbo].[spCategory_Insert]";
        internal const string spCategory_Update = "[dbo].[spCategory_Update]";
        internal const string spCategory_Delete = "[dbo].[spCategory_Delete]";
        internal const string spCategory_SelectAll = "[dbo].[spCategory_SelectAll]";
        internal const string spCategory_IsDuplicate = "[dbo].[spCategory_IsDuplicate]";
    }
}
