namespace CafeSanchez.API.Models.Requests
{
    public class CreateOrder
    {
        public required string CustomerName { get; set; }
        public int Discount { get; set; }
        public CreateOrderline[] Items { get; set; } = [];
    }

    public class CreateOrderline
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
