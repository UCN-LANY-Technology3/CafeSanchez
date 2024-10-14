using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess.Entities
{
    public class Order
    {
        public required string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public IList<Orderline> Orderlines { get; set; } = new List<Orderline>();
    }
}
