using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBBrand : DBAccess
    {
        internal static Task Insert(BrandModel brand,int CategoryId)
            => SaveData(storedProcedure: BrandSP.spBrand_Insert, new { brand.Name, brand.IsDolarValue, CategoryId });
        internal static Task Update(BrandModel brand)
            => SaveData(storedProcedure: BrandSP.spBrand_Update, new { brand.Id, brand.Name, brand.IsDolarValue });
        internal static Task Delete(BrandModel brand)
            => SaveData(storedProcedure: BrandSP.spBrand_Delete, new { brand.Id });
        internal static Task<IEnumerable<BrandModel>> SelectByCategoryId(CategoryModel category)
            => LoadData<BrandModel,dynamic> (storedProcedure: BrandSP.spBrand_SelectByCategoryId, new {CategoryId = category.Id});
        internal static Task<IEnumerable<BrandModel>> CheckBrand(string name)
            => LoadData<BrandModel,dynamic>(storedProcedure:BrandSP.spBrand_CheckBrand, new {Name = name});
        internal static Task<IEnumerable<BrandModel>> SelectByCategory_Provider(int CategoryId, int ProviderId)
            => LoadData<BrandModel, dynamic>(storedProcedure: BrandSP.spBrand_SelectByCategory_Provider, new { CategoryId, ProviderId });
    }
}
