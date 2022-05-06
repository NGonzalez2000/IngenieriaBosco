using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class BrandSP 
    {
        internal const string spBrand_Insert = "[dbo].[spBrand_Insert]";
        internal const string spBrand_Update = "[dbo].[spBrand_Update]";
        internal const string spBrand_Delete = "[dbo].[spBrand_Delete]";
        internal const string spBrand_SelectByCategoryId = "[dbo].[spBrand_SelectByCategoryId]";
    }
}
