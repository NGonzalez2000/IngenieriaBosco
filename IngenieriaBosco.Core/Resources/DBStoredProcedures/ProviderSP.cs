using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.DBStoredProcedures
{
    internal class ProviderSP
    {
        internal const string spProvider_Insert = "[dbo].[spProvider_Insert]";
        internal const string spProvider_Update = "[dbo].[spProvider_Update]";
        internal const string spProvider_Delete = "[dbo].[spProvider_Delete]";
        internal const string spProvider_SelectAll = "[dbo].[spProvider_SelectAll]";
        internal const string spProvider_InsertEmail = "[dbo].[spProvider_InsertEmail]";
        internal const string spProvider_DeleteEmail = "[dbo].[spProvider_DeleteEmail]";
        internal const string spProvider_UpdateEmail = "[dbo].[spProvider_UpdateEmail]";
        internal const string spProvider_SelectEmails = "[dbo].[spProvider_SelectEmails]";
    }
}
