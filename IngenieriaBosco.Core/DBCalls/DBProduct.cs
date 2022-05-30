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
            => SaveData(storedProcedure:ProductSP.spProduct_Insert, 
                new
                {
                    product.Description,
                    product.Code,
                    CategoryId = product.Category!.Id,
                    BrandId = product.Brand!.Id,
                    product.ListingPrice,
                    product.RetailPrice,
                    product.WholesalerPrice,
                    product.Stock,
                    product.WarningStock
                });

        internal static Task Update(ProductModel product)
            => SaveData(storedProcedure:ProductSP.spProduct_Update,
                new
                {
                    product.Id,
                    product.Description,
                    product.Code,
                    CategoryId = product.Category!.Id,
                    BrandId = product.Brand!.Id,
                    product.ListingPrice,
                    product.RetailPrice,
                    product.WholesalerPrice,
                    product.Stock,
                    product.WarningStock
                });

        internal static Task Delete(ProductModel product)
            => SaveData(storedProcedure: ProductSP.spProduct_Delete, new { product.Id });

        public static Task Recived(int Id)
            => SaveData(storedProcedure: ProductSP.spProduct_Recived, new { Id });

        internal static Task<IEnumerable<ProductModel>> CheckCode(string code)
            => LoadData<ProductModel,dynamic>(storedProcedure:ProductSP.spProduct_CheckCode, new {Code = code});
        internal static Task<IEnumerable<ProductModel>> SelectByCategory(CategoryModel category)
            => LoadData<ProductModel, dynamic>(storedProcedure: ProductSP.spProduct_SelectByCategory, new { CategoryId = category.Id });
        internal static Task<IEnumerable<CategoryModel>> SelectCategory(ProductModel product)
            => LoadData<CategoryModel, dynamic>(storedProcedure: ProductSP.spProduct_GetCategory, new { product.Id });
        internal static Task<IEnumerable<BrandModel>> SelectBrand(ProductModel product)
            => LoadData<BrandModel, dynamic>(storedProcedure: ProductSP.spProduct_SelectBrand, new { product.Id });
        internal static Task<IEnumerable<ProductModel>> SelectByBrand(BrandModel brand)
            => LoadData<ProductModel, dynamic>(storedProcedure: ProductSP.spProduct_SelectByBrand, new { BrandId = brand.Id });
    }
}
