using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources
{
    public class DBAccess
    {
        private static string ConnectionString()
        {
            return $"Data Source={DataBaseSettings.Default.IP},{DataBaseSettings.Default.Port};" +
                $"Network Library=DBMSSOCN;" +
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
    }
}
