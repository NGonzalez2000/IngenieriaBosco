using IngenieriaBosco.Core.DBCalls;
using IngenieriaBosco.Core.Models.Generics;
using System.ComponentModel;
using System.Linq;

namespace IngenieriaBosco.Core.Models
{
    public class CategoryModel : BaseModel, IDataErrorInfo
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        private bool isDupe;


        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name))
                {
                    if (string.IsNullOrEmpty(Name)) return "Este campo es olbigatorio";
                    CheckDup(Name);
                    if (isDupe) return $"Ya existe la categoría {Name}";
                }
                return string.Empty;
            }
        }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }
        private async void CheckDup(string name)
        {
            isDupe = (await DBCategory.IsDuplicate(name)).First() == 1;
        }
    }
}
