using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBProvider : DBAccess
    {
        internal static Task Insert(ProviderModel provider)
            => SaveData(storedProcedure: ProviderSP.spProvider_Insert, new
            {
                provider.Name,
                provider.PhoneNumber,
                provider.CUIT,
                provider.Web
            });
        internal static Task Update(ProviderModel provider)
            => SaveData(storedProcedure: ProviderSP.spProvider_Update, new
            {
                provider.Id,
                provider.Name,
                provider.PhoneNumber,
                provider.CUIT,
                provider.Web
            });
        internal static Task Delete(ProviderModel provider)
            => SaveData(storedProcedure: ProviderSP.spProvider_Delete, new { provider.Id });
        internal static Task InsertEmail(ProviderModel provider, EmailModel email)
            => SaveData(storedProcedure: ProviderSP.spProvider_InsertEmail,
                new
                {
                    email.Email, 
                    ProviderId = provider.Id 
                });
        internal static Task UpdateEmail(EmailModel email)
            => SaveData(storedProcedure: ProviderSP.spProvider_UpdateEmail,
                new
                {
                    email.Id,
                    email.Email 
                });
        internal static Task DeleteEmail(EmailModel email)
            => SaveData(storedProcedure: ProviderSP.spProvider_DeleteEmail,
                new
                {
                    email.Id
                });


        internal static Task<IEnumerable<ProviderModel>> SelectAll()
            => LoadData<ProviderModel, dynamic>(storedProcedure: ProviderSP.spProvider_SelectAll, new { });
        internal static Task<IEnumerable<EmailModel>> SelectEmails(ProviderModel provider)
            => LoadData<EmailModel, dynamic>(storedProcedure: ProviderSP.spProvider_SelectEmails, new { ProviderId = provider.Id });
    }
}
