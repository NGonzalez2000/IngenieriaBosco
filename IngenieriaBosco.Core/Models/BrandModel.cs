
using System.ComponentModel;

namespace IngenieriaBosco.Core.Models
{
    public class BrandModel : BaseModel, IDataErrorInfo
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDolarValue { get; set; }
        public string this[string columnName] {
            get
            {
                if(columnName == nameof(Name))
                {
                    if (string.IsNullOrEmpty(Name)) return "Este campo es obligatiorio";
                }
                return string.Empty;
            }
        }

        public string Error => string.Empty;
    }
}
