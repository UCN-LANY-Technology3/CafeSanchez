using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CafeSanchez.POS.Services.Auth
{
    public class UserService(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public bool ValidateLogin(string username, string password)
        {
            IDbConnection conn = new SqlConnection(_connectionString);

            var user = conn.QuerySingleOrDefault("SELECT * FROM Users WHERE Username = @username AND Password = @password", new { username, password });

            return user != null;
        }
    }
}
