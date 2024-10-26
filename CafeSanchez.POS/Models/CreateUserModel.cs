namespace CafeSanchez.POS.Models
{
    public class CreateUserModel
    {
        public required string Username { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
