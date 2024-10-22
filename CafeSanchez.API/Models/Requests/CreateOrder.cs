namespace CafeSanchez.API.Models.Requests
{
    public class CreateOrder
    {
        public Guid OrderId { get; set; }
        public required string NewStatus { get; set; }
    }
}
