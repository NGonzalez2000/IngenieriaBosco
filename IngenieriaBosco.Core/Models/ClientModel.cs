using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IngenieriaBosco.Core.Models
{
    internal class ClientModel : BaseModel, IDataErrorInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CUIL { get; set; }
        public string PhoneNumber { get; set; }
        public ObservableCollection<EmailModel> Emails { get; set; }

        public ClientModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            CUIL = string.Empty;
            PhoneNumber = string.Empty;
            Emails = new();
        }
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if(columnName == nameof(FirstName))
                {
                    if (string.IsNullOrEmpty(FirstName)) return "Este campo es obligatorio";
                }
                if(columnName == nameof(LastName))
                {
                    if (string.IsNullOrEmpty(LastName)) return "Este campo es obligatorio";
                }
                if (columnName == nameof(CUIL))
                {
                    if (string.IsNullOrEmpty(CUIL)) return "Este campo es obligatorio";
                    if (!Resources.AFIP.CUIL.IsValid(CUIL)) return "CUIT inválido";
                }
                return string.Empty;
            }
        }

    }
}
