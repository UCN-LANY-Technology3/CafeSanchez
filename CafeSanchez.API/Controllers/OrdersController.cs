using CafeSanchez.API.Models;
using CafeSanchez.API.Models.Requests;
using CafeSanchez.DataAccess.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeSanchez.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderDao orderDao) : ControllerBase
    {
        private readonly IOrderDao _orderDao = orderDao;

        [HttpGet]
        public IEnumerable<OrderDto> GetOrders()
        {
            var orders = _orderDao.Read();

            foreach (var order in orders)
            {


                yield return new OrderDto()
                {
                    Id = order.Id,
                    Discount = (int)order.Discount,
                    CustomerName = order.CustomerName,
                    Status = order.Status,
                    WebId = order.WebId,
                    Version = order.Version,    
                    Orderlines = [.. order.Orderlines]
                };
            }
        }

        [HttpPost]
        public OrderDto CreateOrder(CreateOrder order)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public bool UpdateOrderStatus(UpdateOrderStatus data)
        {
            throw new NotImplementedException();
        }
    }
}
