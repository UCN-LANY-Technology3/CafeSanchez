namespace CafeSanchez.API.Models.Requests
{
    public class UpdateOrderStatus
    {
        public Guid OrderId { get; set; }
        public required string NewStatus { get; set; }
    }
}
