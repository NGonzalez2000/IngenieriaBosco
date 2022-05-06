using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class ClientSP
    {
        internal const string spClient_Insert = "[dbo].[spClient_Insert]";
        internal const string spClient_Update = "[dbo].[spClient_Update]";
        internal const string spClient_Delete = "[dbo].[spClient_Delete]";
        internal const string spClient_SelectAll = "[dbo].[spClient_SelectAll]";

        internal const string spClient_InsertEmail = "[dbo].[spClient_InsertEmail]";
        internal const string spClient_UpdateEmail = "[dbo].[spClient_UpdateEmail]";
        internal const string spClient_DeleteEmail = "[dbo].[spClient_DeleteEmail]";
        internal const string spClient_SelectEmails = "[dbo].[spClient_SelectEmails]";

    }
}
