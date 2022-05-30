using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBProductOrder : DBAccess
    {
        public static Task Delete(int Id)
            => SaveData(storedProcedure: ProductOrderSP.spProductOrder_Delete, new { Id });
        public static Task Insert(ProductOrderModel pom)
            => SaveData(storedProcedure: ProductOrderSP.spProductOrder_Insert, new { ProviderId = pom.Provider!.Id, pom.Date, pom.ARGPrice, pom.USDPrice, pom.IsRecived, pom.IsPayed });
        public static Task InsertProduct(ProductModel product, int OrderId)
            => SaveData(storedProcedure: ProductOrderSP.spProductOrder_InsertProduct, new { OrderId, product.Code, product.Description, product.ListingPrice, product.Multiplier });
        public static Task Recived(int Id)
            => SaveData(storedProcedure: ProductOrderSP.spProductOrder_Recived, new { Id });
        public static Task Payed(int Id)
            => SaveData(storedProcedure: ProductOrderSP.spProductOrder_Payed, new { Id });

        public static Task<IEnumerable<ProductOrderModel>> SelectAll()
            => LoadData<ProductOrderModel, dynamic>(storedProcedure: ProductOrderSP.spProductOrder_SelectAll, new { });
        public static Task<IEnumerable<ProviderModel>> GetProvider(int Id)
            => LoadData<ProviderModel, dynamic>(storedProcedure: ProductOrderSP.spProductOrder_GetProvider, new { Id });
        public static Task<IEnumerable<ProductModel>> GetProducts(int Id)
            => LoadData<ProductModel, dynamic>(storedProcedure: ProductOrderSP.spProductOrder_GetProducts, new { Id });
    }
}
