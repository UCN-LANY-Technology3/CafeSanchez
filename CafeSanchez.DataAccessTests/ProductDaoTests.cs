using CafeSanchez.DataAccess.DAO;
using CafeSanchez.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccessTests
{
    public class ProductDaoTests
    {
        ProductDao _productDao;

        [SetUp]
        public void SetUp()
        {
            _productDao = new ProductDao("Server=192.168.56.101; Database=CafeSanchez; User Id=sa; Password=P@$$w0rd; TrustServerCertificate=True");
        }

        [Test]
        public void ReadProductsSuccessfullyTest()
        {
            IEnumerable<Product> products = _productDao.Read();

            Assert.That(products, Is.Not.Null);
            Assert.That(products.Count(), Is.EqualTo(9));

            foreach (Product product in products)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(product.Name, Is.Not.Null);
                    Assert.That(product.Description, Is.Not.Null);
                    Assert.That(product.Price, Is.GreaterThan(0));
                });
            }
        }

        [Test]
        public void ReadSingleProductSuccessfullyTest()
        {
            Product product = _productDao.ReadByName("Cappuccino");

            Assert.That(product, Is.Not.Null);
        }
    }
}
