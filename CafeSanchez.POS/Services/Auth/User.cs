namespace CafeSanchez.POS.Services.Auth
{
    public class User
    {
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Salt { get; set; }
        public required string PasswordHash { get; set; }
    }
}
