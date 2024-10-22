using CafeSanchez.DataAccess.Entities;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CafeSanchez.DataAccess.DAO;

internal class OrderDao(string connectionString) : IOrderDao
{
    private readonly string _connectionString = connectionString;

    public Order Create(Order entity)
    {
        IDbConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        IDbTransaction transaction = connection.BeginTransaction();

        try
        {
            // Insert order
            string insertOrderSql = "INSERT INTO Orders (CustomerName, Date, Discount) VALUES (@CustomerName, @Date, @Discount); SELECT SCOPE_IDENTITY();";
            entity.Id = connection.ExecuteScalar<int>(insertOrderSql, entity, transaction);

            // Get generated data
            string selectOrderSql = "SELECT WebId, Status FROM Orders WHERE Id = @id";
            dynamic generated = connection.QuerySingle(selectOrderSql, entity, transaction);

            entity.WebId = generated.WebId;
            entity.Status = generated.Status;

            // Insert orderlines
            string insertOrderlineSql = "INSERT INTO Orderlines (OrderId, ProductId, Quantity) VALUES (@OrderId, @ProductId, @Quantity);";
            foreach (Orderline orderline in entity.Orderlines)
            {
                connection.Execute(insertOrderlineSql, new { OrderId = entity.Id, ProductId = orderline.Product.Id, orderline.Quantity }, transaction);
            }
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        transaction.Commit();

        return entity;
    }

    public IEnumerable<Order> Read()
    {
        // Only selects active orders
        string selectOrdersSql = "SELECT * FROM Orders WHERE Status != 'Finished'";
        string selectOrderlinesSql = "SELECT ol.*, p.* FROM Orderlines ol JOIN Products p ON p.Id = ol.ProductId WHERE ol.OrderId = @orderId";

        using IDbConnection connection = new SqlConnection(_connectionString);
        IEnumerable<Order> orders = connection.Query<Order>(selectOrdersSql);

        foreach (Order order in orders)
        {
            order.Orderlines = connection.Query<Orderline, Product, Orderline>(selectOrderlinesSql, (ol, p) =>
            {
                ol.Product = p;
                return ol;

            }, new { OrderId = order.Id }).ToList();
        }
        return orders;
    }

    public Order Update(Order entity)
    {
        string updateStateSql = "UPDATE Orders SET Status = @status WHERE Id = @id";

        IDbConnection connection = new SqlConnection(_connectionString);
        if (connection.Execute(updateStateSql, entity) == 1)
        {
            return FindById(entity.Id);
        }
        return entity;
    }

    public bool Delete(Order entity)
    {
        throw new NotImplementedException();
    }

    public Order FindById(int id)
    {
        string selectOrderSql = "SELECT * FROM Orders WHERE Id = @id";
        string selectOrderlinesSql = "SELECT ol.*, p.* FROM Orderlines ol JOIN Products p ON p.Id = ol.ProductId WHERE ol.OrderId = @orderId";

        using IDbConnection connection = new SqlConnection(_connectionString);
        Order order = connection.QuerySingle<Order>(selectOrderSql, new { Id = id });

        order.Orderlines = connection.Query<Orderline, Product, Orderline>(selectOrderlinesSql, (ol, p) =>
        {
            ol.Product = p;
            return ol;

        }, new { OrderId = order.Id }).ToList();

        return order;
    }
}
