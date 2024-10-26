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

        public bool CreateUser(string username, string password, string fullname, string email)
        {
            // Generate a 128-bit salt using a sequence of cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            string generatedSalt = Convert.ToBase64String(salt);
            
            // Generates a 256-bit hash
            string generatedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(generatedSalt),
                KeyDerivationPrf.HMACSHA256,
                100000,
                32));

            // Create user object 
            User user = new()
            {
                Username = username,
                Email = email,
                Fullname = fullname,
                PasswordHash = generatedHash,
                Salt = generatedSalt
            };

            // Inserts user into database
            string insertUserSql = "INSERT INTO Users (Username, Email, Fullname, PasswordHash, Salt) " +
                "VALUES (@username, @email, @fullname, @passwordhash, @salt)";
            using IDbConnection connection = new SqlConnection(_connectionString);
            return connection.Execute(insertUserSql, user) == 1;
        }

        public bool ChangePassword(string username, string password)
        {
            // TODO: Implement this...

            // Generate new salt 
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            string saltedString = Convert.ToBase64String(salt);
            
            // Generate new password hash
            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                32));

            // Update database
            string updateUserSql = "UPDATE Users SET PasswordHash = @passwordHash, Salt = @saltedString WHERE Username = @username";
            using IDbConnection connection = new SqlConnection(_connectionString);
            return connection.Execute(updateUserSql, new { username, passwordHash, saltedString })==1;
        }

        // Source: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
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

            // Create passwordhash
            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(user.Salt),
                KeyDerivationPrf.HMACSHA256,
                100000,
                32));

            // Return if password is valid
            return user.PasswordHash == passwordHash;
        }
    }
}
