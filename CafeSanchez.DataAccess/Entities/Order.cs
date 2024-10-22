using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess.Entities;

public class Order
{
    public int Id { get; set; }
    public Guid WebId { get; set; } = Guid.Empty;
    public required string CustomerName { get; set; }
    public DateTime Date { get; set; }
    public decimal Discount { get; set; }
    public IList<Orderline> Orderlines { get; set; } = [];
    public byte[] Version { get; } = new byte[8];
    public required string Status { get; set; }
}
