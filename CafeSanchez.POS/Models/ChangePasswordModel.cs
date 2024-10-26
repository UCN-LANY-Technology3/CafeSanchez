namespace CafeSanchez.POS.Models
{
    public class ChangePasswordModel
    {
        public required string NewPassword { get; set; }
        public required string OldPassword { get; set; }
    }
}
