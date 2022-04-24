using Dapper;
using IngenieriaBosco.Core.Models.Controls;
using IngenieriaBosco.Front.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources
{
    public class DBAccess
    {
        private static string ConnectionString()
        {
            return $"Data Source={DataBaseSettings.Default.IP},{DataBaseSettings.Default.Port};" +
                $"Initial Catalog = {DataBaseSettings.Default.DataBase};" +
                $" User ID = {DataBaseSettings.Default.User};" +
                $" Password = {DataBaseSettings.Default.Password};";
        }

        public static async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using SqlConnection connection = new(ConnectionString());
            
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
        public static async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using SqlConnection connection = new(ConnectionString());
           
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
        public static async Task<int> GetId(string TableName)
        {
            var output = await LoadData<int, dynamic>(storedProcedure: "[dbo].[spId_GetLastId]", new { TableName });
            return output.First();
        }
        public static string TestConnection()
        {
            try
            {
                using SqlConnection connection = new(ConnectionString());
                connection.Query<int>("Select 1");
                return string.Empty;
            }
            catch (System.Exception ex)
            {
                return ex.GetBaseException().Message;
            }
            
        }
    }
}
