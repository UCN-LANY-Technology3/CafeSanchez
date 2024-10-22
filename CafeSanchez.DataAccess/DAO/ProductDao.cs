using CafeSanchez.DataAccess.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess.DAO;
internal class ProductDao(string connectionString) : IProductDao
{
    private string _connectionString = connectionString;

    public Product Create(Product entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> Read()
    {
        string selectSql = "SELECT Id, WebId, Name, Description, Price FROM Products";

        IDbConnection connection = new SqlConnection(_connectionString);

        return connection.Query<Product>(selectSql);
    }

    public Product Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Product entity)
    {
        throw new NotImplementedException();
    }

    public Product FindByName(string name)
    {
        string selectSql = "SELECT Name, Description, Price FROM Products WHERE Name = @Name";

        IDbConnection connection = new SqlConnection(_connectionString);

        return connection.QuerySingle<Product>(selectSql, new { Name = name });
    }
}
