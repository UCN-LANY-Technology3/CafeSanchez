using CafeSanchez.DataAccess.Entities;

namespace CafeSanchez.DataAccessTests
{
    public class OrderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InstantiateOrderTest()
        {
            Order test = new Order() { CustomerName = "Hans"};

            Assert.That(test, Is.Not.Null);
        }
    }
}