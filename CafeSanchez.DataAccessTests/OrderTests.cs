using CafeSanchez.DataAccess;
using CafeSanchez.DataAccess.DAO;
using CafeSanchez.DataAccess.Entities;

namespace CafeSanchez.DataAccessTests;

public class OrderTests
{
    private IDao<Order> _orderDao;
    private readonly string _connectionString = "Server=localhost; Database=CafeSanchez; User Id=sa; Password=1StrongPassword!; TrustServerCertificate=True";

    [SetUp]
    public void Setup()
    {
        _orderDao = DaoFactory.Create<IOrderDao>(_connectionString);
    }

    [Test]
    public void InstantiateOrderTest()
    {
        Order test = new() { CustomerName = "Hans", Date = DateTime.Now, Discount = 10, Status = "New" };

        Assert.That(test, Is.Not.Null);
    }

    [Test]
    public void CreateOrderTest()
    {
        Product product = new() { Id = 1, Name = "Ligegyldigt", Description = "Underordnet" };

        List<Orderline> orderlines = [new Orderline() { Product = product, Quantity = 2 }];

        Order order = new() { CustomerName = "Hans", Date = DateTime.Now, Discount = 10, Status = "New", Orderlines = orderlines };

        var test = _orderDao.Create(order);

        Assert.That(test, Is.Not.Null);
        Assert.That(test, Is.InstanceOf<Order>());
        Assert.Multiple(() =>
        {
            Assert.That(test.Id, Is.GreaterThan(0));
            Assert.That(test.WebId, Is.Not.Empty);
            Assert.That(test.Status, Is.EqualTo("New"));
            Assert.That(test.Orderlines, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void ReadOrdersSuccessfullyTest()
    {
        var test = _orderDao.Read();

        Assert.That(test, Is.Not.Null);
        foreach (Order order in test)
        {
            Assert.That(order.Orderlines, Is.Not.Null);
            Assert.That(order.Orderlines, Is.Not.Empty);
        }
    }

    [Test]
    public void UpdateOrderStatusSuccessfullyTest()
    {
        var test = _orderDao.Read().First();
        test.Status = "Finished";
        test = _orderDao.Update(test);
        Assert.That(test.Status, Is.EqualTo("Finished"));
    }
}