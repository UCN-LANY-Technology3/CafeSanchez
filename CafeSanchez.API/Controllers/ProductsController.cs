using CafeSanchez.API.Models;
using CafeSanchez.DataAccess;
using CafeSanchez.DataAccess.DAO;
using CafeSanchez.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CafeSanchez.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductDao _productDao) : ControllerBase
    {
        private readonly IProductDao _productDao = _productDao;

        [HttpGet]
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var data = _productDao.Read().ToArray();

            foreach (var entity in data)
            {
                yield return new ProductDto
                {
                    Id = entity.Id,
                    WebId = entity.WebId,
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price
                };
            }
        }
    }
}
