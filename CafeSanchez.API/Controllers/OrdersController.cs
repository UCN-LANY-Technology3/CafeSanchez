using CafeSanchez.API.Models;
using CafeSanchez.API.Models.Requests;
using CafeSanchez.DataAccess.DAO;
using CafeSanchez.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeSanchez.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderDao orderDao, IProductDao productDao) : ControllerBase
    {
        private readonly IOrderDao _orderDao = orderDao;
        private readonly IProductDao _productDao = productDao;

        [HttpGet]
        public IEnumerable<OrderDto> GetOrders()
        {
            return _orderDao.Read().Map();
        }

        [HttpPost]
        public OrderDto CreateOrder(CreateOrder order)
        {
            // Validate input
            IEnumerable<Product> products = _productDao.FindByWebIds(order.Items.Select(i => i.Id).ToArray());
            if (products.Count() != order.Items.Length)
            {
                throw new Exception("Error in product data");
            }

            // Create order entity
            Order orderData = new()
            {
                CustomerName = order.CustomerName,
                Status = "New",
                Date = DateTime.Now,
                Discount = order.Discount
            };

            // Create and add orderline entities
            foreach (CreateOrderline orderline in order.Items)
            {
                orderData.Orderlines.Add(
                    new()
                    {
                        Product = products.Single(p => p.WebId == orderline.Id),
                        Quantity = orderline.Quantity
                    });
            }

            // Returning the created order object with all the generated values
            return _orderDao.Create(orderData).Map();
        }

        [HttpPatch]
        public bool UpdateOrderStatus(UpdateOrderStatus data)
        {
            throw new NotImplementedException();
        }
    }
}
