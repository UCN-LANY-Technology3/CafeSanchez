namespace CafeSanchez.POS.Models
{
    public class PosModel
    {
        public required string CashierName { get; set; }

        public IEnumerable<ProductModel>? AvailableProducts { get; init; }

        public IEnumerable<OrderModel>? ActiveOrders { get; init; }
    }
}
