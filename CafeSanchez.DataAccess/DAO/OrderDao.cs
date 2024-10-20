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
        throw new NotImplementedException();
    }

    public Order Update(Order entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Order entity)
    {
        throw new NotImplementedException();
    }
}
