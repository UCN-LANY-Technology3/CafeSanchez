using CafeSanchez.DataAccess.Entities;

namespace CafeSanchez.API.Models
{
    public static class Mappers
    {
        public static IEnumerable<OrderDto> Map(this IEnumerable<Order> orders)
        {
            foreach(Order order in orders)
            {
                yield return order.Map();
            }
        }

        public static OrderDto Map(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                Discount = (int)order.Discount,
                CustomerName = order.CustomerName,
                Status = order.Status,
                WebId = order.WebId,
                Version = order.Version,
                Orderlines = [.. order.Orderlines.Map()]
            };
        }

        public static IEnumerable<OrderlineDto> Map(this IEnumerable<Orderline> orderlines)
        {
            foreach(Orderline orderline in orderlines)
            {
                yield return orderline.Map();
            }
        }

        public static OrderlineDto Map(this Orderline orderline)
        {
            return new OrderlineDto()
            {
                Product = orderline.Product.Map(),
                Quantity = orderline.Quantity,
            };
        }

        public static IEnumerable<ProductDto> Map(this IEnumerable<Product> products)
        {
            foreach (Product product in products)
            {
                yield return product.Map();
            }
        }

        public static ProductDto Map(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                WebId= product.WebId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };
        }
    }
}
