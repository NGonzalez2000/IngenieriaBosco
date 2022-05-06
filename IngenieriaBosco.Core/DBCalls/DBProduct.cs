using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBProduct : DBAccess
    {
        internal static Task Insert(ProductModel product)
            => SaveData(storedProcedure:ProductSP.spProduct_Insert, product);

        internal static Task Update(ProductModel product)
            => SaveData(storedProcedure:ProductSP.spProduct_Update, product);

        internal static Task Delete(ProductModel product)
            => SaveData(storedProcedure: ProductSP.spProduct_Delete, product);

        internal static Task<IEnumerable<ProductModel>> SelectByCategory(CategoryModel category)
            => LoadData<ProductModel, dynamic>(storedProcedure: ProductSP.spProduct_SelectByCategory, new { CategoryId = category.Id });

        internal static Task<IEnumerable<BrandModel>> SelectBrand(ProductModel product)
            => LoadData<BrandModel, dynamic>(storedProcedure: ProductSP.spProduct_SelectBrand, new { product.Id });
    }
}
