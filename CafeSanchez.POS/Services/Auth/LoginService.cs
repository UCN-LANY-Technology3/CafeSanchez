using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace CafeSanchez.POS.Services.Auth
{
    public class LoginService(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public bool CreateUser(User user)
        {
            // TODO: Implement this...
            throw new NotImplementedException();
        }

        public bool ChangePassword(string username, string password)
        {
            // TODO: Implement this
            throw new NotImplementedException();
        }

        public bool Validate(string username, string password, out User? user)
        {
            // Get user
            IDbConnection connection = new SqlConnection(_connectionString);
            string selectUserSql = "SELECT * FROM Users WHERE Username = @username";
            user = connection.QuerySingleOrDefault<User>(selectUserSql, new { Username = username });

            if (user == null)
            {
                return false;
            }

            // Validate passwordhash
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(user.Salt),
                KeyDerivationPrf.HMACSHA256,
                100000,
                32));

            // Return if user was validated
            return user.PasswordHash == hashedPassword;
        }
    }
}
