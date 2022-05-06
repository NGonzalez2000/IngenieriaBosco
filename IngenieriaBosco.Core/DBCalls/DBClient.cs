using IngenieriaBosco.Core.Resources.DBStoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DBCalls
{
    internal class DBClient : DBAccess
    {
        internal static Task Insert(ClientModel client)
            => SaveData(storedProcedure: ClientSP.spClient_Insert,
                new { 
                    client.FirstName, 
                    client.LastName, 
                    client.CUIL,
                    client.PhoneNumber
                });
        internal static Task Update(ClientModel client)
            => SaveData(storedProcedure: ClientSP.spClient_Update,
                new { 
                    client.Id,
                    client.FirstName, 
                    client.LastName, 
                    client.CUIL,
                    client.PhoneNumber
                });
        internal static Task Delete(ClientModel client)
            => SaveData(storedProcedure: ClientSP.spClient_Delete, new {client.Id});
        internal static Task InsertEmail(ClientModel client, EmailModel email)
            => SaveData(storedProcedure: ClientSP.spClient_InsertEmail, new {ClientId = client.Id, email.Email}); 
        internal static Task UpdateEmail(EmailModel email)
            => SaveData(storedProcedure:ClientSP.spClient_UpdateEmail, new {email.Id, email.Email});
        internal static Task DeleteEmail(EmailModel email)
            => SaveData(storedProcedure:ClientSP.spClient_DeleteEmail, new {email.Id});
        internal static Task<IEnumerable<ClientModel>> SelectAll()
            => LoadData<ClientModel, dynamic>(storedProcedure: ClientSP.spClient_SelectAll, new { });
        internal static Task<IEnumerable<EmailModel>> SelectEmails(ClientModel client)
            => LoadData<EmailModel, dynamic>(storedProcedure: ClientSP.spClient_SelectEmails, new { client.Id });
    }
}
