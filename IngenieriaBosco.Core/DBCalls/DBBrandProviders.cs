using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBBrandProviders : DBAccess
    {
        internal static Task Insert(BrandModel brand, ProviderModel provider)
            => SaveData(storedProcedure: BrandProviderSP.spBrandProvider_Insert, new { BrandId = brand.Id, ProviderId = provider.Id });
        internal static Task Delete_Provider(BrandModel brand, ProviderModel provider)
            => SaveData(storedProcedure: BrandProviderSP.spBrandProvider_Delete_Provider, new { BrandId = brand.Id, ProviderId = provider.Id });
        internal static Task<IEnumerable<ProviderModel>> SelectByBrand(BrandModel brand)
            => LoadData<ProviderModel, dynamic>(storedProcedure: BrandProviderSP.spBrandProvider_SelectByBrand, new { BrandId = brand.Id });
    }
}
