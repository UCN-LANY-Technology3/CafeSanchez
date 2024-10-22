using CafeSanchez.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess.DAO
{
    public interface IOrderDao : IDao<Order>
    {
        Order FindById(int id);
    }
}
