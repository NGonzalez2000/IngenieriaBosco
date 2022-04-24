using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBCategory : DBAccess
    {
        internal static Task Insert(CategoryModel category)
            => SaveData(storedProcedure:CategorySP.spCategory_Insert , new {CategoryName = category.Name});
        internal static Task Update(CategoryModel category)
            => SaveData(storedProcedure: CategorySP.spCategory_Update, new { Id = category.Id, CategoryName = category.Name });
        internal static Task Delete(CategoryModel category)
            => SaveData(storedProcedure: CategorySP.spCategory_Delete, new {category.Id});
        internal static Task<IEnumerable<CategoryModel>> SelectAll()
            => LoadData<CategoryModel, dynamic>(storedProcedure: CategorySP.spCategory_SelectAll, new { });
        internal static Task<IEnumerable<int>> IsDuplicate(string CategoryName)
            => LoadData<int, dynamic>(storedProcedure: CategorySP.spCategory_IsDuplicate, new {CategoryName = CategoryName});
    }
}
