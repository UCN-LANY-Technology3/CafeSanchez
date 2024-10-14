using CafeSanchez.DataAccess;
using CafeSanchez.DataAccess.DAO;
using CafeSanchez.DataAccess.Entities;

namespace CafeSanchez.DataAccessTests;

public class OrderTests
{
    private IDao<Order> _orderDao;

    [SetUp]
    public void Setup()
    {
        _orderDao = new OrderDao("Server=192.168.56.101; Database=CafeSanchez; User Id=sa; Password=P@$$w0rd; TrustServerCertificate=True");
    }

    [Test]
    public void InstantiateOrderTest()
    {
        Order test = new Order() { CustomerName = "Hans", Date = DateTime.Now, Discount = 10 };

        Assert.That(test, Is.Not.Null);
    }

    [Test]
    public void CreateOrderTest()
    {
        Product product = new Product() { Id = 1, Name = "Ligegyldigt", Description = "Underordnet" };

        List<Orderline> orderlines = [new Orderline() { Product = product, Quantity = 2 }];

        Order order = new Order() { CustomerName = "Hans", Date = DateTime.Now, Discount = 10, Orderlines = orderlines };

        var test = _orderDao.Create(order);

        Assert.That(test, Is.Not.Null);
        Assert.That(test, Is.InstanceOf<Order>());
        Assert.That(test.Id, Is.GreaterThan(0));
    }
}