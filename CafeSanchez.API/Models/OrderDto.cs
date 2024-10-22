using CafeSanchez.DataAccess.Entities;

namespace CafeSanchez.API.Models
{
    public class OrderDto
    {
        public required int Id { get; set; }
        public required Guid WebId { get; set; }
        public required string CustomerName { get; set; }
        public required int Discount { get; set; }
        public required string Status { get; set; }
        public Orderline[] Orderlines { get; set; } = [];
        public byte[] Version { get; set; } = new byte[8];
    }
}
