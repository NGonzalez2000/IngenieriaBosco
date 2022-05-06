using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class BrandProviderSP
    {
        internal const string spBrandProvider_Insert = "[dbo].[spBrandProviders_Insert]";
        internal const string spBrandProvider_SelectByBrand = "[dbo].[spBrandProviders_SelectByBrand]";
        internal const string spBrandProvider_Delete_Provider = "[dbo].[spBrandProviders_Delete_Provider]";
    }
}
