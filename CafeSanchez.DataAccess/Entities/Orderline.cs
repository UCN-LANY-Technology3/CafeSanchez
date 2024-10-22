using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess.Entities;

public class Orderline
{
    public int Quantity { get; set; }
    public required Product Product{ get; set; }
}
