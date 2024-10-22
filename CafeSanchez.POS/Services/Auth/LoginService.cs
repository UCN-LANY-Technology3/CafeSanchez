using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CafeSanchez.POS.Services.Auth
{
    public class LoginService(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public bool Validate(string username, string password, out User? user)
        {
            IDbConnection conn = new SqlConnection(_connectionString);

            user = conn.QuerySingleOrDefault<User>("SELECT Fullname, Email FROM Users WHERE Username = @username AND Password = @password", new { username, password });

            return user != null;
        }
    }
}
