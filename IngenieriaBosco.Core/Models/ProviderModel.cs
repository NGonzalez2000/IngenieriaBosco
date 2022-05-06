using IngenieriaBosco.Core.Resources.AFIP;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IngenieriaBosco.Core.Models
{
    public class ProviderModel : BaseModel,IDataErrorInfo
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public ObservableCollection<EmailModel>? Emails { get; set; }
        public string CUIT { get; set; } = string.Empty;
        public string Web { get; set; } = string.Empty;

        public string this[string columnName]
        {
            get
            {
                if(columnName == nameof(Name))
                {
                    if (string.IsNullOrEmpty(Name)) return "Este campo es obligatorio";
                }     
                if(columnName == nameof(CUIT))
                {
                    if (string.IsNullOrEmpty(CUIT)) return "Este campo es obligatorio";
                    if (!CUIL.IsValid(CUIT)) return "CUIT inválido";
                }
                return string.Empty;
            }
        }
        public string Error => string.Empty;
    }
}
