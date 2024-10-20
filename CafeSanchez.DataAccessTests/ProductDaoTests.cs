using CafeSanchez.DataAccess;
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
        private readonly string _connectionString = "Server=localhost; Database=CafeSanchez; User Id=sa; Password=1StrongPassword!; TrustServerCertificate=True";

        private IProductDao _productDao;

        [SetUp]
        public void SetUp()
        {
            _productDao = DaoFactory.Create<IProductDao>(_connectionString);
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
            Product product = _productDao.FindByName("Cappuccino");

            Assert.That(product, Is.Not.Null);
        }
    }
}
